using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;

public class GenericTrigger : MonoBehaviour
{
	public GameObject callObject;
	public string functionName;

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.GetComponent<Platformer>())
		{
			callObject.SendMessage(functionName, other);
		}
	}
}
