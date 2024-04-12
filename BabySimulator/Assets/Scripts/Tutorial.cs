using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Const;

public class Tutorial : MonoBehaviour
{
    enum TutorialFase
    {
        FASE0_RULE,// 目的説明
        FASE1_MILK,// ミルク説明
        FASE2,
        FASE3,
        FASE4,
    }
    TutorialFase fase;
    [Header("チュートリアル機能")]
    [SerializeField]
    GameObject fasePanelParent;
    int pagecCnt = 0;
    int pageNum = 4;
    bool isMilkCherged;
    Vector3 pageSize = new Vector3(1080, 0, 0);

    [SerializeField]
    GameObject[] buttonArrows;
    [SerializeField]
    GameObject[] panelDisplays;
    [SerializeField]
    Text[] displays;
    [SerializeField]
    GameObject[] arrows;
    [SerializeField]
    GameObject highLightsPanel;
    [SerializeField]
    GameObject[] highLightsButtons;
    [SerializeField]
    GameObject[] tutoPops;


    [SerializeField]
    GameObject compPanel;


    [Space]
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
    Image[] milkBars;
    [Range(0, 300)]
    public float milkQuantity;
    [Range(0f, 100f)]
    public float milkSpeed;
    public float milkNum;
    public const int milkCapacity = 100;
    public bool replenishmentFlg;


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
        isMilkCherged = false;
        fase = TutorialFase.FASE0_RULE;
        fasePanelParent.SetActive(true);
        buttonArrows[0].SetActive(false);
        buttonArrows[1].SetActive(true);
        pageNum = fasePanelParent.transform.childCount - 1;
        fasePanelParent.transform.localPosition = -pageSize;
        foreach(var arrow in arrows)
        {
            arrow.SetActive(false);
        }



        dispState = DisplayState.DEFAULT;
        //var tmp = Common.TEST;

        milkQuantity = 0;
        milkNum = 0;
        replenishmentFlg = false;
        milkPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        //ミルク補充処理
        if (replenishmentFlg)
        {
            milkQuantity += Time.deltaTime * milkSpeed;
            if (milkQuantity >= 300)
            {
                milkQuantity = 300;
                
                if(!isMilkCherged)
                {
                    MilkChergeComplete();
                    isMilkCherged = true;
                }
            }

            milkNum = milkQuantity / milkCapacity;
        }

        DispMilkBar();
        DispFullText();

        milkNumText.text = Mathf.Floor(milkNum).ToString();

    }


    /// <summary>
    /// ボタンのオンオフ制御
    /// </summary>
    void PushButton(DisplayState _state)
    {
        nappyPanel.SetActive(false);
        toyPanel.SetActive(false);
        milkPanel.SetActive(false);
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

        if (dispState == DisplayState.DEFAULT)
        {
            nappyPanel.SetActive(false);
        }
        else
        {
            nappyPanel.SetActive(true);
            if (GameObject.Find("Baby").GetComponent<BabyManager>().nappyPops.Count > 0)
            {
                newNappy.SetActive(true);
                oldNappy.SetActive(true);
            }
        }
    }

    public void PushToyButton()
    {
        PushButton(DisplayState.TOY);

        if (dispState == DisplayState.DEFAULT)
        {
            toyPanel.SetActive(false);
        }
        else
        {
            toyPanel.SetActive(true);
        }
    }

    #region ミルク処理
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
        milkPanel.SetActive(false);

        if (milkNum >= 1)
        {
            babyBottle.SetActive(true);
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
    #endregion




    public void PushArrowRightButton()
    {
        pagecCnt++;
        buttonArrows[0].SetActive(true);
        if (pagecCnt == pageNum)
        {
            buttonArrows[1].SetActive(false);
        }
        fasePanelParent.GetComponent<RectTransform>().localPosition -= pageSize;
        //fasePanelParent.transform.position -= pageSize;
    }

    public void PushArrowLeftButton()
    {
        pagecCnt--;
        buttonArrows[1].SetActive(true);
        if (pagecCnt == 0)
        {
            buttonArrows[0].SetActive(false);
        }
        fasePanelParent.GetComponent<RectTransform>().localPosition += pageSize;
    }

    public void PushTestStartButton()
    {
        fase++;
        fasePanelParent.SetActive(false);
        buttonArrows[0].SetActive(false);
        var msg = "↑\n" + "赤ちゃんが要求したら\n" + "そのタブを開く\n" + "↓";
        DispMsg(0, msg);
    }

    public void DispMsg(int _index, string _msg)
    {
        displays[_index].text = _msg;
        panelDisplays[_index].gameObject.SetActive(true);
    }

    // ミルクボタン押す手前
    public void PushFinExplainButton()
    {
        panelDisplays[0].gameObject.SetActive(false);
        var msg = "まずはミルクから";
        panelDisplays[0].transform.GetChild(1).gameObject.SetActive(false);
        arrows[0].SetActive(true);
        highLightsPanel.SetActive(true);

        DispMsg(1, msg);
    }

    /// <summary>
    /// ミルクボタンを押したとき
    /// </summary>
    public void PushMilkTestFunc()
    {
        highLightsButtons[0].SetActive(false);
        //highLightsPanel.SetActive(false);
        panelDisplays[1].SetActive(false);
        var msg = "まずは補充を長押し\n" + "三本まで補充できるよ";
        arrows[0].SetActive(false);
        DispMsg(2, msg);
    }

    // 補充を押す手前
    public void PushMilkExplainButton()
    {
        highLightsPanel.SetActive(false);
        panelDisplays[2].SetActive(false);
    }


    // ミルク補充完了時
    void MilkChergeComplete()
    {
        HighLightsOn(1);
        var msg = "次は飲ませてあげよう\n" + "ドラッグしたまま赤ちゃんの口でキープ";
        DispMsg(2, msg);
        panelDisplays[2].transform.GetChild(1).gameObject.SetActive(false);
    }

    // ミルク出現
    public void MilkUsed()
    {
        PushUseButton();
        HighLightsOff(1);
    }

    // おもちゃのボタン押す前
    public void SuccessMilk()
    {
        HighLightsOff(1);
        HighLightsOn(2);
        var msg = "OK!\n次はおもちゃを振ろう";
        arrows[1].SetActive(true);
        DispMsg(2, msg);
    }

    // おもちゃボタン押したら
    public void PushToyTestFunc()
    {
        HighLightsOff(2);
        highLightsPanel.SetActive(true);
        panelDisplays[1].SetActive(false);
        var msg = "おもちゃをドラッグして\n" + "左右に振りまくれ";
        arrows[1].SetActive(false);
        DispMsg(3, msg);
    }

    // OKおしたら
    public void PushToyTestOK()
    {
        panelDisplays[3].SetActive(false);
        highLightsPanel.SetActive(false);
    }

    // おむつのボタン押す前
    public void SuccessToy()
    {
        HighLightsOn(3);
        var msg = "OK!\n最後はおむつを替えよう";
        arrows[2].SetActive(true);
        DispMsg(3, msg);
        panelDisplays[3].transform.GetChild(1).gameObject.SetActive(false);
    }

    // おむつボタン押したら
    public void PushNappyTestFunc()
    {
        HighLightsOff(3);

        highLightsPanel.SetActive(true);
        panelDisplays[3].SetActive(false);
        var msg = "古いおむつを捨ててから\n" + "新しいおむつに替えよう\n" + "ドラッグ&ドロップで捨てられるよ";
        arrows[2].SetActive(false);
        DispMsg(5, msg);
    }

    // OKおしたら
    public void PushNappyTestOK()
    {
        panelDisplays[5].SetActive(false);
        highLightsPanel.SetActive(false);
        highLightsPanel.SetActive(false);
    }


    // おむつチュートリアルが終わり次第
    public void TutorialComplete()
    {
        compPanel.SetActive(true);
    }




    void HighLightsOn(int _id)
    {
        highLightsPanel.SetActive(true);
        highLightsButtons[_id].SetActive(true);
    }

    void HighLightsOff(int _id)
    {
        highLightsPanel.SetActive(false);
        highLightsButtons[_id].SetActive(false);
    }

    public void PushFinishButton()
    {
        Common.LoadScene("BabyScene");
    }
}
