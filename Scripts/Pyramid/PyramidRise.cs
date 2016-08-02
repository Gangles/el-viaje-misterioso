using UnityEngine;
using System.Collections;

public class PyramidRise : MonoBehaviour
{
	private bool isRising = false;
	public Transform start;
	public Transform end;
	public Turtle turtle;

	void Awake ()
	{
		transform.position = start.position;
	}

	public void Rise (Collider2D other)
	{
		if (!isRising)
		{
			isRising = true;

			GetComponent<AudioSource>().Play();

			LerpPosition lp = gameObject.AddComponent<LerpPosition>();
			lp.startPos = start.position;
			lp.endPos = end.position;
			lp.delayTime = 0.0f;
			lp.lerpTime = 4.0f;

			CameraShake cs = Camera.main.gameObject.AddComponent<CameraShake>();
			cs.duration = 4.0f;
			cs.intensity = 0.02f;

			turtle.blockTime = Time.time + 4.5f;
			turtle.hideDuration = 4.0f;
		}
	}
}
