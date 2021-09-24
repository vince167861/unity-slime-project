using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogBoxHandler : MonoBehaviour
{
	public static int dialogID = 0, adviceperson, adwhich = 0;
	public Sprite[] ch;
	static GameObject point;
	static TextMeshProUGUI story;
	static Text teller;
	static Image littlech;
	public static bool isChat = false, playsurprise = false, playHint = false, isAnimPause = false;
	public bool isAnim, isStart = false;
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

	void Update()
	{
		switch (Game.gameState)
		{
			case Game.GameState.Dialog:
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
				story.text = DataStorage.lines[Game.currentLevel][dialogID];
				teller.text = DataStorage.speakerName[DataStorage.speaker[Game.currentLevel][dialogID]];
				littlech.sprite = ch[DataStorage.speaker[Game.currentLevel][dialogID]];
				if (Input.GetMouseButtonDown(0) && (!isAnim || Game.dstopanim))
				{
					if(Game.dstopanim)
					{
						isAnim = false;
						Game.dstopanim = false;
					}
					MainCameraHandler.PlayEntityClip(17);
					littlech.sprite = ch[0];
					story.text = teller.text = "";
					if(Game.currentLevel == 2)	Game.SetPlaying();
					else{
						dialogID++;
						Animation.handler.handle();
					}
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
					MainCameraHandler.PlayEntityClip(17);
					littlech.sprite = ch[0];
					story.text = teller.text = "";
					if (GuildWoman.otheradvice)
					{
						GuildWoman.otheradvice = false;
					}
					else GuildWoman.trigger2 = false;
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
					MainCameraHandler.PlayEntityClip(17);
					story.text = teller.text = "";
					if(Game.storychat == 3)
					{
						MainCameraHandler.PlayEntityClip(15);
						FunisController.disappear_f = true;
					}
					if (Game.storychat >= 4)	ScreenCover.animator.speed = 1;
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
