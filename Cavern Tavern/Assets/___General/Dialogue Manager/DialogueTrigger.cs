using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    
    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(this, dialogue);   
    }

    public virtual void EndDialogueEvent() { }
}
