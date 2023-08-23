using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SageAshta : MonoBehaviour
{
    public Transform playerTransform;
    public DialogueTrigger dialogueTrig;
    public DialogueManager dialogueMgr;
    bool dialogueTriggered = false;

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(transform.position, playerTransform.position) <= 2.5f && !dialogueTriggered)
        {
            dialogueTrig.TriggerDialogue();
            dialogueTriggered = true;
        }

        if (Input.GetKeyDown(KeyCode.Return) && dialogueMgr.sentences.Count >= 0)
        {
            dialogueMgr.DisplayNextSentence();
        }
    }
}
