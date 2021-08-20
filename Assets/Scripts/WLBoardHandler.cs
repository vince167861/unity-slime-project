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
	static public float expamount, moneyamount, needexp, lastnexp, nowneedexp = 0;
	bool stmoney, stexp = false;
	static public bool stmenu = false;
	// Start is called before the first frame update
	void Start()
	{
		stmenu = false;
		Manimator = Money.GetComponent<Animator>();
		Eanimator = Exp.GetComponent<Animator>();
		Canimator = ChLevel.GetComponent<Animator>();
		needexp = 10 * Mathf.Pow(Game.chLevel, 1.5f);
	}

	// Update is called once per frame
	void Update()
	{
		switch (Game.gameState)
		{
			case Game.GameState.End:
				Board.GetComponent<Image>().sprite = WL[Game.battle ? 1 : 0];
				Money.SetActive(!Game.battle);
				Exp.SetActive(!Game.battle);
				LosePicture.SetActive(Game.battle);
				if (!Game.battle)
				{
					if ((Game.totalexp + expamount) / needexp >= 1)
					{
						Game.chLevel = (int)Mathf.Floor(Game.chLevel + (Game.totalexp + expamount) / needexp);
						needexp += 10 * (Mathf.Pow(Game.chLevel, 1.5f));
					}
					nowneedexp = 10 * (float)(Mathf.Pow(Game.chLevel, 1.5f));
					if (Game.chLevel > 1) lastnexp = 10 * (float)(Mathf.Pow(Game.chLevel - 1, 1.5f));
					getMoney.GetComponent<TextMeshProUGUI>().text = "x" + (int)Mathf.Round(moneyamount);
					getExp.GetComponent<TextMeshProUGUI>().text = "x" + (int)Mathf.Round(expamount);
					levelText.GetComponent<TextMeshProUGUI>().text = "Lv." + Game.chLevel;
					ChLevelFill.GetComponent<Image>().fillAmount = (Game.totalexp + expamount - lastnexp) / nowneedexp;
					if (stmoney && moneyamount < Mathf.Round(Game.moneycount / Game.playtimes)) moneyamount += 0.5f;
					else if (stmoney && moneyamount >= Mathf.Round(Game.moneycount / Game.playtimes))
					{
						stmoney = false;
						stexp = true;
						Eanimator.Play("bigger");
						Game.moneycount = 0;
					}
					if (stexp && expamount < Mathf.Round(Game.expcount / Game.playtimes)) expamount += 0.25f;
					else if (stexp && expamount >= Mathf.Round(Game.expcount / Game.playtimes))
					{
						stexp = false;
						stmenu = true;
						Canimator.Play("bigger_ch");
						Game.expcount = 0;
					}
				}
				else
				{
					// BUG: Index was outside of array.
					LosePicture.GetComponent<Image>().sprite = LP[Game.lastattack];
					levelText.GetComponent<TextMeshProUGUI>().text = "Lv." + Game.chLevel;
					ChLevelFill.GetComponent<Image>().fillAmount = ((Game.totalexp + expamount) % needexp) / needexp;
					Game.moneycount = 0;
					Game.expcount = 0;
				}
				break;
		}
	}

	void boardin()
	{
		if (!Game.battle)
		{
			Manimator.Play("bigger");
			stmoney = true;
		}
		else stmenu = true;
	}
}
