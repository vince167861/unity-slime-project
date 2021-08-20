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
	public static Game.GameState lastgameState;
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
		switch (Game.gameState)
		{
			case Game.GameState.Animation:
				point.SetActive(true);
				if (!isAnim && playsurprise)
				{
					animator.Play("cbsurprise");
					isAnim = true;
					playsurprise = false;
				}
				if (playHint)
				{
					circle.SetActive(true);
					circle.GetComponent<RectTransform>().localPosition = DataStorage.circlepoint[Game.currentLevel][0];
					playHint = false;
				}
				story.text = DataStorage.story[Game.currentLevel][cbnum];
				teller.text = DataStorage.teller[Game.currentLevel][cbnum];
				littlech.sprite = ch[DataStorage.littlech[Game.currentLevel][cbnum]];
				if (Input.GetMouseButtonDown(0) && !isAnim)
				{
					littlech.sprite = ch[0];
					story.text = teller.text = "";
					cbnum++;
					Animation.handler.handle();
				}
				break;
			case Game.GameState.Advice:
				point.SetActive(true);
				if (isChat) story.text = DataStorage.chat[adviceperson][adwhich];
				else story.text = DataStorage.advice[adviceperson][adwhich];
				teller.text = DataStorage.adteller[adviceperson];
				littlech.sprite = ch[adviceperson];
				if (Input.GetMouseButtonDown(0))
				{
					littlech.sprite = ch[0];
					story.text = teller.text = "";
					if (guildwoman.otheradvice)
					{
						guildwoman.otheradvice = false;
					}
					else guildwoman.trigger2 = false;
					Game.gameState = lastgameState;
				}
				break;
			case Game.GameState.Story:
				if (FunisController.story == true)
				{
					isAnim = false;
					FunisController.story = false;
				}
				point.SetActive(false);
				if (!isAnim && !isStart && (Game.storychat == 2 || Game.storychat == 3 || Game.storychat == 6))
				{
					if (Game.storychat == 2) FunisController.animator.Play("funis_ststory");
					else animator.Play("cbsurprise");
					isAnim = true;
					isStart = true;
				}
				story.text = DataStorage.start[Game.storychat - 1];
				teller.text = DataStorage.stteller[Game.storychat - 1];
				littlech.sprite = ch[0];
				if (Input.GetMouseButtonDown(0) && !isAnim)
				{
					story.text = teller.text = "";
					if (Game.storychat >= 4) ScreenCover.animator.speed = 1;
					else RedlightHandler.animator.speed = 1;
					Game.storychat = 0;
					isStart = false;
				}
				break;
		}
	}
	public static void advice(int person, int which)
	{
		adviceperson = person;
		adwhich = which;
		lastgameState = Game.gameState;
		Game.gameState = Game.GameState.Advice;
	}

	void animend()
	{
		isAnim = false;
	}
}
