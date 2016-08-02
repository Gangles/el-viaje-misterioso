using UnityEngine;
using System.Collections;

public class IdleQuit : MonoBehaviour
{
	private static IdleQuit singleton;
	public float idleTime = 0f;

	void Awake()
	{
		// only create this object once, but persist it
		if (!singleton)
		{
			DontDestroyOnLoad(this.gameObject);
			singleton = this;
		}
		else
		{
			Destroy(this.gameObject);
		}
	}

	void Update ()
	{
		float x = Mathf.Abs(Input.GetAxis("Vertical"));
		float y = Mathf.Abs(Input.GetAxis("Horizontal"));
		if (Input.anyKey || x > 0f || y > 0f)
		{
			idleTime = 0f;
		}
		else
		{
			idleTime += Time.deltaTime;
			if (idleTime > 300f && !Application.isWebPlayer)
			{
				Debug.Log("Quitting to desktop.");
				Application.Quit();
			}
		}
	}
}
