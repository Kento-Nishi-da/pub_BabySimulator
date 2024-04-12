using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Const;

/// <summary>
/// ������Ԃ�I�u�W�F�N�g
/// </summary>
public class Dummy : Drag
{
    // ��������񂪂��킦�Ă��邩�ǂ���
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
        // �h���b�v�����Ƃ��ɖڕW���W�Ȃ�B���A�����łȂ��Ȃ�߂邩���̂܂�
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

        // �^�b�`���W�������Əd�Ȃ��Ă��邩�A�Ԃ���񂪂��킦�Ă��Ȃ��Ȃ�h���b�O�\
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
    /// ������Ԃ��f���o�����Ƃ��̏���
    /// </summary>
    // todo:������Ԃ��f���o�����Ƃ��ɌĂ΂��
    public void UnHoldDummy()
    {
        FailedDrop();
    }
}
