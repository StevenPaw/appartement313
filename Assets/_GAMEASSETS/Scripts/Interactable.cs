using System;
using System.Collections;
using System.Collections.Generic;
using _GAMEASSETS.Scripts;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    [SerializeField] private bool isInteractable;
    [SerializeField] public UnityEvent<PlayerController> onInteractEvent;
    [SerializeField] private GameObject interactionIndicator;
    private PlayerController playerController;

    private void Start()
    {
        playerController = GameObject.FindWithTag(GameTags.PLAYER).GetComponent<PlayerController>();

        if (onInteractEvent == null)
        {
            onInteractEvent = new UnityEvent<PlayerController>();
        }
        onInteractEvent.AddListener(DoInteraction);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isInteractable)
        {
            if (other.CompareTag(GameTags.PLAYER))
            {
                interactionIndicator.SetActive(true);
                playerController.ActiveInteractable = this;
            }
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(GameTags.PLAYER))
        {
            interactionIndicator.SetActive(false);
            playerController.ActiveInteractable = null;
        }
    }

    private void DoInteraction(PlayerController playerController)
    {
        Debug.Log("It was interacted!");
    }
}
