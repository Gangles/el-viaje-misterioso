using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class TriggerMusicFade : MonoBehaviour
{
	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.GetComponent<Platformer>())
		{
			Music.singleton.fadeOutMusic();
			Destroy(this);
		}
	}
}
