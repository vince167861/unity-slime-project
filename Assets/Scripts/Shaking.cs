using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shaking : MonoBehaviour
{
	Transform camera_T;
	float delta = 0;
	bool dragon = false;
	public GameObject dragonPrefab;
	public static float[] timer = { 0, 0.2f, 0.4f, 0.6f, 0.8f, 0.9f, 1.0f, 1.1f, 1.2f, 1.3f, 1.4f, 1.8f, 2.2f };

	void Start()
	{
		camera_T = Camera.main.GetComponent<Transform>();
	}

	// Update is called once per frame
	void Update()
	{
		switch (Game.gameState)
		{
			case Game.GameState.Shaking:
				this.delta += Time.deltaTime;
				if (this.delta >= timer[12])
				{
					Game.gameState = Game.GameState.Dialog;
					this.delta = 0;
				}
				if (this.delta >= timer[10] && this.delta <= timer[11])
				{
					if (!dragon)
					{
                        MainCameraHandler.PlayEntityClip(1);
						Instantiate(dragonPrefab).GetComponent<Transform>().position = new Vector3(3.1f, 91, 0);
						TDragonController.levelstory1 = true;
						dragon = true;
					}
					MainCameraHandler.targetPosition = new Vector3(40, 58, -10);
				}
				for (int i = 0; i <= 9; i++)
				{
					if (this.delta <= timer[i + 1])
					{
						MainCameraHandler.targetPosition = camera_T.position + new Vector3(0, ((i % 2) * -2 + 1), 0);
						break;
					}
				}
				break;
		}

	}
}
