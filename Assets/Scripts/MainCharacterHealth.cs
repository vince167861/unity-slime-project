using UnityEngine;
using UnityEngine.UI;

public class MainCharacterHealth : MonoBehaviour
{
	public static float entityMaxHealth, targetHealth, lastHealth;
	public static float tghealamount = 0, tgsufferamount = 0;
	public static bool isSuffer, isHeal = false, anim1, anim2 = false, start = true;
	public int situation = 0;
	public Sprite[] icon;
	public Animator heal, suffer;
	static Animator staticHeal, staticSuffer;
	public Image bar, statusIcon;
	public Text healthText, nameText;

	void Start()
	{
		lastHealth = targetHealth = entityMaxHealth = Slime.instance.defaultHealth;
		staticHeal = heal;
		staticSuffer = suffer;
	}

	void Update()
	{
		switch (Game.gameState)
		{
			case Game.GameState.Dialog:
			case Game.GameState.Shaking:
			case Game.GameState.Playing:
				if (start)
				{
					situation = 0;
					entityMaxHealth = Mathf.Round(1002 - 0.092f * Mathf.Pow(Game.chLevel - 100, 2));
					lastHealth = targetHealth = entityMaxHealth;
					tghealamount = tgsufferamount = 0;
					HealChange.healAmount = 0;
					SufferChange.sufferAmount = 0;
					isHeal = isSuffer = anim1 = anim2 = start = false;
				}
				switch (situation)
				{
					case 0: Slime.suppression = 1; break;
					case 1: Slime.suppression = 0.4f; break;
				}
				heal.speed = tghealamount > 30 ? tghealamount / 30 : 1;
				suffer.speed = tgsufferamount > 30 ? tghealamount / 30 : 1;
				statusIcon.sprite = icon[situation];
				nameText.text = DataStorage.me;
				healthText.text = (int)targetHealth + " / " + entityMaxHealth;
				if (targetHealth > entityMaxHealth)
				{
					lastHealth = entityMaxHealth;
					isHeal = false;
					tghealamount = 0;
				}
				targetHealth = lastHealth + HealChange.healAmount - SufferChange.sufferAmount;
				bar.fillAmount = targetHealth / entityMaxHealth;
				bar.color = Color.HSVToRGB(0.2f * (targetHealth / entityMaxHealth), 1, 1);
				// if(Input.GetKeyDown(KeyCode.U)) Heal(30);
				// if(Input.GetKeyDown(KeyCode.I)) Suffer(30);
				if (HealChange.healAmount > tghealamount)
				{
					heal.enabled = false;
					HealChange.healAmount = tghealamount;
					if (SufferChange.sufferAmount >= tgsufferamount)
					{
						lastHealth = targetHealth;
						isHeal = false;
						tghealamount = 0;
					}

				}
				if (SufferChange.sufferAmount > tgsufferamount)
				{
					suffer.enabled = false;
					SufferChange.sufferAmount = tgsufferamount;
					if (HealChange.healAmount >= tghealamount)
					{
						lastHealth = targetHealth;
						isSuffer = false;
						tgsufferamount = 0;
						tghealamount = 0;
					}
				}
				break;
		}
	}
	public static void Heal(float amount)
	{
		tghealamount += amount;
		isHeal = true;
		if (tghealamount != 0)
		{
			if (!staticHeal.enabled || !anim1)
			{
				anim1 = true;
				staticHeal.enabled = true;
				staticHeal.Play("lifeheal", 0, 0);
			}
		}
	}
	public static void Suffer(float amount)
	{
		tgsufferamount += amount;
		isSuffer = true;
		if (!staticSuffer.enabled || !anim2)
		{
			anim2 = true;
			staticSuffer.enabled = true;
			staticSuffer.Play("lifesuffer", 0, 0);
		}
	}
}

