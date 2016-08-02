using UnityEngine;
using System.Collections;

public class FadeTitle : MonoBehaviour
{
	private bool fadeOut = false;

	void Update ()
	{
		if (Time.time > 1.0f && !fadeOut)
		{
			if (Input.anyKey || Mathf.Abs(Input.GetAxis("Horizontal")) > 0f)
			{
				fadeOut = true;

				Color current = GetComponent<SpriteRenderer>().color;
				LerpColor lc = gameObject.AddComponent<LerpColor>();
				lc.startColor = current;
				current.a = 0.0f;
				lc.endColor = current;
				lc.delayTime = 0.5f;
				lc.lerpTime = 1.5f;
			}
		}
	}
}
