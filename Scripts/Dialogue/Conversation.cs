using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Conversation : MonoBehaviour
{
	public Image portraitBox, arrow;
	public Text textbox;
	public GameObject speaker;
	public Transform dialogueParent;

	public DialogueLine[] dialogue;
	public int line = 0;
	public int part = 0;
	public int letter = 0;

	public bool done = false;
	private bool initialized = false;
	private bool paused = false;
	private bool updateBox = true;
	private float lastChar;

	void Start ()
	{
		textbox.text = "";
		GetComponent<Image>().enabled = false;
		textbox.enabled = false;
		portraitBox.enabled = false;
		arrow.enabled = false;
	}
	
	void Update ()
	{
		if (initialized && !done)
		{
			if (!paused && line < dialogue.Length)
			{
				if (updateBox)
				{
					portraitBox.sprite = dialogue[line].portrait;
					textbox.text = "";
					textbox.color = dialogue[line].color;
					updateBox = false;

					string trigger = dialogue[line].animTrigger;
					if (trigger.Length > 0)
						speaker.GetComponent<Animator>().SetTrigger(trigger);
					if (dialogue[line].callback.Length > 0)
						speaker.SendMessage(dialogue[line].callback);
				}

				if (Time.time - lastChar > dialogue[line].interval)
				{
					textbox.text += dialogue[line].parts[part][letter];
					lastChar = Time.time;
					IncrementLetter();
				}
			}

			if (paused && Input.GetButtonDown("Jump"))
			{
				if (line >= dialogue.Length)
				{
					if (speaker) speaker.SendMessage("ConversationDone");
					done = true;
				}
				else
				{
					paused = false;
				}
			}

			arrow.enabled = paused;
		}
	}

	public void Initialize ()
	{
		if (!initialized)
		{
			initialized = true;

			int i = 0;
			dialogue = new DialogueLine[dialogueParent.childCount];
			foreach(var child in dialogueParent.Cast<Transform>().OrderBy(t=>t.name))
			{
				dialogue[i] = child.gameObject.GetComponent<DialogueLine>();
				++i;
			}

			lastChar = Time.time;
			updateBox = true;

			GetComponent<Image>().enabled = true;
			textbox.enabled = true;
			portraitBox.enabled = true;
			arrow.enabled = true;
		}
	}

	private void IncrementLetter()
	{
		++letter;
		if (letter >= dialogue[line].parts[part].Length)
		{
			paused = true;
			letter = 0;
			textbox.text += "\n";
			IncrementPart();
		}
	}

	private void IncrementPart()
	{
		++part;
		if (part >= dialogue[line].parts.Length)
		{
			part = 0;
			IncrementLine();
		}
	}

	private void IncrementLine()
	{
		++line;
		updateBox = true;
	}
}
