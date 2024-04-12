using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Const;

public class Toy : Drag
{
    [SerializeField, Range(0, 10)]
    protected int shakeAmount = 10;
    [SerializeField]
    protected int shakeCnt;


    protected enum ShakeState
    {
        DEFAULT,
        RIGHT,
        LEFT
    }

    // 1で右振り、2で左振り、0は初期状態
    protected ShakeState shakeState;

    [SerializeField, Range(0f, 1f)]
    float shakeLen = 0.7f;

    protected override void Start()
    {
        base.Start();
        shakeCnt = 0;
        shakeState = ShakeState.DEFAULT;
    }

    protected override void EndDrag()
    {
        isDrag = false;
        //transform.position = stPos;
        transform.parent.localRotation = new Quaternion(0, 0, 0, 0);
    }

    /// <summary>
    /// ドラッグ中は距離に応じて傾ける
    /// </summary>
    protected override void OnDrag()
    {
        var mPos = Common.GetWorldMousePosition();

        var diff = mPos.x - transform.position.x;

        // 大きさをそろえる
        if(diff > 1)
        {
            diff = 1;
        }

        if(diff < -1)
        {
            diff = -1;
        }

        // 45~-45に正規化
        var euler = diff * 45;
        //vec.x = euler;

        // 角度の作成
        var rotate = new Quaternion(0, 0, 1, 0);
        // オイラー角で計算、Quaternionは左右逆周りなのでマイナスをかける
        rotate.eulerAngles = new Vector3 (0, 0, euler) * -1;
        // 計算した値分傾ける
        transform.parent.localRotation = rotate;

        if(shakeAmount > shakeCnt)
        {
            // ひと振り目は左右区別なし
            switch(shakeState)
            {
                case ShakeState.DEFAULT:
                    if (diff > shakeLen)
                    {
                        shakeCnt++;
                        shakeState = ShakeState.LEFT;
                    }
                    if (diff < -shakeLen)
                    {
                        shakeCnt++;
                        shakeState = ShakeState.RIGHT;
                    }
                    break;
                case ShakeState.RIGHT:
                    if (diff > shakeLen)
                    {
                        shakeCnt++;
                        shakeState = ShakeState.LEFT;
                    }
                    break;
                case ShakeState.LEFT:
                    if (diff < -shakeLen)
                    {
                        shakeCnt++;
                        shakeState = ShakeState.RIGHT;
                    }
                    break;
            }
        }
        else
        {
            // タスク完了
            SuccessToy();
        }


        
    }

    /// <summary>
    /// タスク成功時の処理
    /// </summary>
    virtual protected void SuccessToy()
    {
        shakeCnt = 0;
        shakeState = 0;
        // おもちゃタスク完了
        var baby = GameObject.Find("Baby").GetComponent<BabyManager>();
        if (baby.toyPops.Count > 0)
        {
            baby.SuccessToy();
        }
        GameObject.Find("GameManager").GetComponent<GameManager>().PushToyButton();
        EndDrag();
        print("おもちゃで機嫌よくなったよ");
    }
}
