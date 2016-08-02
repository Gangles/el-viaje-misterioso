using UnityEngine;
using System.Collections;

public class SpinCamera : MonoBehaviour
{
	public Camera useCamera;
	private Transform cameraTransform;
	public float startTime = 0f;

	void Start ()
	{
		cameraTransform = useCamera.gameObject.transform;
		if (startTime <= 0f) startTime = Time.time;
	}
	
	void Update ()
	{
		float angle = Map(-1f, 1f, -15, 15f, Mathf.Sin(startTime - Time.time));
		cameraTransform.rotation = Quaternion.Euler(0f, 0f, angle);
	}

	private float Map (float a1, float a2, float b1, float b2, float s)
	{
		return b1 + (s-a1)*(b2-b1)/(a2-a1);
	}
}
