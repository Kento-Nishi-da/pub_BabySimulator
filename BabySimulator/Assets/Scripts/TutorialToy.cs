using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialToy : Toy
{
    protected override void SuccessToy()
    {
        shakeCnt = 0;
        shakeState = 0;
        // todo: ��������^�X�N����
        var tuto = GameObject.Find("Tutorial").GetComponent<Tutorial>();
        tuto.SuccessToy();
        tuto.PushToyButton();


        EndDrag();
        print("��������ŋ@���悭�Ȃ�����");
    }
}
