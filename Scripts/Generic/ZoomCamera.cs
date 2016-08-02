using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;

public class ZoomCamera : MonoBehaviour
{
	public bool smooth = true;
	public float startZoom = 0.0f;
	public float endZoom = 0.0f;
	public float delayTime = 0.0f;
	public float lerpTime = 0.0f;
	public GameObject callback;

	void Start ()
	{
		StartCoroutine(WaitZoomCamera());
	}

	IEnumerator WaitZoomCamera ()
	{
		Assert.AreNotEqual(lerpTime, 0.0f);
		yield return new WaitForSeconds(delayTime);
		
		float startTime = Time.time;
		while (Time.time - startTime <= lerpTime)
		{
			float lerp = (Time.time - startTime) / lerpTime;
			if (smooth) lerp = Mathf.SmoothStep(0f, 1f, lerp);
			float z = Mathf.Lerp(startZoom, endZoom, lerp);
			Camera.main.orthographicSize = z;
			yield return 1;
		}
		Camera.main.orthographicSize = endZoom;
		if (callback)
			callback.SendMessage("ZoomCallback", gameObject);
	}
}
