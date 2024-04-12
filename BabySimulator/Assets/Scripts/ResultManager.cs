using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Const;

public class ResultManager : MonoBehaviour
{
    public enum ResultScore
    {
        BAD = 0,
        GOOD = 20,
        EXCELLENT = 50
    }

    public enum ButtonType
    {
        CONTINUE,
        BACK
    }

    [SerializeField]
    public Sprite[] BabySprites;

    const float SCORE_TIME = 5.0f;

    int score;
    float dispScore = 0f;

    [SerializeField]
    Text scoreText;
    [SerializeField]
    Image imageBack;
    [SerializeField]
    GameObject BabyResults;

    SpriteRenderer ResultSprites;

    Vector3 babyPos;

    // Start is called before the first frame update
    void Start()
    {
        babyPos = BabyResults.transform.position;
        ResultSprites = BabyResults.GetComponent<SpriteRenderer>();


#if UNITY_EDITOR
        score = PlayerPrefs.GetInt(Common.KEY_GAME_SCORE);
#else
        score = PlayerPrefs.GetInt(Common.KEY_GAME_SCORE, score);
#endif
    }

    // Update is called once per frame
    void Update()
    {
        if(dispScore < score)
        {
            dispScore += (score / SCORE_TIME) * Time.deltaTime;
            //scoreText.text = ((score / 5) * Time.deltaTime).ToString();
            scoreText.text = "ñûë´ÇµÇΩâÒêî:" + dispScore.ToString("F0");

            if(dispScore > (float)ResultScore.GOOD)
            {
                imageBack.color = Color.yellow;
                ResultSprites.sprite = BabySprites[1];
            }
            if(dispScore > (float)ResultScore.EXCELLENT)
            {
                imageBack.color = new Color(1.0f, 0.5f, 0f);
                ResultSprites.sprite = BabySprites[2];
                BabyResults.transform.localScale = new Vector3(0.8f, 0.8f, 0.4f);
            }
        }
        //scoreText.text = ((score / 5) * Time.deltaTime).ToString();
    }

    //É{É^ÉìÇâüÇµÇΩÇ∆Ç´ÇÃèàóù
    public void PushReturnButton(int back)
    {
        switch ((ButtonType)back)
        {
            case ButtonType.CONTINUE:
                Common.LoadScene("BabyScene");
                break;

            case ButtonType.BACK:
                //PlayerPrefs.SetInt(Common.KEY_TITLE_MODE, Common.MODE_SELECT);
                TitleManager.levelPanelFlg = true;
                Common.LoadScene("TitleScene");
                break;

            default:
                break;
        }
    }
}
