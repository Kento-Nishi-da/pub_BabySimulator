using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Const;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    Vector3 enabledButtonPos;

    [SerializeField]
    GameObject nappyPanel;
    [SerializeField]
    GameObject newNappy;
    [SerializeField]
    GameObject oldNappy;
    
    
    [SerializeField]
    GameObject toyPanel;

    //ミルク関連
    [Header("ミルク関連")]
    [SerializeField]
    GameObject milkPanel;
    [SerializeField]
    GameObject babyBottle;
    [SerializeField]
    Text milkNumText;
    [SerializeField]
    GameObject[] FullTexts;
    [SerializeField]
    GameObject Triangle;
    [SerializeField]
    Image[] milkBars;
    [Range(0, 300)]
    public float milkQuantity;
    [Range(0f, 100f)]
    public float milkSpeed;
    public float milkNum;
    public float discardTime;
    public const int milkCapacity = 100;
    public bool replenishmentFlg;

    [SerializeField]
    Text scoreText;

    // 画面モード
    public enum DisplayState
    {
        MILK,
        TOY,
        NAPPY,


        DEFAULT = -1
    }

    // ３つのボタン
    [SerializeField]
    GameObject[] actionButtons;

    public DisplayState dispState;


    // Start is called before the first frame update
    void Start()
    {
        dispState = DisplayState.DEFAULT;
        var tmp = Common.TEST;

        newNappy.SetActive(false);
        oldNappy.SetActive(false);

        milkQuantity = 0;
        milkNum = 0;
        discardTime = 0;
        replenishmentFlg = false;
        milkPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        /* 西田：おむつテスト用 */
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            newNappy.SetActive(true);
            oldNappy.SetActive(true);
        }
        /* 西田：テストここまで */

        //ミルク補充処理
        if(replenishmentFlg)
        {
            milkQuantity += Time.deltaTime * milkSpeed;
            if (milkQuantity >= 300)
            {
                milkQuantity = 300;
            }

            discardTime = 0;
            Triangle.SetActive(false);
        }
        milkNum = milkQuantity / milkCapacity;

        DispMilkBar();
        DispFullText();

        milkNumText.text = Mathf.Floor(milkNum).ToString();

        discardTime +=  1f * Time.deltaTime;

        if(discardTime > 10)
        {
            Triangle.SetActive(true);
            milkQuantity -= 5f * Time.deltaTime;
            if(milkQuantity < 0)
            {
                Triangle.SetActive(false);
                milkQuantity = 0;
            }
        }
        
    }


    /// <summary>
    /// ボタンのオンオフ制御
    /// </summary>
    void PushButton(DisplayState _state)
    {
        nappyPanel.SetActive(false);
        toyPanel.SetActive(false);
        milkPanel.SetActive(false);
        babyBottle.SetActive(false);
        if (dispState == _state)
        {
            actionButtons[(int)dispState].transform.position -= enabledButtonPos;
            dispState = DisplayState.DEFAULT;


        }
        else
        {
            if (dispState != DisplayState.DEFAULT)
            {
                actionButtons[(int)dispState].transform.position -= enabledButtonPos;
            }
            dispState = _state;
            actionButtons[(int)dispState].transform.position += enabledButtonPos;
        }
    }


    /// <summary>
    /// ミルクボタン押下時処理
    /// </summary>
    public void PushMilkButton()
    {
        PushButton(DisplayState.MILK);

        if (dispState == DisplayState.DEFAULT)
        {
            milkPanel.SetActive(false);
        }
        else
        {
            milkPanel.SetActive(true);
        }

    }

    /// <summary>
    /// おむつボタン押下時
    /// </summary>
    public void PushNappyButton()
    {
        PushButton(DisplayState.NAPPY);

        if(dispState == DisplayState.DEFAULT)
        {
            nappyPanel.SetActive(false);
        }
        else
        {
            nappyPanel.SetActive(true);
            if(GameObject.Find("Baby").GetComponent<BabyManager>().nappyPops.Count > 0)
            {
                newNappy.SetActive(true);
                oldNappy.SetActive(true);
            }
        }
    }

    public void PushToyButton()
    {
        PushButton(DisplayState.TOY);
        
        if( dispState == DisplayState.DEFAULT)
        {
            toyPanel.SetActive(false);
        }
        else
        {
            toyPanel.SetActive(true);
        }
    }

    /// <summary>
    /// ミルク補充ボタン押下時
    /// </summary>
    public void ReplenishmentButtonDown()
    {
        replenishmentFlg = true;
    }

    /// <summary>
    /// ミルク補充ボタン離した時
    /// </summary>
    public void ReplenishmentButtonUp()
    {
        replenishmentFlg = false;
    }

    /// <summary>
    /// ミルク使用ボタン押下時
    /// </summary>
    public void PushUseButton()
    {

        if(milkNum >= 1)
        {
            milkPanel.SetActive(false);
            babyBottle.SetActive(true);
            babyBottle.GetComponent<Milk>().count = 0;
            babyBottle.GetComponent<Image>().fillAmount = 0;
            babyBottle.GetComponent<Milk>().StopDrag();
        }
        
    }

    public void DispMilkBar()
    {
        if (milkNum < 1)
        {
            milkBars[0].fillAmount = milkNum;
            milkBars[1].fillAmount = 0;
            milkBars[2].fillAmount = 0;
        }
        else if (milkNum < 2)
        {
            milkBars[0].fillAmount = 1;
            milkBars[1].fillAmount = milkNum - 1;
            milkBars[2].fillAmount = 0;
        }
        else if (milkNum < 3)
        {
            milkBars[0].fillAmount = 1;
            milkBars[1].fillAmount = 1;
            milkBars[2].fillAmount = milkNum - 2;
        }
    }

    public void DispFullText()
    {
        if (milkNum < 1)
        {
            FullTexts[0].SetActive(false);
            FullTexts[1].SetActive(false);
            FullTexts[2].SetActive(false);
        }
        else if (milkNum < 2)
        {
            FullTexts[0].SetActive(true);
            FullTexts[1].SetActive(false);
            FullTexts[2].SetActive(false);
        }
        else if (milkNum < 3)
        {
            FullTexts[0].SetActive(true);
            FullTexts[1].SetActive(true);
            FullTexts[2].SetActive(false);
        }
        else
        {
            FullTexts[0].SetActive(true);
            FullTexts[1].SetActive(true);
            FullTexts[2].SetActive(true);
        }
    }

    public void ScoreDisp(int _score)
    {
        scoreText.text = "達成数：" + _score;
    }
}
