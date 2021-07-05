using System;
using System.Collections;
using System.Collections.Generic;
using _GAMEASSETS.Scripts;
using UnityEngine;

public class InteractableNPC : MonoBehaviour
{
    [SerializeField] private Dialogue dialogue;
    private PlayerController playerController;
    private DialogueManager dialogueManager;

    public void TriggerDialog()
    {
        playerController = GameObject.FindGameObjectWithTag(GameTags.PLAYER).GetComponent<PlayerController>();
        dialogueManager = GameObject.FindGameObjectWithTag(GameTags.DIALOGMANAGER).GetComponent<DialogueManager>();
        dialogueManager.StartDialogue(dialogue);
    }
}
