using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicControllerHandler : MonoBehaviour
{
    public Scrollbar Music_C;
    public Sprite[] Music = new Sprite[2];
    float volume = 0;
    // Start is called before the first frame update
    void Update()
    {
        if(MainCameraHandler.prevolume == 0)
            gameObject.GetComponent<Image>().sprite = Music[1];
        else
            gameObject.GetComponent<Image>().sprite = Music[0];
    }
    public void MusicVolume()
    {
        if(MainCameraHandler.prevolume > 0)
        {
            volume = MainCameraHandler.prevolume;
            MainCameraHandler.prevolume = 0;
        }
        else{
            MainCameraHandler.prevolume = volume;
        }
    }
    public void Musicchange()
    {
        MainCameraHandler.prevolume = Music_C.value;
    }
    public void Instruction()
    {
        MainCameraHandler.allSound = 3;
        GameGlobalController.gameState = GameGlobalController.GameState.Instruction;
    }
}
