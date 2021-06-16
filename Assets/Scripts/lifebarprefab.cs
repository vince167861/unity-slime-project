using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class lifebarprefab : MonoBehaviour
{
    public float totalhealth;
    public float targethealth;
    public float nexthealth;
    public float delta = 0;
    public float speed = 1;
    public Image LifeBar;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (GameGlobalController.gameState)
        {
            case GameGlobalController.GameState.Animation:
            case GameGlobalController.GameState.Shaking:
            case GameGlobalController.GameState.Interval:
            case GameGlobalController.GameState.Playing:
                if(LifeHandler.start)
                {
                    nexthealth = totalhealth;
                    targethealth = totalhealth;
                    delta = 0;
                }
                if(targethealth > 100)
                {
                    nexthealth = totalhealth;
                    targethealth = totalhealth;
                }
                delta += Time.deltaTime;
                if(delta >= 3 && GameGlobalController.gameState == GameGlobalController.GameState.Playing && targethealth < 100)  changeamount(30);
                if(targethealth < nexthealth) Heal(speed);
                if(targethealth > nexthealth)
                {
                    if(nexthealth < 0)
                    {
                        LifeHandler.Suffer(-1/1.5f * nexthealth);
                        nexthealth = 0;
                    }
                    else  Suffer(speed);
                }
                LifeBar.fillAmount = targethealth/totalhealth;
                break;
        }
    }
    public void Heal(float amount)
    {
        targethealth += amount;
        if(targethealth > nexthealth)  targethealth = nexthealth;
    }
    public void Suffer(float amount)
    {
        targethealth -= amount;
        if(targethealth < nexthealth)  targethealth = nexthealth;
    }
    public void changeamount(float amount)
    {
        nexthealth += amount;
        if(amount < 0)
        {
            speed = (targethealth - nexthealth)/30;
            delta = 0;
        }
        else  speed = 1;
    }
}
