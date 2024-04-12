using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialToy : Toy
{
    protected override void SuccessToy()
    {
        shakeCnt = 0;
        shakeState = 0;
        // todo: おもちゃタスク完了
        var tuto = GameObject.Find("Tutorial").GetComponent<Tutorial>();
        tuto.SuccessToy();
        tuto.PushToyButton();


        EndDrag();
        print("おもちゃで機嫌よくなったよ");
    }
}
