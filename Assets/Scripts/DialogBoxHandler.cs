using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogBoxHandler : MonoBehaviour
{
	public static int cbnum = 0;
	public static int adviceperson, adwhich = 0;
	public Sprite[] ch;
	static Text story;
	static Text teller;
	static Image littlech;
	public static GameGlobalController.GameState lastgameState;

	void Start()
	{
		story = GameObject.Find("Story").GetComponent<Text>();
		teller = GameObject.Find("Teller").GetComponent<Text>();
		littlech = GameObject.Find("Little Character").GetComponent<Image>();

	}

	// Update is called once per frame
	void Update()
	{
		switch (GameGlobalController.gameState)
		{
			case GameGlobalController.GameState.Animation:
				story.text = LevelVarity.story[GameGlobalController.currentLevel][cbnum];
				teller.text = LevelVarity.teller[GameGlobalController.currentLevel][cbnum];
				littlech.sprite = ch[LevelVarity.littlech[GameGlobalController.currentLevel][cbnum]];
				if (Input.GetMouseButtonDown(0))
				{
					story.text = teller.text = "";
					cbnum++;
					Animation.handler.handle();
				}
				break;
			case GameGlobalController.GameState.Advice:
				story.text = LevelVarity.advice[adviceperson][adwhich];
				teller.text = LevelVarity.adteller[adviceperson];
				littlech.sprite = ch[adviceperson];
				if (Input.GetMouseButtonDown(0))
				{
					story.text = teller.text = "";
					guildwoman.trigger2 = false;
					GameGlobalController.gameState = lastgameState;
				}
				break;
		}
	}
	public static void advice(int person, int which)
	{
		adviceperson = person;
		adwhich = which;
		lastgameState = GameGlobalController.gameState;
		GameGlobalController.gameState = GameGlobalController.GameState.Advice;
	}
}
