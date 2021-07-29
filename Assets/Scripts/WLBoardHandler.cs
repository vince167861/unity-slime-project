using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WLBoardHandler : MonoBehaviour
{
    public Sprite[] WL;
    public Sprite[] LP;
    public GameObject Board, Money, Exp, ChLevel, LosePicture;
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
        needexp = 10*(Mathf.Pow(GameGlobalController.chLevel,1.5f));
    }

    // Update is called once per frame
    void Update()
    {
        switch(GameGlobalController.gameState)
        {
            case GameGlobalController.GameState.End:
                Board.GetComponent<Image>().sprite = WL[GameGlobalController.battle? 1:0];
                Money.SetActive(!GameGlobalController.battle);
                Exp.SetActive(!GameGlobalController.battle);
                LosePicture.SetActive(GameGlobalController.battle);
                if(!GameGlobalController.battle)
                {
                    needexp = 10*(float)(Mathf.Pow(GameGlobalController.chLevel,1.5f));
                    getMoney.GetComponent<TextMeshProUGUI>().text = "x" + (int)Mathf.Round(moneyamount);
                    getExp.GetComponent<TextMeshProUGUI>().text = "x" + (int)Mathf.Round(expamount);
                    levelText.GetComponent<TextMeshProUGUI>().text = "Lv." + (int)Mathf.Floor(GameGlobalController.chLevel + (GameGlobalController.totalexp + expamount)/needexp);
                    ChLevelFill.GetComponent<Image>().fillAmount = ((GameGlobalController.totalexp + expamount) % needexp) / needexp;
                    if(stmoney && moneyamount < Mathf.Round(GameGlobalController.moneycount/GameGlobalController.playtimes))  moneyamount += 0.5f;
                    else if(stmoney && moneyamount >= Mathf.Round(GameGlobalController.moneycount/GameGlobalController.playtimes))
                    {
                        stmoney = false;
                        stexp = true;
                        Eanimator.Play("bigger");
                        GameGlobalController.moneycount = 0;
                    }
                    if(stexp && expamount < Mathf.Round(GameGlobalController.expcount/GameGlobalController.playtimes))  expamount += 0.25f;
                    else if(stexp && expamount >= Mathf.Round(GameGlobalController.expcount/GameGlobalController.playtimes))
                    {
                        stexp = false;
                        stmenu = true;
                        Canimator.Play("bigger_ch");
                        GameGlobalController.expcount = 0;
                    }
                }
                else
                {
                    LosePicture.GetComponent<Image>().sprite = LP[GameGlobalController.lastattack];
                    levelText.GetComponent<TextMeshProUGUI>().text = "Lv." + GameGlobalController.chLevel;
                    ChLevelFill.GetComponent<Image>().fillAmount = ((GameGlobalController.totalexp + expamount) % needexp) / needexp;
                    GameGlobalController.moneycount = 0;
                    GameGlobalController.expcount = 0;
                }
                break;
        }
    }

    void boardin()
    {
        if(!GameGlobalController.battle)
        {
            Manimator.Play("bigger");
            stmoney = true;
        }
        else  stmenu = true;
    }
}
