using UnityEngine;

public class Bird : Entity, Attackable
{
    public Bird() : base(150, 1, OnSuffer, OnDie) { }
    public int AttackDamage { get => 1; }

    float moveSpeed = 0.03f; //bird movement speed
    public float delta2 = 0;
    float offset = 0;

    GameObject progressBar;
    Transform fillings;


    // Start is called before the first frame update
    void Start()
    {
        progressBar = transform.Find("Progress Bar").gameObject;
        progressBar.SetActive(false);
        fillings = transform.Find("Progress Bar").Find("Fillings");
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector2(-direction * 0.5f, 0.5f);
        if (health != 150)
        {
            progressBar.SetActive(true);
            progressBar.transform.localScale = new Vector3(-direction, 1, 1);
            fillings.localScale = new Vector3(((float)health) / 150, 1, 1);
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

    void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "bullet":
                Destroy(collision.gameObject);
                Suffer(collision.GetComponent<Attackable>().AttackDamage);
                break;
                
        }
    }

    static void OnSuffer(Entity entity) { entity.GetComponent<Animator>().Play("suffer"); }
    static void OnDie(Entity entity) { entity.GetComponent<Animator>().Play("die"); }

    void DieAnimationEnd()
    {
        Destroy(gameObject);
        GetComponentInParent<EnemySpawnerHandler>().isActive = true;
    }

}
