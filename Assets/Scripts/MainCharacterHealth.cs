using UnityEngine;
using UnityEngine.UI;

public class MainCharacterHealth : MonoBehaviour
{
	public static float entityMaxHealth, targetHealth, lastHealth, tghealamount = 0, tgsufferamount = 0, healAmount = 0, sufferAmount = 0;
	public static bool start = true;
	public int situation = 0;
	public Sprite[] icon;
	public Image bar, statusIcon;
	public Text healthText, nameText;

	void Start()
	{
		lastHealth = targetHealth = entityMaxHealth = Slime.instance.defaultHealth;
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
					tghealamount = tgsufferamount = healAmount = sufferAmount = 0;
					start = false;
				}
				switch (situation)
				{
					case 0: Slime.suppression = 1; break;
					case 1: Slime.suppression = 0.4f; break;
				}
				statusIcon.sprite = icon[situation];
				nameText.text = DataStorage.me;
				healthText.text = (int)targetHealth + " / " + entityMaxHealth;
				if (targetHealth > entityMaxHealth)
				{
					lastHealth = entityMaxHealth;
					tghealamount = 0;
				}
				if (tghealamount > 0)
				{
					healAmount += Time.deltaTime * tghealamount * (tghealamount > 30 ? tghealamount / 30 : 1f);
				}
				if (tgsufferamount > 0)
				{
					sufferAmount += Time.deltaTime * tgsufferamount * (tgsufferamount > 30 ? tgsufferamount / 30 : 1f);
				}
				if (healAmount >= tghealamount)
				{
					lastHealth += tghealamount;
					healAmount = tghealamount = 0;
				}
				if (sufferAmount >= tgsufferamount)
				{
					lastHealth -= tgsufferamount;
					sufferAmount = tgsufferamount = 0;
				}
				targetHealth = lastHealth + healAmount - sufferAmount;
				bar.fillAmount = targetHealth / entityMaxHealth;
				bar.color = Color.HSVToRGB(0.2f * (targetHealth / entityMaxHealth), 1, 1);
				break;
		}
	}
	public static void Heal(float amount)
	{
		tghealamount += amount;
	}
	public static void Suffer(float amount)
	{
		tgsufferamount += amount;
	}
}

