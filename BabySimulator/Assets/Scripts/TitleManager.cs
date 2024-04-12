using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Const;

public class TitleManager : MonoBehaviour
{
    [SerializeField] GameObject panelStory;
    [SerializeField] GameObject panelLevel;
    public static bool levelPanelFlg = false;
    // Start is called before the first frame update
    void Start()
    {
        //PlayerPrefs.GetInt(Common.KEY_TITLE_MODE, Common.MODE_DEFAULT);
        if (levelPanelFlg)
        {
            panelLevel.SetActive(true); 
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //�X�g�[���[�p�l���̕\��
    public void PushStartButton()
    {
        panelStory.SetActive(true);
    }

    //��Փx�I���p�l���̕\��
    public void PushToLevelSelectButton()
    {
        panelStory.SetActive(false);
        panelLevel.SetActive(true);
    }

    //�e��Փx�{�^�����������Ƃ��̏���
    public void PushLevelButton(int level)
    {
        switch ((Level)level)
        {
            case Level.TUTORIAL:
                PlayerPrefs.SetInt(Common.KEY_GAME_LEVEL, Common.MODE_NORMAL);
                Common.LoadScene("Tutorial");
                break;

            case Level.NORMAL:
                PlayerPrefs.SetInt(Common.KEY_GAME_LEVEL, Common.MODE_HARD);
                Common.LoadScene("BabyScene");
                break;

            case Level.HARD:
                Common.LoadScene("BabyScene");
                break;

            default:
                break;
        }
    }
}
