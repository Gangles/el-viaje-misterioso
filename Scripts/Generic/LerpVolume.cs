using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;

public class LerpVolume : MonoBehaviour
{
	private AudioSource audioSource;
	public bool smooth = true;
	public float startVolume = 1.0f;
	public float endVolume = 1.0f;
	public float lerpTime = 0.0f;
	public float delayTime = 0.0f;

	void Start ()
	{
		audioSource = GetComponent<AudioSource>();
		StartCoroutine(WaitLerpVolume());
	}

	IEnumerator WaitLerpVolume ()
	{
		yield return new WaitForSeconds(delayTime);
		Assert.AreNotEqual(lerpTime, 0.0f);
		float startTime = Time.time;
		while (Time.time - startTime <= lerpTime)
		{
			float lerp = (Time.time - startTime) / lerpTime;
			if (smooth) lerp = Mathf.SmoothStep(0f, 1f, lerp);
			audioSource.volume = Mathf.Lerp(startVolume, endVolume, lerp);
			yield return 1;
		}
		audioSource.volume = endVolume;
		Destroy (this);
	}
}
