using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryElement : MonoBehaviour {

    public Dialogue dialogue;

    [SerializeField]
    private GameObject StartDialogueButton;

    public void TriggerDialogue () {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        StartDialogueButton.SetActive(false);
    }

    public void TriggerDialogueBoss()
    {
        FindObjectOfType<DialogueManagerBossMenu>().StartDialogue(dialogue);
        StartDialogueButton.SetActive(false);
    }
}

/*

[SerializeField]
private GameObject StartDialogueButton;
StartDialogueButton.SetActive(false);

*/
