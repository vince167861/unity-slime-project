using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelBoardHandler : MonoBehaviour
{
    public GameObject UserLevelAmount;
    public TextMeshProUGUI UserLevel;
    public Text UserName;
    float Lneedexp = 0, Nneedexp = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Nneedexp = 10*(float)(Mathf.Pow(Game.chLevel, 1.5f));
        if(Game.chLevel > 1)  Lneedexp = 10*(float)(Mathf.Pow(Game.chLevel - 1, 1.5f));
        UserName.GetComponent<Text>().text = DataStorage.me;
        UserLevel.GetComponent<TextMeshProUGUI>().text = "Lv." + Game.chLevel;
        UserLevelAmount.GetComponent<Image>().fillAmount = (Game.totalexp - Lneedexp) / Nneedexp;
    }
}
