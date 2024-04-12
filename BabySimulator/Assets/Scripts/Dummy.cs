using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Const;

/// <summary>
/// おしゃぶりオブジェクト
/// </summary>
public class Dummy : Drag
{
    // あかちゃんがくわえているかどうか
    protected bool isHold;

    protected override void Start()
    {
        base.Start();
        isHold = false;
    }



    protected override void EndDrag()
    {
        if(isHold)
        {
            return;
        }

        if(!isDrag)
        {
            return;
        }

        var mPos = Common.GetWorldMousePosition();
        // ドロップしたときに目標座標なら達成、そうでないなら戻るかそのまま
        if (
            mPos.x <= dropPos.x + dropPosWidthHeight.x && mPos.x >= dropPos.x - dropPosWidthHeight.x &&
            mPos.y <= dropPos.y + dropPosWidthHeight.y && mPos.y >= dropPos.y - dropPosWidthHeight.y
            )
        {
            SuccessDrop();
        }
        else
        {
            FailedDrop();
        }

        isDrag = false;
    }

    protected override bool CheckDrag()
    {
        var mPos = Common.GetWorldMousePosition();
        var objPos = transform.position;

        // タッチ座標が自分と重なっているかつ、赤ちゃんがくわえていないならドラッグ可能
        if (
            mPos.x <= objPos.x + widthHeight.x && mPos.x >= objPos.x - widthHeight.x &&
            mPos.y <= objPos.y + widthHeight.y && mPos.y >= objPos.y - widthHeight.y &&
            isHold == false
            )
        {
            return true;
        }
        return false;
    }

    protected override void SuccessDrop()
    {
        transform.position = dropPos;
        //transform.rotation = Quaternion.Euler( 0, 0, 180 );
        isHold = true;
        GameObject.Find("Baby").GetComponent<BabyManager>().SubmitDummy();
    }

    protected override void FailedDrop()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
        transform.position = stPos;
        isHold = false;
    }

    /// <summary>
    /// おしゃぶりを吐き出したときの処理
    /// </summary>
    // todo:おしゃぶりを吐き出したときに呼ばれる
    public void UnHoldDummy()
    {
        FailedDrop();
    }
}
