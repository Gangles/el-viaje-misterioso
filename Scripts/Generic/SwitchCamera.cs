using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;

public class SwitchCamera : MonoBehaviour
{
	public Camera startCamera;
	public Camera endCamera;

	private bool isSwitched = false;
	private bool requestSwitch = false;
	private bool isSwitching = false;

	public float lerpTime = 2.0f;
	public float delayTime = 0.0f;
	public bool smooth = true;

	void Update ()
	{
		if (!isSwitching)
		{
			if (requestSwitch && !isSwitched)
			{
				StartCoroutine(WaitSwitchCamera(startCamera, endCamera));
				isSwitched = true;
			}
			else if (!requestSwitch && isSwitched)
			{
				StartCoroutine(WaitSwitchCamera(endCamera, startCamera));
				isSwitched = false;
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.GetComponent<Platformer>())
			requestSwitch = true;
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.GetComponent<Platformer>())
			requestSwitch = false;
	}

	public void ForceSwitch ()
	{
		requestSwitch = true;
	}

	IEnumerator WaitSwitchCamera (Camera start, Camera end)
	{
		isSwitching = true;
		Assert.AreNotEqual(lerpTime, 0.0f);
		yield return new WaitForSeconds(delayTime);

		Camera lerpCamera = (Camera) Instantiate(start);
		float x1 = start.transform.position.x;
		float x2 = end.transform.position.x;
		float y1 = start.transform.position.y;
		float y2 = end.transform.position.y;

		start.gameObject.SetActive(false);
		end.gameObject.SetActive(false);

		float startTime = Time.time;
		while (Time.time - startTime <= lerpTime)
		{
			float lerp = (Time.time - startTime) / lerpTime;
			if (smooth) lerp = Mathf.SmoothStep(0f, 1f, lerp);
			lerpCamera.transform.position = new Vector3(Mathf.Lerp(x1, x2, lerp), Mathf.Lerp(y1, y2, lerp), -10f);
			lerpCamera.orthographicSize = Mathf.Lerp(start.orthographicSize, end.orthographicSize, lerp);
			yield return 1;
		}

		lerpCamera.gameObject.SetActive(false);
		end.gameObject.SetActive(true);
		Destroy(lerpCamera.gameObject);
		isSwitching = false;
	}
}
