using UnityEngine;
using UnityEngine.UI;

public class UserController : MonoBehaviour
{
	Text Input;
	void Start()
	{
		Input = GameObject.Find("Input(U)").GetComponent<Text>();
	}

	public void UserCheck()
	{
		if (Input.text == "BrianGodd" || Input.text == "Vince")
			Game.debugMode = true;
	}
}
