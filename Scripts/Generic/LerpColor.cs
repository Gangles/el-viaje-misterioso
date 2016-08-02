using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;

public class LerpColor : MonoBehaviour
{
	public bool includeChildren = false;
	public bool smooth = true;
	public Color startColor = Color.white;
	public Color endColor = Color.white;
	public float delayTime = 0.0f;
	public float lerpTime = 0.0f;
	private SpriteRenderer[] sprites;
	private MeshRenderer[] meshes;

	void Start()
	{
		if (includeChildren)
		{
			sprites = GetComponentsInChildren<SpriteRenderer>();
			meshes = GetComponentsInChildren<MeshRenderer>();
		}
		else
		{
			sprites = new SpriteRenderer[1];
			sprites[0] = GetComponent<SpriteRenderer>();
			meshes = new MeshRenderer[1];
			meshes[0] = GetComponent<MeshRenderer>();
		}
		StartCoroutine(WaitLerpColor(startColor, endColor, delayTime, lerpTime));
	}

	IEnumerator WaitLerpColor (Color startColor, Color endColor, float delayTime, float lerpTime)
	{
		Assert.AreNotEqual(lerpTime, 0.0f);
		yield return new WaitForSeconds(delayTime);
		
		float startTime = Time.time;
		while (Time.time - startTime <= lerpTime)
		{
			float lerp = (Time.time - startTime) / lerpTime;
			if (smooth) lerp = Mathf.SmoothStep(0f, 1f, lerp);
			Color c = Color.Lerp(startColor, endColor, lerp);
			SetRenderColor(c);
			yield return 1;
		}
		SetRenderColor(endColor);
		Destroy (this);
	}

	private void SetRenderColor(Color c)
	{
		foreach (SpriteRenderer sr in sprites)
			if (sr) sr.color = c;
		foreach (MeshRenderer mr in meshes)
			if (mr) mr.material.color = c;
	}
}
