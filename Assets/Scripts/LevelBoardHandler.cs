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
        Lneedexp = 10*(Mathf.Pow(GameGlobalController.chLevel,1.5f));
    }

    // Update is called once per frame
    void Update()
    {
        UserName.GetComponent<TextMeshProUGUI>().text = LevelVarity.me;
        UserLevel.GetComponent<TextMeshProUGUI>().text = "Lv." + GameGlobalController.chLevel;
        UserLevelAmount.GetComponent<Image>().fillAmount = ((GameGlobalController.totalexp) % Lneedexp) / Lneedexp;
    }
}
