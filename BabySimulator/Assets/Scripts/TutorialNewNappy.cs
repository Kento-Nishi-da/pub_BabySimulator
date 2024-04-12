using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialNewNappy : NewNappy
{
    protected override void SuccessDrop()
    {
        // 古いおむつを捨てているなら
        if (isDumped)
        {
            gameObject.SetActive(false);


            //赤ちゃんの欲求解消
            //GameObject.Find("Baby").GetComponent<BabyManager>().SuccessNappy();
            GameObject.Find("Tutorial").GetComponent<Tutorial>().TutorialComplete();
            print("チュートリアル官僚");
        }
        else
        {
            FailedDrop();
        }
    }
}
