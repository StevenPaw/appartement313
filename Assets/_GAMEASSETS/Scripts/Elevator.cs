using System;
using System.Collections;
using System.Collections.Generic;
using _GAMEASSETS.Scripts;
using UnityEngine;
using UnityEngine.UIElements;

public class Elevator : MonoBehaviour
{
    [Header("Target Levels")]
    [SerializeField] private GameObject elevatorUp; //The Elevator when traveling up
    [SerializeField] private GameObject elevatorDown; //The Elevator when traveling down
    
    [Header("Parts of Elevator")]
    [SerializeField] private GameObject elevatorFrame;
    [SerializeField] private GameObject elevatorDoorLeft;
    [SerializeField] private GameObject elevatorDoorRight;
    [SerializeField] private GameObject elevatorBackground;

    [Header("Settings")] 
    [SerializeField] private bool canBeAccessed = true;
    
    //Reference to the player to set animations etc.
    private GameObject player;
    private PlayerController playerController;
    private Animator animator;
    private PlayerController playerInElevator;

    private void Start()
    {
        player = GameObject.FindWithTag(GameTags.PLAYER);
        playerController = player.GetComponent<PlayerController>();
        animator = GetComponent<Animator>();
    }

    public void EnterLeaveElevator(PlayerController player)
    {
        playerController.CanMove = !playerController.CanMove;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (canBeAccessed)
        {
            if (other.gameObject == player)
            {
                animator.SetBool("isOpen", true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (canBeAccessed)
        {
            if (other.gameObject == player)
            {
                animator.SetBool("isOpen", false);
            }
        }
    }
}
