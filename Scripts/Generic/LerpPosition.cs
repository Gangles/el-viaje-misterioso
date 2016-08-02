using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;

public class LerpPosition : MonoBehaviour
{
	public bool smooth = true;
	public Vector3 startPos;
	public Vector3 endPos;
	public float lerpTime = 0.0f;
	public float delayTime = 0.0f;

	void Start ()
	{
		StartCoroutine(WaitLerpPos());
	}

	IEnumerator WaitLerpPos ()
	{
		yield return new WaitForSeconds(delayTime);
		Assert.AreNotEqual(lerpTime, 0.0f);
		float startTime = Time.time;
		while (Time.time - startTime <= lerpTime)
		{
			float lerp = (Time.time - startTime) / lerpTime;
			if (smooth) lerp = Mathf.SmoothStep(0f, 1f, lerp);
			transform.position = Vector3.Lerp(startPos, endPos, lerp);
			yield return 1;
		}
		transform.position = endPos;
		Destroy (this);
	}
}
