using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialNewNappy : NewNappy
{
    protected override void SuccessDrop()
    {
        // �Â����ނ��̂ĂĂ���Ȃ�
        if (isDumped)
        {
            gameObject.SetActive(false);


            //�Ԃ����̗~������
            //GameObject.Find("Baby").GetComponent<BabyManager>().SuccessNappy();
            GameObject.Find("Tutorial").GetComponent<Tutorial>().TutorialComplete();
            print("�`���[�g���A������");
        }
        else
        {
            FailedDrop();
        }
    }
}
