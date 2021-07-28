using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonWarriorHandler : MonoBehaviour
{
	void Attacking()
	{
		GetComponentInChildren<ParticleSystem>().Play();
	}

	void noAttack()
	{
		GetComponentInChildren<ParticleSystem>().Stop();
	}
}
