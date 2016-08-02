using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
	public Camera useCamera;
	public float duration = 0.0f;
	public float intensity = 0.01f;

	void Start ()
	{
		if (!useCamera) useCamera = Camera.main;
		StartCoroutine(WaitShakeCamera());
	}

	IEnumerator WaitShakeCamera ()
	{
		Vector3 originalPos = useCamera.gameObject.transform.localPosition;
		float startTime = Time.time;
		while (Time.time - startTime <= duration)
		{
			Vector2 delta = Random.insideUnitCircle * intensity;
			Vector3 newPos = originalPos + new Vector3(delta.x, delta.y, 0f);
			useCamera.gameObject.transform.localPosition = newPos;
			yield return 10;
		}
		useCamera.gameObject.transform.localPosition = originalPos;
		Destroy (this);
	}
}
