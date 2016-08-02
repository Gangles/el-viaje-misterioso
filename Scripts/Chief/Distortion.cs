using UnityEngine;
using System.Collections;

public class Distortion : MonoBehaviour
{
	public Camera useCamera;
	private float startTime = 0f;
	private float yoffset = -6f;

	void Start ()
	{
		startTime = Time.time;
	}

	void Update ()
	{
		float x = Map(-1f, 1f, 0f, useCamera.pixelWidth, Mathf.Cos(Time.time - startTime));
		float y = Map(-1f, 1f, 0f, useCamera.pixelHeight, Mathf.Sin(Time.time - startTime));
		Vector3 pos = useCamera.ScreenToWorldPoint(new Vector3(x, y, 10f));
		yoffset = Mathf.SmoothStep(-8f, 0f, (Time.time - startTime) / 2.0f);
		pos.y += yoffset;
		transform.position = pos;
	}

	private float Map (float a1, float a2, float b1, float b2, float s)
	{
		return b1 + (s-a1)*(b2-b1)/(a2-a1);
	}
}
