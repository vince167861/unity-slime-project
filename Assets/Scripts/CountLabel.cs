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

	/// <summary> Update the count of a count label. </summary>
	/// <param name="count">The count to assign to a count label.</param>
	public void UpdateCount(int count)
	{
		animator.Play("update");
		countText.text = " X  " + count;
	}
}
