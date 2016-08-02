using UnityEngine;
using System.Collections;

public class ButterflyWing : MonoBehaviour
{
	void Update ()
	{
		float rot = Mathf.Abs(50.0f * Mathf.Sin(Time.time * 1.5f));
		transform.localRotation = Quaternion.Euler(new Vector3(0, rot, 0));
	}
}
