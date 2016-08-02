using UnityEngine;
using System.Collections;

public class Speaker : MonoBehaviour
{
	private Platformer player;
	public Conversation textBox;
	public bool inTrigger = false;
	public bool returnControl = true;

	void Update ()
	{
		if (inTrigger && Input.GetButtonDown("Jump"))
		{
			textBox.Initialize();
			player.ForceSettle();
			player.enabled = false;
		}
		else if (textBox.gameObject.activeSelf && textBox.done)
		{
			player.overrideJump = false;
			if (returnControl) player.enabled = true;
			textBox.gameObject.SetActive(false);
			Destroy(gameObject);
		}
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		player = other.GetComponent<Platformer>();
		if (player)
		{
			player.overrideJump = true;
			inTrigger = true;
		}
	}

	void OnTriggerExit2D (Collider2D other)
	{
		if (player && other == player)
		{
			player.overrideJump = false;
			inTrigger = false;
		}
	}
}
