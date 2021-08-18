using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickHanlder : MonoBehaviour
{
	void Update()
	{
		switch (Game.gameState)
		{
			case Game.GameState.End:
				Destroy(gameObject);
				break;
		}
	}
}
