using UnityEngine;
using System.Collections;

public class ShowHide : MonoBehaviour
{
	private LerpColor lc;
	private SpriteRenderer sprite;
	private MeshRenderer mesh;
	public bool startHidden;

	void Start ()
	{
		sprite = GetComponent<SpriteRenderer>();
		mesh = GetComponent<MeshRenderer>();

		if (startHidden)
		{
			Color start = new Color(1f, 1f, 1f, 0f);
			if (sprite) sprite.color = start;
			if (mesh) mesh.material.color = start;
		}
	}

	public void Show (Collider2D other = null)
	{
		if (lc) Destroy(lc);
		lc = gameObject.AddComponent<LerpColor>();
		lc.startColor = CurrentColor();
		lc.endColor = new Color(1f, 1f, 1f, 1f);
		lc.lerpTime = 1.0f;
	}

	public void Hide (Collider2D other = null)
	{
		if (lc) Destroy(lc);
		lc = gameObject.AddComponent<LerpColor>();
		lc.startColor = CurrentColor();
		lc.endColor = new Color(1f, 1f, 1f, 0f);
		lc.lerpTime = 1.0f;
	}

	private Color CurrentColor ()
	{
		if (sprite) return sprite.color;
		else if (mesh) return mesh.material.color;
		else return new Color(1f, 1f, 1f, startHidden? 0f : 1f);
	}
}
