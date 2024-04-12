using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Const;

public class Pop : MonoBehaviour
{
    public BabyRequest id;
    public float popTimer;

    Color popColor;

    SpriteRenderer tmpsr;
    // Start is called before the first frame update
    void Start()
    {
        popTimer = 15.0f;
        tmpsr = gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>();
        popColor = Color.white;
    }

    // Update is called once per frame
    void Update()
    {
        popTimer -= Time.deltaTime;

        //transform.localScale += new Vector3(0.1f,0.1f);
        transform.localScale += new Vector3(0.0005f, 0.0005f) * 0.5f;
        popColor.g -= Time.deltaTime / 15;
        popColor.b -= Time.deltaTime / 15;

        tmpsr.color = new Color(1, popColor.g, popColor.b);
        if (popTimer <= 0f)
        {
            GameObject.Find("Baby").GetComponent<BabyManager>().TaskFailed(gameObject, id);
        }
    }
}
