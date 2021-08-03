using UnityEngine;
using UnityEngine.UI;

public class MainCharacterHealth : MonoBehaviour
{
	public static float entityHealth = 100, targetHealth = entityHealth, lastHealth = entityHealth;
	//public static float healamount = 0;
	//public float sufferamount = 0;
	public static float tghealamount = 0, tgsufferamount = 0;
	public static bool isSuffer, isHeal = false, anim1, anim2 = false, start = true;
	public int situation = 0;
	public Sprite[] icon;
	public Animator heal, suffer;
	public Image bar, statusIcon;
	public Text healthText, nameText;

	void Start()
	{
	}

	void Update()
	{
		switch (Game.gameState)
		{
			case Game.GameState.Animation:
			case Game.GameState.Shaking:
			case Game.GameState.Playing:
				if (start)
				{
					situation = 0;
					entityHealth = Mathf.Round(1002 - 0.092f * Mathf.Pow(Game.chLevel - 100, 2));
					lastHealth = targetHealth = entityHealth;
					tghealamount = tgsufferamount = 0;
					HealChange.healAmount = 0;
					SufferChange.sufferAmount = 0;
					isHeal = isSuffer = anim1 = anim2 = start = false;
				}
				switch (situation)
				{
					case 0:
						Slime.suppression = 1;
						break;
					case 1:
						Slime.suppression = 0.4f;
						break;
				}
				if (tghealamount > 30) heal.speed = tghealamount / 30;
				else heal.speed = 1;
				if (tgsufferamount > 30) suffer.speed = tgsufferamount / 30;
				else heal.speed = 1;
				statusIcon.sprite = icon[situation];
				nameText.text = LevelVarity.me;
				healthText.text = (int)targetHealth + " / " + entityHealth;
				if (targetHealth > entityHealth)
				{
					lastHealth = entityHealth;
					isHeal = false;
					tghealamount = 0;
				}
				targetHealth = lastHealth + HealChange.healAmount - SufferChange.sufferAmount;
				bar.fillAmount = targetHealth / entityHealth;
				bar.color = Color.HSVToRGB(0.2f * (targetHealth / entityHealth), 1, 1);
				//if(Input.GetKeyDown(KeyCode.U))  Heal(30);
				//if(Input.GetKeyDown(KeyCode.I))  Suffer(30);
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
	public void Heal(float amount)
	{
		Slime.instance.Healanim();
		tghealamount += amount;
		isHeal = true;
		if (tghealamount != 0)
		{
			if (!heal.enabled || !anim1)
			{
				anim1 = true;
				heal.enabled = true;
				heal.Play("lifeheal", 0, 0);
			}
		}
	}
	public void Suffer(float amount)
	{
		Slime.ImmuneOn(Slime.instance);
		tgsufferamount += amount;
		isSuffer = true;
		if (!suffer.enabled || !anim2)
		{
			anim2 = true;
			suffer.enabled = true;
			suffer.Play("lifesuffer", 0, 0);
		}
	}
}

