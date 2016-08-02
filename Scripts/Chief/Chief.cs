using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Chief : MonoBehaviour
{
	private AsyncOperation async;
	private AudioSource source;

	public GameObject distortion;
	public AudioClip rattle, gulp;

	void Start ()
	{
		source = GetComponent<AudioSource>();
		async = SceneManager.LoadSceneAsync("Clouds-1");
		async.allowSceneActivation = false;
	}

	public void PlayRattle ()
	{
		source.clip = rattle;
		source.PlayDelayed(0.0f);
	}

	public void FadeOutMusic ()
	{
		Music.singleton.fadeOutMusic();
		source.clip = gulp;
		source.PlayDelayed(0.0f);
	}

	public void PlayDistortion ()
	{
		Music.singleton.playMusic("Clouds-1");

		string graphics = SystemInfo.graphicsDeviceVersion;
		if (graphics.Contains("OpenGL"))
		{
			// this shader only works with OpenGL
			distortion.SetActive(true);
		}
		else
		{
			GameObject g = Camera.current.gameObject;
			g.GetComponent<SpinCamera>().enabled = true;
		}
	}

	public void ConversationDone ()
	{
		SwitchCamera sc = GetComponent<SwitchCamera>();
		sc.enabled = true;
		sc.ForceSwitch();
		Invoke ("SwitchScenes", 2);
	}

	public void SwitchScenes ()
	{
		async.allowSceneActivation = true;
	}
}
