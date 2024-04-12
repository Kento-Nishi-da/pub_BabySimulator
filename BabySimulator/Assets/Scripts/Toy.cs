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

    // 1�ŉE�U��A2�ō��U��A0�͏������
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
    /// �h���b�O���͋����ɉ����ČX����
    /// </summary>
    protected override void OnDrag()
    {
        var mPos = Common.GetWorldMousePosition();

        var diff = mPos.x - transform.position.x;

        // �傫�������낦��
        if(diff > 1)
        {
            diff = 1;
        }

        if(diff < -1)
        {
            diff = -1;
        }

        // 45~-45�ɐ��K��
        var euler = diff * 45;
        //vec.x = euler;

        // �p�x�̍쐬
        var rotate = new Quaternion(0, 0, 1, 0);
        // �I�C���[�p�Ōv�Z�AQuaternion�͍��E�t����Ȃ̂Ń}�C�i�X��������
        rotate.eulerAngles = new Vector3 (0, 0, euler) * -1;
        // �v�Z�����l���X����
        transform.parent.localRotation = rotate;

        if(shakeAmount > shakeCnt)
        {
            // �ЂƐU��ڂ͍��E��ʂȂ�
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
            // �^�X�N����
            SuccessToy();
        }


        
    }

    /// <summary>
    /// �^�X�N�������̏���
    /// </summary>
    virtual protected void SuccessToy()
    {
        shakeCnt = 0;
        shakeState = 0;
        // ��������^�X�N����
        var baby = GameObject.Find("Baby").GetComponent<BabyManager>();
        if (baby.toyPops.Count > 0)
        {
            baby.SuccessToy();
        }
        GameObject.Find("GameManager").GetComponent<GameManager>().PushToyButton();
        EndDrag();
        print("��������ŋ@���悭�Ȃ�����");
    }
}
