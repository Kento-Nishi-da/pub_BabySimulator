using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Const;

public class BabyManager : MonoBehaviour
{
    Level level;
    CoolTime speed;

    const int COOLTIME_MIN = 1;
    const int COOLTIME_MAX = 11;
    const int COOLTIME_BOOST = 3;

    public int successNum;
    int continuousNum;
    int life = 3;
    int maxLife = 3;
    float speedBoost;
    bool isDummy;

    public List<BabyRequest> babyRequests = new List<BabyRequest>();
    public List<GameObject> milkPops = new List<GameObject>();
    public List<GameObject> nappyPops = new List<GameObject>();
    public List<GameObject> toyPops = new List<GameObject>();

    [SerializeField,Range(3.0f, 10.0f)]
    float milkCoolTime;
    [SerializeField, Range(3.0f, 10.0f)]
    float nappyCoolTime;
    [SerializeField, Range(3.0f, 10.0f)]
    float toyCoolTime;
    [SerializeField] 
    Sprite[] icons;
    [SerializeField]
    GameObject Pop;
    [SerializeField]
    GameObject Baby;
    Vector3 babyPos;

    GameManager gm;

    // Start is called before the first frame update
    void Start()
    {

        gm = GameObject.Find("GameManager").GetComponent<GameManager>();

        level = (Level)PlayerPrefs.GetInt(Common.KEY_GAME_LEVEL, Common.MODE_NORMAL);
        switch (level)
        {
            case Level.NORMAL:
                speed = CoolTime.SLOW;
                break;

            case Level.HARD:
                speed = CoolTime.NORMAL;
                break;
        }

        continuousNum = 0;

        babyPos = Baby.transform.position;

        speedBoost = 1.0f;

        isDummy = true;

        milkCoolTime = Random.Range(COOLTIME_MIN, COOLTIME_MAX) * COOLTIME_BOOST;
        nappyCoolTime = Random.Range(COOLTIME_MIN, COOLTIME_MAX) * COOLTIME_BOOST;
        toyCoolTime = Random.Range(COOLTIME_MIN, COOLTIME_MAX) * COOLTIME_BOOST;
    }

    // Update is called once per frame
    void Update()
    {
        if(successNum >= 10)
        {
            speed = CoolTime.NORMAL;
        }
        else if(successNum >= 50)
        {
            speed = CoolTime.FAST;
        }

        milkCoolTime -= Time.deltaTime * (int)speed * speedBoost;
        nappyCoolTime -= Time.deltaTime * (int)speed * speedBoost;
        toyCoolTime -= Time.deltaTime * (int)speed * speedBoost;
        if (milkCoolTime < 0)
        {
            UnSubmitDummy();
            babyRequests.Add(BabyRequest.MILK);
            GeneretePop(BabyRequest.MILK);
            milkCoolTime = Random.Range(COOLTIME_MIN, COOLTIME_MAX) * COOLTIME_BOOST * 1.5f;
            //Instantiate(Pop, popPos, Quaternion.identity);
        }
        if (nappyCoolTime < 0)
        {
            UnSubmitDummy();
            babyRequests.Add(BabyRequest.NAPPY);
            GeneretePop(BabyRequest.NAPPY);
            nappyCoolTime = Random.Range(COOLTIME_MIN, COOLTIME_MAX) * COOLTIME_BOOST;
            //Instantiate(Pop, popPos, Quaternion.identity);
        }
        if (toyCoolTime < 0)
        {
            UnSubmitDummy();
            babyRequests.Add(BabyRequest.TOY);
            GeneretePop(BabyRequest.TOY);


            toyCoolTime = Random.Range(COOLTIME_MIN, COOLTIME_MAX) * COOLTIME_BOOST;
            //Instantiate(Pop, popPos, Quaternion.identity);
        }

        gm.ScoreDisp(successNum);
    }

    void GeneretePop(BabyRequest _req)
    {
        var popPos = new Vector2();
        popPos.x = Random.Range(babyPos.x - 1.0f, babyPos.x + 1.0f);
        popPos.y = Random.Range(babyPos.y + 1.0f, babyPos.y + 1.5f);
        var tmp = Instantiate(Pop, popPos, Quaternion.identity);
        var tmpsr = tmp.transform.GetChild(3).gameObject.GetComponent<SpriteRenderer>();
        switch (_req)
        {
            case BabyRequest.MILK:
                milkPops.Add(tmp);
                tmp.GetComponent<Pop>().id = BabyRequest.MILK;
                tmpsr.sprite = icons[(int)BabyRequest.MILK];
                break;

            case BabyRequest.NAPPY:
                nappyPops.Add(tmp);
                tmp.GetComponent<Pop>().id = BabyRequest.NAPPY;
                tmpsr.sprite = icons[(int)BabyRequest.NAPPY];
                break;

            case BabyRequest.TOY:
                toyPops.Add(tmp);
                tmp.GetComponent<Pop>().id = BabyRequest.TOY;
                tmpsr.sprite = icons[(int)BabyRequest.TOY];
                break;
            default:
                break;

        }

    }

    public void SuccessMilk()
    {
        DestroyImmediate(milkPops[0], true);
        milkPops.Remove(milkPops[0]);
        successNum++;
        continuousNum++;
        if (continuousNum == 10)
        {
            HealHp();
        }
    }

    public void SuccessNappy()
    {
        DestroyImmediate(nappyPops[0], true);
        nappyPops.Remove(nappyPops[0]);
        successNum++;
        continuousNum++;
        if (continuousNum == 10)
        {
            HealHp();
        }
    }

    public void SuccessToy()
    {
        DestroyImmediate(toyPops[0], true);
        toyPops.Remove(toyPops[0]);
        successNum++;
        continuousNum++;
        if(continuousNum == 10)
        {
            HealHp();
        }
    }

    void HealHp()
    {
        if(life >= maxLife)
        {
            return;
        }
        life++;
        var tmp = GameObject.Find("Hp");
        var tmpHp = tmp.transform.GetChild(life - 1);
        print(tmpHp.gameObject);
        tmpHp.gameObject.SetActive(true);

    }

    public void TaskFailed(GameObject instance, BabyRequest id)
    {
        if(life > 1)
        {
            switch (id)
            {
                //è¡Ç∑êÅÇ´èoÇµÇÃidÇ™MILKÇæÇ¡ÇΩéû
                case BabyRequest.MILK:
                    DestroyImmediate(milkPops[0], true);
                    milkPops.Remove(milkPops[0]);
                    print("aaa");
                    break;

                //è¡Ç∑êÅÇ´èoÇµÇÃidÇ™NAPPYÇæÇ¡ÇΩéû
                case BabyRequest.NAPPY:
                    DestroyImmediate(nappyPops[0], true);
                    nappyPops.Remove(nappyPops[0]);
                    print("bbb");
                    break;

                //è¡Ç∑êÅÇ´èoÇµÇÃidÇ™TOYÇæÇ¡ÇΩéû
                case BabyRequest.TOY:
                    DestroyImmediate(toyPops[0], true);
                    toyPops.Remove(toyPops[0]);
                    print("ccc");
                    break;
                default:
                    break;

            }
        }
        else
        {
            PlayerPrefs.SetInt(Common.KEY_GAME_SCORE, successNum);
            PlayerPrefs.Save();
            Common.LoadScene("ResultScene");
        }
        var tmp = GameObject.Find("Hp");
        var tmpHp = tmp.transform.GetChild(life - 1);
        tmpHp.gameObject.SetActive(false);
        life--;
        continuousNum = 0;
    }

    public void SubmitDummy()
    {
        print("dummy");
        isDummy = true;
        speedBoost = 1.0f;
    }

    public void UnSubmitDummy()
    {
        if(!isDummy)
        {
            return;
        }
        print("Undummy");
        GameObject.Find("Dummy").GetComponent<Dummy>().UnHoldDummy();
        isDummy = false;
        speedBoost = 1.2f;
    }
}
