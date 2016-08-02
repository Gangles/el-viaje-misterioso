using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Music : MonoBehaviour
{
	private AudioSource audioSource;
	public AudioClip startMusic, chiefMusic, pepperMusic, nightMusic;
	private AudioClip playing = null;
	public static Music singleton;
	private const float MAX_VOLUME = 0.6f;

	void Awake()
	{
		// only create this object once, but persist it
		if (!singleton)
		{
			DontDestroyOnLoad(this.gameObject);
			singleton = this;
			audioSource = GetComponent<AudioSource>();
			playLevelMusic();
		}
		else
		{
			Destroy(this.gameObject);
		}
	}

	void OnLevelWasLoaded(int levelID)
	{
		if (singleton == this) playLevelMusic();
	}

	public void playMusic (string level)
	{
		switch(level)
		{
			case "Begin-1":
			case "Begin-2":
			case "Begin-3":
			case "Begin-4":
			case "Credits-1":
			case "Credits-2":
			case "Credits-3":
				playClip(startMusic);
				break;
			case "Chief":
				playClip(chiefMusic);
				break;
			case "Clouds-1":
			case "Clouds-2":
			case "Recursion":
			case "Butterfly":
			case "SunShatter":
				playClip(pepperMusic);
				break;
			case "Turtle-1":
			case "Turtle-2":
			case "Pyramid":
			case "Coyote":
				playClip(nightMusic);
				break;
			default:
				fadeOutMusic();
				break;
		}
	}

	public void fadeOutMusic ()
	{
		StartCoroutine("FadeOutMusic");
		playing = null;
	}

	private void playLevelMusic ()
	{
		playMusic(SceneManager.GetActiveScene().name);
	}

	private void playClip (AudioClip music, float volume = MAX_VOLUME)
	{
		StopCoroutine("FadeOutMusic");
		if (playing != music)
		{
			playing = music;
			audioSource.volume = volume;
			audioSource.clip = music;
			audioSource.Play();
		}
	}

	IEnumerator FadeOutMusic()
	{
		while (audioSource.volume > 0.01f)
		{
			audioSource.volume = Mathf.Lerp(audioSource.volume, 0.0f, Time.deltaTime * 3);
			yield return 0;
		}
		audioSource.volume = 0.0f;
	}
}