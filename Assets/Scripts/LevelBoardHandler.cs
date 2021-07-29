using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelBoardHandler : MonoBehaviour
{
    public Image UserLevelAmount;
    public TextMeshProUGUI UserName, UserLevel;
    float Lneedexp = 0;
    // Start is called before the first frame update
    void Start()
    {
        Lneedexp = 10*(Mathf.Pow(Game.chLevel,1.5f));
    }

    // Update is called once per frame
    void Update()
    {
        UserName.GetComponent<TextMeshProUGUI>().text = LevelVarity.me;
        UserLevel.GetComponent<TextMeshProUGUI>().text = "Lv." + Game.chLevel;
        UserLevelAmount.GetComponent<Image>().fillAmount = ((Game.totalexp) % Lneedexp) / Lneedexp;
    }
}
