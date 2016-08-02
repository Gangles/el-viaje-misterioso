using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class EndGame : MonoBehaviour
{
	void Start ()
	{
		Invoke("DoEndGame", 10f);
	}

	void DoEndGame ()
	{
		if (Application.isWebPlayer || Application.isEditor)
		{
			SceneManager.LoadScene("Begin-1");
		}
		else
		{
			Debug.Log("Quitting to desktop.");
			Application.Quit();
		}
	}
}
