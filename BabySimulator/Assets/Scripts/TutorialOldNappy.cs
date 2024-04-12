using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialOldNappy : OldNappy
{
    protected override void SuccessDrop()
    {
        gameObject.SetActive(false);

        GameObject.Find("NewNappy").GetComponent<TutorialNewNappy>().Dump();
        print("ŽÌ‚Ä‚½");
    }
}
