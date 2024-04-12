using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Const;

/// <summary>
/// �h���b�O&�h���b�v���ł���N���X
/// �p������
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
        // �h���b�O�J�n���̏���
        if (Input.GetMouseButtonDown(0))
        {
            if (CheckDrag())
            {
                BeginDrag();
            }
        }

        // �h���b�O�I�����̏���
        if (Input.GetMouseButtonUp(0))
        {
            EndDrag();
        }

        // �h���b�O���̏���
        if (isDrag)
        {
            OnDrag();
        }
    }
    /// <summary>
    /// �h���b�O�J�n���̏���
    /// </summary>
    virtual protected void BeginDrag()
    {
        isDrag = true;
    }

    /// <summary>
    /// �h���b�O���̏���
    /// </summary>
    virtual protected void OnDrag()
    {
        var mPos = Common.GetWorldMousePosition();
        transform.position = new Vector3(mPos.x, mPos.y, transform.position.z);
    }


    /// <summary>
    /// �h���b�O�I�����̏���
    /// �h���b�v�̔��菈�����s��
    /// </summary>
    virtual protected void EndDrag()
    {
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
        transform.position = stPos;
    }

    /// <summary>
    /// �h���b�O�J�n���̓����蔻�菈��
    /// </summary>
    virtual protected bool CheckDrag()
    {
        var mPos = Common.GetWorldMousePosition();
        var objPos = transform.position;

        // �^�b�`���W�������Əd�Ȃ��Ă�����h���b�O�\
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
    /// �h���b�O�I�����Ɏw��̃h���b�v���W�łȂ�������Ă΂�鏈��
    /// </summary>
    virtual protected void FailedDrop()
    {
        
    }

    /// <summary>
    /// �h���b�O�I�����Ɏw��̃h���b�v���W��������Ă΂�鏈��
    /// </summary>
    virtual protected void SuccessDrop()
    {
        gameObject.SetActive(false);
    }
}
