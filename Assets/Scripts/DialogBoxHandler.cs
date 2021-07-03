using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogBoxHandler : MonoBehaviour
{
	public static int cbnum = 0;
	public static int adviceperson, adwhich = 0;
	public Sprite[] ch;
	static GameObject point;
	static Text story;
	static Text teller;
	static Image littlech;
	public static bool isChat = false;
	public bool isAnim, isStart = false;
	public static GameGlobalController.GameState lastgameState;
	public static Animator animator;

	void Start()
	{
		point = GameObject.Find("Point");
		story = GameObject.Find("Story").GetComponent<Text>();
		teller = GameObject.Find("Teller").GetComponent<Text>();
		littlech = GameObject.Find("Little Character").GetComponent<Image>();
		animator = GetComponent<Animator>();

	}

	// Update is called once per frame
	void Update()
	{
		switch (GameGlobalController.gameState)
		{
			case GameGlobalController.GameState.Animation:
				point.SetActive(true);
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
				point.SetActive(true);
				if(isChat)  story.text = LevelVarity.chat[adviceperson][adwhich];
				else  story.text = LevelVarity.advice[adviceperson][adwhich];
				teller.text = LevelVarity.adteller[adviceperson];
				littlech.sprite = ch[adviceperson];
				if (Input.GetMouseButtonDown(0))
				{
					story.text = teller.text = "";
					guildwoman.trigger2 = false;
					GameGlobalController.gameState = lastgameState;
				}
				break;
			case GameGlobalController.GameState.StartStory:
				point.SetActive(false);
				if(!isAnim && !isStart && (GameGlobalController.storychat == 3 || GameGlobalController.storychat == 6))
				{
					animator.Play("cbsurprise");
					isAnim = true;
					isStart = true;
				}
				story.text = LevelVarity.start[GameGlobalController.storychat - 1];
				teller.text = LevelVarity.stteller[GameGlobalController.storychat - 1];
				littlech.sprite = ch[0];
				if (Input.GetMouseButtonDown(0) && !isAnim)
				{
					story.text = teller.text = "";
					if(GameGlobalController.storychat >= 4)  DarkAnimatorController.animator.speed = 1;
					else  RedlightHandler.animator.speed = 1;
					GameGlobalController.storychat = 0;
					isStart = false;
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

	void animend()
	{
		isAnim = false;
	}
}
