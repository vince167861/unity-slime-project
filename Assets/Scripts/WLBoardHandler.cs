using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WLBoardHandler : MonoBehaviour
{
    public Sprite[] WL;
    public GameObject Board, Money, Exp, ChLevel;
    public Image ChLevelFill;
    public TextMeshProUGUI getMoney, getExp, levelText;
    Animator Manimator, Eanimator, Canimator;
    static public float expamount, moneyamount, needexp = 0;
    bool stmoney, stexp = false;
    static public bool stmenu = false;
    // Start is called before the first frame update
    void Start()
    {
        stmenu = false;
        Manimator = Money.GetComponent<Animator>();
        Eanimator = Exp.GetComponent<Animator>();
        Canimator = ChLevel.GetComponent<Animator>();
        Board.GetComponent<Image>().sprite = WL[Game.battle? 1:0];
        needexp = 10*(float)(System.Math.Pow(Game.chLevel,(1.5f)));
    }

    // Update is called once per frame
    void Update()
    {
        needexp = 10*(float)(System.Math.Pow(Game.chLevel,(3/2)));
        getMoney.GetComponent<TextMeshProUGUI>().text = "x" + (int)moneyamount;
        getExp.GetComponent<TextMeshProUGUI>().text = "x" + (int)expamount;
        levelText.GetComponent<TextMeshProUGUI>().text = "Lv." + System.Math.Round((Game.totalexp + expamount)/needexp);
        ChLevelFill.GetComponent<Image>().fillAmount = (Game.totalexp + expamount) % needexp;
        if(stmoney && moneyamount < Game.moneycount)  moneyamount += 0.05f;
        else if(stmoney && moneyamount >= Game.moneycount)
        {
            stmoney = false;
            stexp = true;
            Eanimator.Play("bigger");
            Game.moneycount = 0;
        }
        if(stexp && expamount < Game.expcount)  expamount += 0.05f;
        else if(stexp && expamount >= Game.expcount)
        {
            stexp = false;
            stmenu = true;
            Canimator.Play("bigger");
            Game.expcount = 0;
        }
    }

    void boardin()
    {
        Manimator.Play("bigger");
        stmoney = true;
    }
}
