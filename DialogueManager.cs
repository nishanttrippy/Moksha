using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

	public Text nameText;
	public Text dialogueText;

	public Animator animator;

	public Queue<string> sentences;

	bool alternateNames = true;
	Dialogue charDialogue;

	// Use this for initialization
	void Start()
	{
		sentences = new Queue<string>();
	}

	public void StartDialogue(Dialogue dialogue)
	{
		animator.SetBool("IsOpen", true);
		charDialogue = dialogue;

		sentences.Clear();

		foreach (string sentence in dialogue.sentences)
		{
			sentences.Enqueue(sentence);
		}

		DisplayNextSentence();
	}

	public void DisplayNextSentence()
	{
		if(alternateNames)
        {
			nameText.text = charDialogue.NPCname;
			alternateNames = !alternateNames;
		}
		else
        {
			nameText.text = charDialogue.playerName;
			alternateNames = !alternateNames;
		}

		if (sentences.Count == 0)
		{
			EndDialogue();
			return;
		}

		string sentence = sentences.Dequeue();
		StopAllCoroutines();
		StartCoroutine(TypeSentence(sentence));
	}

	IEnumerator TypeSentence(string sentence)
	{
		dialogueText.text = "";
		foreach (char letter in sentence.ToCharArray())
		{
			dialogueText.text += letter;
			yield return new WaitForSeconds(0.05f);
		}
	}

	void EndDialogue()
	{
		animator.SetBool("IsOpen", false);
	}

}