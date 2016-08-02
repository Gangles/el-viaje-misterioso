using UnityEngine;
using System.Collections;

public class PlayMovie : MonoBehaviour {
	public MovieTexture movTexture;
	public Color color;
	
	void Start()
	{
		Renderer render = GetComponent<Renderer>();
		render.material.mainTexture = movTexture;
		render.material.color = color;
		movTexture.loop = true;
		movTexture.Play();
	}
}