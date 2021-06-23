using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelHandler : MonoBehaviour
{
	void Update()
	{
		if (GameGlobalController.isMenuPrepare || GameGlobalController.isDarking)
		{
			DialogBoxHandler.cbnum = 0;
			Destroy(gameObject);
		}
	}
}
