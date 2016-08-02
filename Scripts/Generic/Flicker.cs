using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Flicker : MonoBehaviour
{
	private Image image;
	private SpriteRenderer sprite;

	public bool useRandomOffset = false;
	public float offset = 0.0f;
	public float speed = 3.0f;
	public float minAlpha = 0.0f;
	public float maxAlpha = 1.0f;

	void Start()
	{
		image = GetComponent<Image>();
		sprite = GetComponent<SpriteRenderer>();
		if (useRandomOffset)
		{
			offset = Random.Range(-10.0f, 10.0f);
		}
	}

	void Update ()
	{
		float lerp = 0.5f + 0.5f * Mathf.Cos(Time.time * speed + offset);
		float alpha = Mathf.Lerp(minAlpha, maxAlpha, lerp);

		if (image)
		{
			Color ic = image.color;
			ic.a = alpha;
			image.color = ic;
		}
		if (sprite)
		{
			Color sc = sprite.color;
			sc.a = alpha;
			sprite.color = sc;
		}
	}
}
