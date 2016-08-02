using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class Turtle : MonoBehaviour
{
	private Animator animator;
	private float speed = 0.15f;
	public float blockTime = -1.0f;
	public float hideDuration = 0.0f;

	public float sceneStart = 0f;
	public RectTransform message;
	public float goalX = 1000f;

	void Start ()
	{
		sceneStart = Time.time;
		animator = GetComponent<Animator>();
		if (message)
		{
			message.sizeDelta = new Vector2(0f, 35f);
		}
	}
	
	void FixedUpdate ()
	{
		if (hideDuration > 0.0f) hideDuration -= Time.deltaTime;
		animator.SetFloat("HideTime", hideDuration);

		if (transform.position.x >= goalX || hideDuration > 0.0f || Time.time - blockTime < 0.5f)
		{
			animator.SetBool("Walking", false);
		}
		else
		{
			animator.SetBool("Walking", true);

			speed = Mathf.Lerp(0.15f, 0.25f, (Time.time - sceneStart)/5f);
			Vector2 pos = transform.position;
			pos.x += speed * Time.deltaTime;
			transform.position = pos;

			if (message)
			{
				float width = Map(-2f, 2.45f, 0f, 490f, pos.x);
				message.sizeDelta = new Vector2(width, 35f);
			}
		}
	}

	public void PlayerBlocking (Collider2D other)
	{
		if (other.gameObject.GetComponent<Platformer>())
		{
			blockTime = Time.time;
		}
	}

	public void ChooseLevel (Collider2D other)
	{
		if (transform.position.x > 3.2f)
			SceneManager.LoadScene("Pyramid");
		else
			SceneManager.LoadScene("Turtle-1");
	}

	private float Map (float a1, float a2, float b1, float b2, float s)
	{
		return b1 + (s-a1)*(b2-b1)/(a2-a1);
	}
}
