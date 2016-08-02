using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class TriggerLoad : MonoBehaviour
{
	public string level;
	public GameObject destroy;
	private AsyncOperation async;

	void Start ()
	{
		async = SceneManager.LoadSceneAsync(level, LoadSceneMode.Additive);
		async.allowSceneActivation = false;
	}
	
	void Update ()
	{
		if (async.isDone)
		{
			Music.singleton.playMusic(level);
			foreach (Transform child in destroy.transform)
			{
				child.gameObject.SetActive(false);
			}
			Destroy(destroy);
			Destroy(gameObject);
		}
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		Platformer player = other.GetComponent<Platformer>();
		if (player)
		{
			player.ForceSettle();
			player.enabled = false;
			Destroy(player);
			DoLoadLevel();
		}
	}

	void DoLoadLevel ()
	{
		async.allowSceneActivation = true;
	}
}
