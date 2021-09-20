using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChInfoController : MonoBehaviour
{
    public GameObject ChP, cLB, cRB;
    public Sprite[] chpicture;
    public static int cnowpage = 0, ctotalpage = 1;
    public static bool is_hanim = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(ctotalpage != DataStorage.totalpage[Game.currentLevel])  ctotalpage = DataStorage.totalpage[Game.currentLevel];
        cRB.SetActive(cnowpage != ctotalpage && is_hanim);
        cLB.SetActive(cnowpage != 0);
        ChP.GetComponent<Image>().sprite = chpicture[cnowpage];
    }

    public void anim_end()
    {
        is_hanim = true;
    }

    public void cleftpage()
    {
        cnowpage--;
    }

    public void crightpage()
    {
        cnowpage++;
    }
}
