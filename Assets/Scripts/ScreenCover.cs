﻿using System.Collections.Generic;
using System;
using UnityEngine;

public class ScreenCover : MonoBehaviour
{
	public GameObject dragonPrefab, housePrefab, startScene, startGameButton;
	public static Animator animator;
	public SpriteRenderer spriteRenderer;
#warning Please specify where and when the field 'start' would be used.
	public static bool start = false;
	static GameObject loadingScreen, background2;
	public static bool hadAnimationStarted = false;
	public GameObject loadingScreenReference, background2Reference;
	public static Behaviour flareLayer;

	void Start()
	{
		loadingScreen = loadingScreenReference;
		background2 = background2Reference;
		animator = GetComponent<Animator>();
		flareLayer = Camera.main.GetComponent<FlareLayer>();
	}

	private void Update()
	{
		switch (Game.gameState)
		{
			case Game.GameState.StartGame:
				animator.Play("Start Game");
				break;
			case Game.GameState.Loading:
				// Navigate to different gameState from here.
				switch (Game.storyState)
				{
					case Game.StoryState.NoStory:
						if (Game.currentLevel == 0) Game.gameState = Game.GameState.DarkFadeIn;
						else if (start)
						{
							Slime.ResetState();
							loadingScreen.SetActive(true);
							animator.Play(Game.battle ? "loadgame" : "loadlobby");
							start = false;
						}
						break;
					case Game.StoryState.StartStory:
						Slime.ResetState();
						loadingScreen.SetActive(true);
						animator.Play("loadstory");
						Game.storyState = Game.StoryState.Loading;
						break;
					case Game.StoryState.StoryDragon:
						loadingScreen.SetActive(false);
						animator.speed = 1;
						Game.gameState = Game.GameState.Story;
						break;
				}
				break;
			case Game.GameState.Story:
				MainCameraHandler.storymusic = 1;
				switch (Game.storyState)
				{
					case Game.StoryState.House:
					case Game.StoryState.State7:
						animator.speed = 1;
						break;
				}
				break;
			case Game.GameState.DarkFadeOut:
				if (!hadAnimationStarted)
				{
					spriteRenderer.color = new Color(0, 0, 0);
					animator.Play("Fade Out");
                    hadAnimationStarted = true;
				}
				break;
			case Game.GameState.DarkFadeIn:
				if (!hadAnimationStarted)
				{
					spriteRenderer.color = new Color(0, 0, 0);
					animator.Play("Fade In");
                    hadAnimationStarted = true;
				}
				break;
			case Game.GameState.BrightFadeOut:
				if (!hadAnimationStarted)
				{
					spriteRenderer.color = new Color(1, 1, 1);
					animator.Play("Fade Out");
                    hadAnimationStarted = true;
				}
				break;
			case Game.GameState.BrightFadeIn:
				if (!hadAnimationStarted)
				{
					spriteRenderer.color = new Color(1, 1, 1);
					animator.Play("Fade In");
                    hadAnimationStarted = true;
				}
				break;
		}
	}

	/// <summary> For animation 'Start Game' callback. </summary>
	void Start1()
	{
		animator.speed = 0;
		Slime.instance.animator.Play("Splash Screen Jump");
	}

	// Start2() -> Slime.Start2()

	/// <summary> For animation 'Start Game' callback. </summary>
	void Start3()
	{
		startGameButton.SetActive(true);
		MainCameraHandler.PlayLoopClip(); //Game.gameState = Game.GameState.MenuPrepare;
		// TODO: Fix groundoffire destroy problem.
		Game.storyEffect = Game.StoryEffect.GroundOfFire; // Without this line the fire will be destroyed.
		Instantiate(Game.staticEffect[5]);
	}

	public void EnterBackStory()
	{
		startScene.SetActive(false);
		startGameButton.SetActive(false);
		MainCameraHandler.PlayEntityClip(3);
		animator.Play("Start Story");
		MainCameraHandler.hasMusicPlaying = false;
		Game.storyState = Game.StoryState.StartStory;
		Game.storyEffect = Game.StoryEffect.Clear;
		Game.gameState = Game.GameState.Story;
	}

	/// <summary> Close loading screen. </summary>
	/// <remark> Also change gameState to DarkFadeIn. </remark>
	void EndLoading()
	{
		loadingScreen.SetActive(false);
		Game.gameState = Game.GameState.DarkFadeIn;
	}

	static GameObject houseReference;

	/// <summary> For animation 'BackStory' callback. </summary>
	readonly List<Action> Story = new List<Action>{
		() => {
			animator.SetFloat("speed", 0);
			Game.storyEffect = Game.StoryEffect.ThunderStorm;
			background2.SetActive(true);
			background2.GetComponent<SpriteRenderer>().sprite = UnityEditor.AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Textures/Screen Mask.png");
		},
		() => {
			animator.speed = 0;
			Instantiate(UnityEditor.AssetDatabase.LoadAssetAtPath<GameObject>("Assets/DragonPrefab.prefab"));
		},
		() => {
			flareLayer.enabled = false;
			Game.storyEffect = Game.StoryEffect.Clear;
		},
		() => {
			houseReference = Instantiate(UnityEditor.AssetDatabase.LoadAssetAtPath<GameObject>("Assets/房子內部.prefab"));
		},
		() => {
			Game.storyState = Game.StoryState.Light;
			animator.speed = 0;
			flareLayer.enabled = true;
			Game.storyEffect = Game.StoryEffect.Light;
		},
		() => {
			Destroy(houseReference);
			Game.storyEffect = Game.StoryEffect.GroundOfFire;
		},
		() => {
			Game.storyState = Game.StoryState.State8;
			animator.speed = 0;
			Game.storychat = 4;
		},
		() => {
			Game.storyEffect = Game.StoryEffect.Effect4;
		},
		() => {
			animator.speed = 0;
			Game.storychat = 5;
		},
		() => {
			animator.speed = 0;
			Slime.instance.transform.position = new Vector3(30, 48, 0);
			Game.storychat = 6;
		},
		() => {
			Game.storyEffect = Game.StoryEffect.Clear;
			Game.storyState = Game.StoryState.State9;
			background2.SetActive(false);
		}
	};
	void StoryPhase(int phase) => Story[phase]();

	public static void SkipStory()
	{
		background2.SetActive(false);
		animator.speed = 1;
		animator.Play("idle");
	}
}
