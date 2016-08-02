using UnityEngine;
using System.Collections;

public class FloatAround : MonoBehaviour
{
	private Vector3 centerPos;
	public float speed = 0.8f;
	public float scale = 0.05f;
	public float time = 0f;

	void Start ()
	{
		centerPos = transform.position;
	}

	void Update ()
	{
		// move around centerpos with a little wobble
		float s = Map(0f, 1f, speed/4, speed, Mathf.Abs(Mathf.Sin(Time.time)));
		time += Time.deltaTime * s;
		float x = centerPos.x + scale * Mathf.Cos(time);
		float y = centerPos.y + scale * Mathf.Sin(time);
		transform.position = new Vector3(x, y, 0f);
	}

	private float Map (float a1, float a2, float b1, float b2, float s)
	{
		return b1 + (s-a1)*(b2-b1)/(a2-a1);
	}
}
