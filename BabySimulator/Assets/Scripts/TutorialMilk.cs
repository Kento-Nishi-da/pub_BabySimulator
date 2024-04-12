using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Const;

public class TutorialMilk : Milk
{
    protected override void SuccessDrop()
    {
        var tuto = GameObject.Find("Tutorial").GetComponent<Tutorial>();
        tuto.milkQuantity -= 100;
        tuto.milkNum -= 1;
        tuto.DispMilkBar();
        tuto.PushMilkButton();
        gameObject.SetActive(false);
        tuto.SuccessMilk();
    }

    protected override void OnDrag()
    {
        var mPos = Common.GetWorldMousePosition();
        transform.position = new Vector3(mPos.x, mPos.y, transform.position.z);

        // �h���b�v�����Ƃ��ɖڕW���W�Ȃ�B���A�����łȂ��Ȃ�߂邩���̂܂�
        if (
            mPos.x <= dropPos.x + dropPosWidthHeight.x && mPos.x >= dropPos.x - dropPosWidthHeight.x &&
            mPos.y <= dropPos.y + dropPosWidthHeight.y && mPos.y >= dropPos.y - dropPosWidthHeight.y
            )
        {
            count += Time.deltaTime * 5;
            Bar.fillAmount = count / 5;
        }
        else
        {
            count = 0;

        }

        if (count >= 5)
        {
            SuccessDrop();
        }
    }
}
