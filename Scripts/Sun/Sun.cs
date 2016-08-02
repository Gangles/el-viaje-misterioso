using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;

public class Sun : MonoBehaviour
{
	public GameObject player;
	public GameObject sky;
	public GameObject[] dirts;
	public ShowHide hint;

	private bool sunShattered = false;
	private float distance = 4.5f;
	private float direction = 1.0f;
	private float directionHold = 0.0f;
	private int directionChanges = 0;

	private float startTime = 0.0f;
	private bool showHint = false;
	
	void Start()
	{
		startTime = Time.time;
	}

	void Update ()
	{
		if (!sunShattered)
		{
			float h = Input.GetAxisRaw("Horizontal");
			if (Mathf.Abs(h) > 0.0f)
			{
				float newDir = Mathf.Sign(h);
				if (newDir != direction)
				{
					if (directionHold > 0.1f && directionHold < 2f)
						++directionChanges;
					else
						directionChanges = 0;
					directionHold = Time.deltaTime;
					direction = newDir;
				}
				else
				{
					directionHold += Time.deltaTime;
				}
			}

			if (direction < 0.0f && directionHold > 0.2f && directionChanges > 3)
			{
				sunShattered = true;

				if (showHint) hint.Hide(null);

				Music.singleton.fadeOutMusic();
				GetComponent<AudioSource>().Play();

				LerpColor l = gameObject.AddComponent<LerpColor>();
				l.endColor = new Color(1.0f, 1.0f, 1.0f, 0.0f);
				l.lerpTime = 0.2f;

				LerpColor ls = sky.AddComponent<LerpColor>();
				ls.endColor = new Color(1.0f, 1.0f, 1.0f, 0.0f);
				ls.lerpTime = 0.2f;

				foreach( GameObject dirt in dirts )
				{
					LerpColor ld = dirt.AddComponent<LerpColor>();
					ld.endColor = new Color(0.5f, 0.5f, 0.5f, 1.0f);
					ld.lerpTime = 0.2f;
				}

				float delay = 0.0f;
				foreach( Transform child in transform )
				{
					StartCoroutine(ShatterPiece(child.gameObject, delay));
					delay += Random.Range(0.0f, 0.02f);
				}
			}
		}

		if (!sunShattered && !showHint && Time.time - startTime > 30.0f)
		{
			showHint = true;
			hint.Show(null);
		}

		if (!sunShattered)
		{
			Vector3 pos = transform.position;
			distance = Mathf.Abs(player.transform.position.x - transform.position.x);
			pos.y = Mathf.Lerp(1.8f, 0.75f, distance / 4.5f);
			transform.position = pos;
		}
	}

	IEnumerator ShatterPiece ( GameObject piece, float delayTime )
	{
		piece.SetActive(true);
		yield return new WaitForSeconds(delayTime);
		piece.GetComponent<Rigidbody2D>().isKinematic = false;
	}
}
