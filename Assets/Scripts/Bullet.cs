using UnityEngine;

public class Bullet : MonoBehaviour, IAttackable
{
	public float moveSpeed = 0.3f; //bullet movement speed

	public int AttackDamage => 50;
	
	public GameObject particle;

	void Start()
	{
		if (moveSpeed < 0)
			transform.localScale = new Vector3(-1, 1, 1);
		if (moveSpeed > 0)
			transform.localScale = new Vector3(1, 1, 1);
	}

	// Update is called once per frame
	void Update()
	{
		switch (Game.gameState)
		{
			case Game.GameState.Lobby:
				Destroy(gameObject);
				break;
			case Game.GameState.Playing:
				transform.Translate(moveSpeed, 0, 0);
				break;
		}
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		switch (collision.tag)
		{
			case "bird":
			case "Mushroom":
				collision.GetComponent<Entity>().Suffer(AttackDamage);
				Instantiate(particle).GetComponent<Transform>().position = transform.position;
				Destroy(gameObject);
				break;
			case "Walls":
			case "Ground":
				Destroy(gameObject);
				break;
		}
	}
}
