using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;

public class LerpScale : MonoBehaviour
{
	public bool smooth = true;
	public float startScale = 1.0f;
	public float endScale = 1.0f;
	public float lerpTime = 0.0f;
	public float delayTime = 0.0f;

	void Start ()
	{
		StartCoroutine(WaitLerpScale());
	}

	IEnumerator WaitLerpScale ()
	{
		yield return new WaitForSeconds(delayTime);
		Assert.AreNotEqual(lerpTime, 0.0f);
		float startTime = Time.time;
		while (Time.time - startTime <= lerpTime)
		{
			float lerp = (Time.time - startTime) / lerpTime;
			if (smooth) lerp = Mathf.SmoothStep(0f, 1f, lerp);
			float scale = Mathf.Lerp(startScale, endScale, lerp);
			transform.localScale = new Vector3(scale, scale, scale);
			yield return 1;
		}
		transform.localScale = new Vector3(endScale, endScale, endScale);
		Destroy (this);
	}
}
