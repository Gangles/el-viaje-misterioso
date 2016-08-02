using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class TimeLoad : MonoBehaviour
{
	public string level;
	public float timer = -1f;
	private AsyncOperation async;

	void Start ()
	{
		async = SceneManager.LoadSceneAsync(level);
		async.allowSceneActivation = false;
		Invoke("DoLoadLevel", timer);
	}
	
	void DoLoadLevel ()
	{
		async.allowSceneActivation = true;
	}
}
