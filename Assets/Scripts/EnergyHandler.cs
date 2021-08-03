using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public static float entityenergy = 100;
    public static float targetenergy = entityenergy;
    public static float nextenergy = entityenergy;
    public static float delta = 0;
    public static float speed = 1;
    Image EBar;

    void Start()
    {
        EBar = GameObject.Find("Energy").GetComponent<Image>();        
    }

    // Update is called once per frame
    void Update()
    {
        switch (Game.gameState)
        {
            case Game.GameState.Animation:
            case Game.GameState.Shaking:
            case Game.GameState.Playing:
                if(MainCharacterHealth.start)
                {
                    nextenergy = entityenergy;
                    targetenergy = entityenergy;
                    delta = 0;
                }
                if(targetenergy > 100)
                {
                    nextenergy = entityenergy;
                    targetenergy = entityenergy;
                }
                delta += Time.deltaTime;
                if(delta >= 3 && Game.gameState == Game.GameState.Playing && targetenergy < 100)  changeamount(1);
                if(targetenergy < nextenergy) EHeal(speed);
                if(targetenergy > nextenergy) ESuffer(speed);
                /*{
                    if(nextenergy < 0)
                    {
                        LifeHandler.Suffer(-1/1.5f * nextenergy);
                        nextenergy = 0;
                    }
                    else  ESuffer(speed);
                }*/
                EBar.fillAmount = targetenergy/entityenergy;
                break;
        }
    }
    public static void EHeal(float amount)
    {
        targetenergy += amount;
        if(targetenergy > nextenergy)  targetenergy = nextenergy;
    }
    public static void ESuffer(float amount)
    {
        targetenergy -= amount;
        if(targetenergy < nextenergy)  targetenergy = nextenergy;
    }
    public static void changeamount(float amount)
    {
        nextenergy += amount;
        if(amount < 0)
        {
            speed = (targetenergy - nextenergy)/30;
            delta = 0;
        }
        else  speed = 1;
    }
}


