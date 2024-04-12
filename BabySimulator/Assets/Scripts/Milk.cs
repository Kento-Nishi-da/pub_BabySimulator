using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Const;
using UnityEngine.UI;

public class Milk : Drag
{
    [SerializeField]
    protected Image Bar;

    GameManager gameManager;
    public float count = 0;
    [SerializeField, Range(-4,0)]
    public float speed;

    [SerializeField]
    bool tmp;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();

        tmp = isDrag;

        if(transform.position.x < -3 || transform.position.x > 3)
        {
            transform.position = new Vector3(0,-1);
        }

        if(transform.position.y >= -3 && isDrag == false)
        { 
            transform.position += new Vector3(0,speed) * Time.deltaTime;
        }
    }

    protected override void OnDrag()
    {
        base.OnDrag();

        var mPos = Common.GetWorldMousePosition();
        // ドロップしたときに目標座標なら達成、そうでないなら戻るかそのまま
        if (
            mPos.x <= dropPos.x + dropPosWidthHeight.x && mPos.x >= dropPos.x - dropPosWidthHeight.x &&
            mPos.y <= dropPos.y + dropPosWidthHeight.y && mPos.y >= dropPos.y - dropPosWidthHeight.y
            )
        {
            count += Time.deltaTime;
            Bar.fillAmount = count / 2;
        }
        else
        {
            count = 0;
            Bar.fillAmount = 0;
        }

        if(count >= 2)
        {
            SuccessDrop();
        }
    }

    protected override void EndDrag()
    {
        Bar.fillAmount = 0;
        count = 0;
        isDrag = false;

    }

    protected override void SuccessDrop()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameManager.milkQuantity -= 100;
        gameManager.milkNum -= 1;
        gameManager.DispMilkBar();
        base.SuccessDrop();
        isDrag = false;
        gameManager.PushMilkButton();
        var baby =  GameObject.Find("Baby").GetComponent<BabyManager>();
        if (baby.milkPops.Count > 0)
        {
            GameObject.Find("Baby").GetComponent<BabyManager>().SuccessMilk();
        }
    }

    public void StopDrag()
    {
        isDrag = false;
    }
}
