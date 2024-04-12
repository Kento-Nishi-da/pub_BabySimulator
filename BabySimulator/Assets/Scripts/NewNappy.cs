using Const;
using UnityEngine;
/// <summary>
/// V‚µ‚¢‘Ö‚¦‚Ì‚¨‚Ş‚Â
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
        // ŒÃ‚¢‚¨‚Ş‚Â‚ğÌ‚Ä‚Ä‚¢‚é‚È‚ç
        if(isDumped)
        {
            base.SuccessDrop();
            //Ô‚¿‚á‚ñ‚Ì—~‹‰ğÁ
            GameObject.Find("Baby").GetComponent<BabyManager>().SuccessNappy();
            GameObject.Find("GameManager").GetComponent<GameManager>().PushNappyButton();
        }
        else
        {
            FailedDrop();
        }

    }

    /// <summary>
    /// ŒÃ‚¢‚¨‚Ş‚Â‚ğÌ‚Ä‚½‚Æ‚«‚É
    /// </summary>
    public void Dump()
    {
        isDumped = true;
    }
}
