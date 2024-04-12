using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Const;

/// <summary>
/// ドラッグ&ドロップができるクラス
/// 継承して
/// </summary>
public class Drag : MonoBehaviour
{

    protected Vector3 stPos;
    protected bool isDrag;
    [SerializeField]
    protected Vector2 widthHeight;

    [SerializeField]
    protected Vector2 dropPos;
    [SerializeField]
    protected Vector2 dropPosWidthHeight;


    // Start is called before the first frame update
    virtual protected void Start()
    {
        stPos = transform.position;
        isDrag = false;
    }

    // Update is called once per frame
    virtual protected void Update()
    {
        // ドラッグ開始時の処理
        if (Input.GetMouseButtonDown(0))
        {
            if (CheckDrag())
            {
                BeginDrag();
            }
        }

        // ドラッグ終了時の処理
        if (Input.GetMouseButtonUp(0))
        {
            EndDrag();
        }

        // ドラッグ中の処理
        if (isDrag)
        {
            OnDrag();
        }
    }
    /// <summary>
    /// ドラッグ開始時の処理
    /// </summary>
    virtual protected void BeginDrag()
    {
        isDrag = true;
    }

    /// <summary>
    /// ドラッグ中の処理
    /// </summary>
    virtual protected void OnDrag()
    {
        var mPos = Common.GetWorldMousePosition();
        transform.position = new Vector3(mPos.x, mPos.y, transform.position.z);
    }


    /// <summary>
    /// ドラッグ終了時の処理
    /// ドロップの判定処理を行う
    /// </summary>
    virtual protected void EndDrag()
    {
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
        transform.position = stPos;
    }

    /// <summary>
    /// ドラッグ開始時の当たり判定処理
    /// </summary>
    virtual protected bool CheckDrag()
    {
        var mPos = Common.GetWorldMousePosition();
        var objPos = transform.position;

        // タッチ座標が自分と重なっていたらドラッグ可能
        if (
            mPos.x <= objPos.x + widthHeight.x && mPos.x >= objPos.x - widthHeight.x &&
            mPos.y <= objPos.y + widthHeight.y && mPos.y >= objPos.y - widthHeight.y
            )
        {
            return true;
        }
        return false;
    }


    /// <summary>
    /// ドラッグ終了時に指定のドロップ座標でなかったら呼ばれる処理
    /// </summary>
    virtual protected void FailedDrop()
    {
        
    }

    /// <summary>
    /// ドラッグ終了時に指定のドロップ座標だったら呼ばれる処理
    /// </summary>
    virtual protected void SuccessDrop()
    {
        gameObject.SetActive(false);
    }
}
