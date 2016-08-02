using UnityEngine;
using System.Collections;

public class SunBridge : MonoBehaviour
{
    void OnTriggerEnter2D (Collider2D other)
    {
    	if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
    	{
    		other.attachedRigidbody.isKinematic = true;
    	}
    }
}
