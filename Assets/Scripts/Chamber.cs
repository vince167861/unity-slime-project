using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chamber : MonoBehaviour
{
	public float dx, dy;
	public Rigidbody2D rigidbody2d;

	private void Start()
	{
		rigidbody2d = GetComponent<Rigidbody2D>();
	}

	private void Update()
	{
		rigidbody2d.AddForce(new Vector2(dx, dy));
	}
}
