﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManagerBossMenu : MonoBehaviour {

    public Text nameText;
    public Text dialogueText;

    public Animator animator;

    private Queue<string> sentences;

    [SerializeField]
    private GameObject continueButton;
    [SerializeField]
    private GameObject quitButton;
    [SerializeField]
    private GameObject mainMenuButton;

    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue (Dialogue dialogue)
    {
        animator.SetBool("IsOpen", true);

        nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence ()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        else if (sentences.Count == 1)
        {
            continueButton.SetActive(false);
            mainMenuButton.SetActive(true);
            quitButton.SetActive(true);
        }
        
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence (string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }
    void EndDialogue()
    {
        animator.SetBool("IsOpen", false);
    }

}

   