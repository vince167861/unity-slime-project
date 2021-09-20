using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpController : MonoBehaviour
{
    public GameObject HelpP, LB, RB;
    public Sprite[] hpicture;
    public int nowpage = 0, totalpage = 1;
    //public static bool is_hanim = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RB.SetActive(nowpage != totalpage/* && is_hanim*/);
        LB.SetActive(nowpage != 0);
        HelpP.GetComponent<Image>().sprite = hpicture[nowpage];
    }

    /*public void anim_end()
    {
        is_hanim = true;
    }*/

    public void leftpage()
    {
        nowpage--;
    }

    public void rightpage()
    {
        nowpage++;
    }
}
