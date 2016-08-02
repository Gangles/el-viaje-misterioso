using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;

public class Butterfly : MonoBehaviour
{
	private bool startSwoop = false;
	private bool startFlyAway = false;
	private bool isMoving = false;

	private Vector3 swoopStart, swoopEnd;
	private Vector3 flyAwayStart, flyAwayEnd;

	void Awake ()
	{
		swoopStart = gameObject.transform.position;
		swoopEnd = swoopStart + new Vector3(10.0f, 0.5f, 0.0f);
		flyAwayStart = new Vector3(4.0f, 1.5f, 0.0f);
		flyAwayEnd = new Vector3(-1.8f, 1.6f, 0.0f);
	}

	public void TriggerSwoop (Collider2D other)
	{
		if (!startSwoop)
		{
			startSwoop = true;
			StartCoroutine(MoveToPos(swoopStart, swoopEnd, 3.0f));
			StartCoroutine(WaitFlyAway());
			Invoke("SwoopSound", 0.5f);
		}
	}

	public void TriggerFlyAway (Collider2D other)
	{
		startFlyAway = true;
	}

	public void SwoopSound ()
	{
		GetComponent<AudioSource>().Play();
		LerpVolume lv = gameObject.AddComponent<LerpVolume>();
		lv.endVolume = 0f;
		lv.delayTime = 0.25f;
		lv.lerpTime = 2.5f;
	}

	IEnumerator WaitFlyAway ()
	{
		while (!startFlyAway || isMoving) yield return 1;
		transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, 30.0f));
		transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
		StartCoroutine(MoveToPos(flyAwayStart, flyAwayEnd, 8.0f));

		LerpScale ls = gameObject.AddComponent<LerpScale>();
		ls.startScale = 0.1f;
		ls.endScale = 0.02f;
		ls.delayTime = 4.0f;
		ls.lerpTime = 4.0f;

		LerpColor lc = gameObject.AddComponent<LerpColor>();
		lc.includeChildren = true;
		lc.smooth = true;
		lc.endColor = new Color(0.255f, 0.051f, 0.306f, 1.0f);
		lc.delayTime = 4.0f;
		lc.lerpTime = 4.0f;
	}

	IEnumerator MoveToPos (Vector3 startPos, Vector3 endPos, float moveTime)
	{
		isMoving = true;
		Assert.AreNotEqual(moveTime, 0.0f);
		float startTime = Time.time;
		while (Time.time - startTime <= moveTime)
		{
			float lerp = (Time.time - startTime) / moveTime;
			transform.position = Vector3.Lerp(startPos, endPos, lerp);
			yield return 1;
		}
		transform.position = endPos;
		isMoving = false;
	}
}
