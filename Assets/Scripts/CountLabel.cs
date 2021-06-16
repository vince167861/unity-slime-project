using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountLabel : MonoBehaviour
{
	Animator animator;
	Text countText;

	private void Start()
	{
		countText = GetComponentInChildren<Text>();
		animator = GetComponent<Animator>();
	}

	public void updateCount(int count)
	{
		animator.Play("update");
		countText.text = " X  " + count;
	}
}
