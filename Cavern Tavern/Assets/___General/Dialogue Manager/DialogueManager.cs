using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    private Queue<string> sentences;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;

    public Animator dialogueBoxAnimator;

    private DialogueTrigger host;

    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(DialogueTrigger host, Dialogue dialogue)
    {
        dialogueBoxAnimator.SetBool("IsOpen", true); //Make dialogue box appear

        this.host = host; //Set reference to gameObject speaking

        //Name and sentence
        nameText.text = dialogue.name;
        sentences.Clear();
        foreach(string sentence in dialogue.sentences) sentences.Enqueue(sentence);
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if(sentences.Count == 0) //reach the end of queue
        {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;
    }

    public void EndDialogue()
    {
        dialogueBoxAnimator.SetBool("IsOpen", false);
        EndDialogueEvent();
    }

    public void EndDialogueEvent()
    {
        host.EndDialogueEvent();
    }
}
