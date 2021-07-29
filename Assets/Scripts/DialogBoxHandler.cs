using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogBoxHandler : MonoBehaviour
{
	public static int cbnum = 0;
	public static int adviceperson, adwhich = 0;
	public Sprite[] ch;
	static GameObject point;
	static TextMeshProUGUI story;
	static Text teller;
	static Image littlech;
	public static bool isChat = false;
	public bool isAnim, isStart = false;
	public static bool playsurprise = false, playHint = false;
	public GameObject circle, oval;
	public static GameGlobalController.GameState lastgameState;
	public static Animator animator;

	void Start()
	{
		point = GameObject.Find("Point");
		story = GameObject.Find("Story").GetComponent<TextMeshProUGUI>();
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
				if(!isAnim && playsurprise)
				{
					animator.Play("cbsurprise");
					isAnim = true;
					playsurprise = false;
				}
				if(playHint)
				{
					circle.SetActive(true);
					circle.GetComponent<RectTransform>().localPosition = LevelVarity.circlepoint[GameGlobalController.currentLevel][0];
					playHint = false;
				}
				story.text = LevelVarity.story[GameGlobalController.currentLevel][cbnum];
				teller.text = LevelVarity.teller[GameGlobalController.currentLevel][cbnum];
				littlech.sprite = ch[LevelVarity.littlech[GameGlobalController.currentLevel][cbnum]];
				if (Input.GetMouseButtonDown(0) && !isAnim)
				{
					littlech.sprite = ch[0];
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
					littlech.sprite = ch[0];
					story.text = teller.text = "";
					guildwoman.trigger2 = false;
					GameGlobalController.gameState = lastgameState;
				}
				break;
			case GameGlobalController.GameState.StartStory:
				if(FunisController.story == true)
				{
					isAnim = false;
					FunisController.story = false;
				}
				point.SetActive(false);
				if(!isAnim && !isStart && (GameGlobalController.storychat == 2 ||GameGlobalController.storychat == 3 || GameGlobalController.storychat == 6))
				{
					if(GameGlobalController.storychat == 2)  FunisController.animator.Play("funis_ststory");
					else  animator.Play("cbsurprise");
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
