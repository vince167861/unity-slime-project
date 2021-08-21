#pragma warning disable CS0108
using UnityEngine;

public class ChamberControl : MonoBehaviour
{
	// TODO: Add animation to buttons.
	// TODO: Add drowning player when get stuck.
	public float dx, dy;
	public bool fx, fy;
	private Chamber parent;

	private void Start()
	{
		parent = GetComponentInParent<Chamber>();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Slime"))
		{
			parent.dx = dx;
			parent.dy = dy;
			parent.rigidbody2d.constraints = (fx ? RigidbodyConstraints2D.FreezePositionX : 0) | (fy ? RigidbodyConstraints2D.FreezePositionY : 0) | RigidbodyConstraints2D.FreezeRotation;
		}
	}
	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.CompareTag("Slime"))
		{
			parent.dx = 0;
			parent.dy = 0;
			parent.rigidbody2d.constraints = RigidbodyConstraints2D.FreezeAll;
		}
	}
}
