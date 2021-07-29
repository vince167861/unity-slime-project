using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelHandler : MonoBehaviour
{
	void Update()
	{
		if (Game.isMenuPrepare || Game.isDarking)
		{
			DialogBoxHandler.cbnum = 0;
			Destroy(gameObject);
		}
	}
}
