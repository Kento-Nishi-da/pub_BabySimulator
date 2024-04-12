using Const;
using UnityEngine;
/// <summary>
/// �V�����ւ��̂��ނ�
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
        // �Â����ނ��̂ĂĂ���Ȃ�
        if(isDumped)
        {
            base.SuccessDrop();
            //�Ԃ����̗~������
            GameObject.Find("Baby").GetComponent<BabyManager>().SuccessNappy();
            GameObject.Find("GameManager").GetComponent<GameManager>().PushNappyButton();
        }
        else
        {
            FailedDrop();
        }

    }

    /// <summary>
    /// �Â����ނ��̂Ă��Ƃ���
    /// </summary>
    public void Dump()
    {
        isDumped = true;
    }
}
