using UnityEngine;
using System.Collections;

public class DialogueLine : MonoBehaviour
{
	public string[] parts;
	public Sprite portrait;
	public string animTrigger;
	public Color color = Color.white;
	public string callback;
	public float interval = 0.02f;
}
