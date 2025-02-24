using Const;
using UnityEngine;
/// <summary>
/// 新しい替えのおむつ
/// </summary>
public class NewNappy : Drag
{
    protected bool isDumped;

    protected override void Start()
    {
        base.Start();
        isDumped = false;
    }

    protected override void SuccessDrop()
    {
        // 古いおむつを捨てているなら
        if(isDumped)
        {
            base.SuccessDrop();
            //赤ちゃんの欲求解消
            GameObject.Find("Baby").GetComponent<BabyManager>().SuccessNappy();
            GameObject.Find("GameManager").GetComponent<GameManager>().PushNappyButton();
        }
        else
        {
            FailedDrop();
        }

    }

    /// <summary>
    /// 古いおむつを捨てたときに
    /// </summary>
    public void Dump()
    {
        isDumped = true;
    }
}
