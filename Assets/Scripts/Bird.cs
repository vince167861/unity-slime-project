using UnityEngine;

public class Bird : Entity, Attackable
{
	public Bird() : base("Bird", 150, 1, OnSuffer, OnDie) { }
	public int AttackDamage => 15;
	public bool isWall = false;

	readonly float moveSpeed = 0.03f; //bird movement speed
	float offset = 0;
	public GameObject effect;

	//GameObject progressBar;

	private void Start()
	{
		GetComponent<Animator>().Play("colliderchange");
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
		switch (Game.gameState)
		{
			case Game.GameState.Lobby:
				Destroy(gameObject);
				break;
			case Game.GameState.Playing:
				
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
				if (Game.gameState == Game.GameState.Playing)
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
		Game.moneycount += 20;
		Game.expcount += 2.5f;
		Destroy(gameObject);
		GetComponentInParent<EnemySpawnerHandler>().isActive = true;
	}

	void DieEffectStart()
	{
		Instantiate(effect).GetComponent<Transform>().position = this.transform.position;
	}

	void WallK()
	{
		isWall = true;
	}
}
