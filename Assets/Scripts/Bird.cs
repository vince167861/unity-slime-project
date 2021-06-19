using UnityEngine;

public class Bird : Entity, Attackable
{
	public Bird() : base("Bird", 150, 1, OnSuffer, OnDie) { }
	public int AttackDamage => 15;

	readonly float moveSpeed = 0.03f; //bird movement speed
	float offset = 0;

	GameObject barbox;
	LifeIndicator fillings;
	//GameObject progressBar;
	//Transform fillings;

	private void Start()
	{
		barbox = transform.Find("Life Indicator").gameObject;
		barbox.SetActive(false);
		fillings = GetComponentInChildren<LifeIndicator>();
		//progressBar = transform.Find("Progress Bar").gameObject;
		//progressBar.SetActive(false);
		//fillings = transform.Find("Progress Bar").Find("Fillings");
	}

	private void Update()
	{
		/*health = lifebarprefab.targethealth;
		Debug.Log(health);*/
		transform.localScale = new Vector2(-direction * 0.5f, 0.5f);
		/*LifeBar.GetComponent<Transform>().localScale = new Vector2(1, 1);*/
		if (health != 150)
		{
			barbox.SetActive(true);
			//barbox.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
			barbox.transform.localScale = new Vector3(-1 * direction, 1, 1);
			//Bar.transform.position +=
			/*progressBar.SetActive(true);
			progressBar.transform.localScale = new Vector3(-direction, 1, 1);
			fillings.localScale = new Vector3(((float)health) / 150, 1, 1);*/
		}
		switch (GameGlobalController.gameState)
		{
			case GameGlobalController.GameState.Lobby:
				Destroy(gameObject);
				break;
			case GameGlobalController.GameState.Playing:
				float newY;
				do newY = Random.Range(-0.03f, 0.03f); while (Mathf.Abs(newY + offset) >= 0.6);
				offset += newY;
				transform.Translate(new Vector2(moveSpeed * direction, newY));
				break;
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		switch (collision.tag)
		{
			case "Slime":
				if (GameGlobalController.gameState == GameGlobalController.GameState.Playing)
					LifeHandler.Suffer(AttackDamage);
				//collision.GetComponent<Entity>().Suffer(AttackDamage);
				break;
		}
	}

	static void OnSuffer(Entity entity)
	{
		entity.GetComponent<Animator>().Play("suffer");
		//lifebarprefab.changeamount(amount);
	}

	static void OnDie(Entity entity)
	{
		entity.GetComponent<Animator>().Play("die");
		entity.direction = 0;
	}

	void DieAnimationEnd()
	{
		Destroy(gameObject);
		GetComponentInParent<EnemySpawnerHandler>().isActive = true;
	}

}
