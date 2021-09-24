#pragma warning disable CS0108
using UnityEngine;

public class ChamberControl : MonoBehaviour
{
	// TODO: Add animation to buttons.
	// TODO: Add drowning player when get stuck.
	public float dx, dy;
	public bool fx, fy, acceptBullet = false;
  public Sprite[] buttonSprite;
  public Chamber parent;
  SpriteRenderer renderer;

  private void Start()
	{
		if (parent == null) parent = GetComponentInParent<Chamber>();
    renderer = GetComponent<SpriteRenderer>();
  }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Slime") || (collision.CompareTag("bullet")) && acceptBullet)
		{
			parent.dx = dx;
			parent.dy = dy;
			parent.rigidbody2d.constraints = (fx ? RigidbodyConstraints2D.FreezePositionX : 0) | (fy ? RigidbodyConstraints2D.FreezePositionY : 0) | RigidbodyConstraints2D.FreezeRotation;
      renderer.sprite = buttonSprite[1];
    }
	}
	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.CompareTag("Slime"))
		{
			parent.dx = 0;
			parent.dy = 0;
			parent.rigidbody2d.constraints = RigidbodyConstraints2D.FreezeAll;
      renderer.sprite = buttonSprite[0];
		}
	}
}
