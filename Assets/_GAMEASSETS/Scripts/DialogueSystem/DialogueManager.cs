using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private GameObject dialogPanel;
    [SerializeField] private Image npcImage;
    [SerializeField] private Sprite placeholderImage;
    private Queue<string> sentences;

    private void Start()
    {
        sentences = new Queue<string>();
        dialogPanel.SetActive(false);
    }

    public void StartDialogue(Dialogue dialogue)
    {
        Debug.Log("Starting Dialog with " + dialogue.name);
        
        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        
        dialogPanel.SetActive(true);
        nameText.text = dialogue.name;

        if (dialogue.npcSprite)
        {
            npcImage.sprite = dialogue.npcSprite;
        }
        else
        {
            npcImage.sprite = placeholderImage;
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;
    }

    private void EndDialogue()
    {
        Debug.Log("End of conversation");
        dialogPanel.SetActive(false);
    }
}
