using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;

public class GenericTriggerStay : MonoBehaviour
{
	public GameObject callObject;
	public string functionName;

	void OnTriggerStay2D (Collider2D other)
	{
		callObject.SendMessage(functionName, other);
	}
}
