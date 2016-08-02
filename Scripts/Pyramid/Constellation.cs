using UnityEngine;
using System.Collections;

public class Constellation : MonoBehaviour
{
	private Platformer player;
	public Conversation textBox;
	public GameObject outline;
	public GameObject[] eyes;

	public GameObject arrow, levelLoad;
	public LerpColor overlay;

	public void StartCoyote (Collider2D other)
	{
		if (other.GetComponent<Platformer>())
		{
			player = other.GetComponent<Platformer>();
			player.ForceSettle();
			player.enabled = false;

			SpriteRenderer s = outline.GetComponent<SpriteRenderer>();
			s.color = new Color(1f, 1f, 1f, 0.0f);
			s.enabled = true;

			LerpColor lc = outline.AddComponent<LerpColor>();
			lc.startColor = new Color(1f, 1f, 1f, 0.0f);
			lc.endColor = new Color(1f, 1f, 1f, 0.25f);
			lc.lerpTime = 3.0f;

			foreach (GameObject eye in eyes)
			{
				LerpColor elc = eye.AddComponent<LerpColor>();
				elc.startColor = new Color(1f, 1f, 1f, 1f);
				elc.endColor = new Color(0.0f, 0.875f, 1f, 1f);
				elc.lerpTime = 3.0f;
			}

			Invoke("StartConversation", 3.0f);
		}
	}

	public void StartConversation ()
	{
		Flicker f =outline.GetComponent<Flicker>();
		f.enabled = true;
		f.offset = -1f * Time.time;

		textBox.gameObject.SetActive(true);
		textBox.Initialize();
	}

	public void StartTrain ()
	{
		Music.singleton.fadeOutMusic();
		AudioSource source = GetComponent<AudioSource>();
		source.Play();
		overlay.enabled = true;
		levelLoad.SetActive(true);
	}

	public void ConversationDone ()
	{
		Destroy(arrow);
	}
}
