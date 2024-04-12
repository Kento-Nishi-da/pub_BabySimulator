using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Const;

public class Tutorial : MonoBehaviour
{
    enum TutorialFase
    {
        FASE0_RULE,// �ړI����
        FASE1_MILK,// �~���N����
        FASE2,
        FASE3,
        FASE4,
    }
    TutorialFase fase;
    [Header("�`���[�g���A���@�\")]
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

    //�~���N�֘A
    [Header("�~���N�֘A")]
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


    // ��ʃ��[�h
    public enum DisplayState
    {
        MILK,
        TOY,
        NAPPY,


        DEFAULT = -1
    }

    // �R�̃{�^��
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

        //�~���N��[����
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
    /// �{�^���̃I���I�t����
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
    /// �~���N�{�^������������
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
    /// ���ނ{�^��������
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

    #region �~���N����
    /// <summary>
    /// �~���N��[�{�^��������
    /// </summary>
    public void ReplenishmentButtonDown()
    {
        replenishmentFlg = true;
    }

    /// <summary>
    /// �~���N��[�{�^����������
    /// </summary>
    public void ReplenishmentButtonUp()
    {
        replenishmentFlg = false;
    }

    /// <summary>
    /// �~���N�g�p�{�^��������
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
        var msg = "��\n" + "�Ԃ���񂪗v��������\n" + "���̃^�u���J��\n" + "��";
        DispMsg(0, msg);
    }

    public void DispMsg(int _index, string _msg)
    {
        displays[_index].text = _msg;
        panelDisplays[_index].gameObject.SetActive(true);
    }

    // �~���N�{�^��������O
    public void PushFinExplainButton()
    {
        panelDisplays[0].gameObject.SetActive(false);
        var msg = "�܂��̓~���N����";
        panelDisplays[0].transform.GetChild(1).gameObject.SetActive(false);
        arrows[0].SetActive(true);
        highLightsPanel.SetActive(true);

        DispMsg(1, msg);
    }

    /// <summary>
    /// �~���N�{�^�����������Ƃ�
    /// </summary>
    public void PushMilkTestFunc()
    {
        highLightsButtons[0].SetActive(false);
        //highLightsPanel.SetActive(false);
        panelDisplays[1].SetActive(false);
        var msg = "�܂��͕�[�𒷉���\n" + "�O�{�܂ŕ�[�ł����";
        arrows[0].SetActive(false);
        DispMsg(2, msg);
    }

    // ��[��������O
    public void PushMilkExplainButton()
    {
        highLightsPanel.SetActive(false);
        panelDisplays[2].SetActive(false);
    }


    // �~���N��[������
    void MilkChergeComplete()
    {
        HighLightsOn(1);
        var msg = "���͈��܂��Ă����悤\n" + "�h���b�O�����܂ܐԂ����̌��ŃL�[�v";
        DispMsg(2, msg);
        panelDisplays[2].transform.GetChild(1).gameObject.SetActive(false);
    }

    // �~���N�o��
    public void MilkUsed()
    {
        PushUseButton();
        HighLightsOff(1);
    }

    // ��������̃{�^�������O
    public void SuccessMilk()
    {
        HighLightsOff(1);
        HighLightsOn(2);
        var msg = "OK!\n���͂��������U�낤";
        arrows[1].SetActive(true);
        DispMsg(2, msg);
    }

    // ��������{�^����������
    public void PushToyTestFunc()
    {
        HighLightsOff(2);
        highLightsPanel.SetActive(true);
        panelDisplays[1].SetActive(false);
        var msg = "����������h���b�O����\n" + "���E�ɐU��܂���";
        arrows[1].SetActive(false);
        DispMsg(3, msg);
    }

    // OK��������
    public void PushToyTestOK()
    {
        panelDisplays[3].SetActive(false);
        highLightsPanel.SetActive(false);
    }

    // ���ނ̃{�^�������O
    public void SuccessToy()
    {
        HighLightsOn(3);
        var msg = "OK!\n�Ō�͂��ނ�ւ��悤";
        arrows[2].SetActive(true);
        DispMsg(3, msg);
        panelDisplays[3].transform.GetChild(1).gameObject.SetActive(false);
    }

    // ���ނ{�^����������
    public void PushNappyTestFunc()
    {
        HighLightsOff(3);

        highLightsPanel.SetActive(true);
        panelDisplays[3].SetActive(false);
        var msg = "�Â����ނ��̂ĂĂ���\n" + "�V�������ނɑւ��悤\n" + "�h���b�O&�h���b�v�Ŏ̂Ă����";
        arrows[2].SetActive(false);
        DispMsg(5, msg);
    }

    // OK��������
    public void PushNappyTestOK()
    {
        panelDisplays[5].SetActive(false);
        highLightsPanel.SetActive(false);
        highLightsPanel.SetActive(false);
    }


    // ���ނ`���[�g���A�����I��莟��
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
