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
    public TextMeshPro getMoney, getExp, levelText;
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
        Board.GetComponent<SpriteRenderer>().sprite = WL[GameGlobalController.battle? 1:0];
        needexp = 10*(float)(System.Math.Pow(GameGlobalController.chLevel,(1.5f)));
    }

    // Update is called once per frame
    void Update()
    {
        needexp = 10*(float)(System.Math.Pow(GameGlobalController.chLevel,(3/2)));
        getMoney.GetComponent<TextMeshPro>().text = "x" + (int)moneyamount;
        getExp.GetComponent<TextMeshPro>().text = "x" + (int)expamount;
        levelText.GetComponent<TextMeshPro>().text = "Lv." + System.Math.Round(GameGlobalController.totalexp + expamount/needexp);
        ChLevelFill.GetComponent<Image>().fillAmount = (GameGlobalController.totalexp + expamount) % needexp;
        if(stmoney && moneyamount < GameGlobalController.moneycount)  moneyamount += 0.05f;
        else if(stmoney && moneyamount >= GameGlobalController.moneycount)
        {
            stmoney = false;
            stexp = true;
            Eanimator.Play("bigger");
            GameGlobalController.moneycount = 0;
        }
        if(stexp && expamount < GameGlobalController.expcount)  expamount += 0.05f;
        else if(stexp && expamount >= GameGlobalController.expcount)
        {
            stexp = false;
            stmenu = true;
            Canimator.Play("bigger");
            GameGlobalController.expcount = 0;
        }
    }

    void boardin()
    {
        Manimator.Play("bigger");
        stmoney = true;
    }
}
