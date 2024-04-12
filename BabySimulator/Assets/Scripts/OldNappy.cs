using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Const;

public class OldNappy : Drag
{
    protected override void OnDrag()
    {
        var tmp = Common.GetWorldMousePosition();
        transform.position = new Vector3(tmp.x, tmp.y, transform.position.z);

    }

    protected override void SuccessDrop()
    {
        base.SuccessDrop();
        GameObject.Find("NewNappy").GetComponent<NewNappy>().Dump();
    }
}
