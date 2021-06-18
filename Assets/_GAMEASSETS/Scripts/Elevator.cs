using System;
using System.Collections;
using System.Collections.Generic;
using _GAMEASSETS.Scripts;
using DG.Tweening;
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
    [SerializeField] private Color elevatorLightColor = Color.gray;
    [SerializeField] private int level;
    
    //Reference to the player to set animations etc.
    private GameObject player;
    private PlayerController playerController;
    private Animator animator;
    private SpriteRenderer playerRenderer;

    private Vector3 doorPosition;

    public int Level
    {
        get => level;
        set => level = value;
    }
    
    private void Start()
    {
        player = GameObject.FindWithTag(GameTags.PLAYER);
        playerController = player.GetComponent<PlayerController>();
        animator = GetComponent<Animator>();
        doorPosition = new Vector3(transform.position.x, transform.position.y -.9f, transform.position.z);
        if (!ElevatorManager.AddLevel(this, level))
        {
            Destroy(this.gameObject); //Destroy the elevator if a position is already taken
        }
    }
    
    private void CloseDoors()
    {
        animator.SetBool("isOpen", false);
    }

    private void OpenDoors()
    {
        animator.SetBool("isOpen", true);
    }

    public Vector3 DoorPosition
    {
        get => doorPosition;
        set => doorPosition = value;
    }

    public void EnterLeaveElevator(PlayerController player)
    {
        if (ElevatorManager.PlayerInElevator == null)
        {
            player.CanMove = false;
            playerRenderer = player.SpriteGO.GetComponent<SpriteRenderer>();

            playerRenderer.DOColor(elevatorLightColor, .3f);
            player.transform.DOMove(doorPosition + Vector3.forward * 6, .2f).OnComplete(CloseDoors);
            ElevatorManager.PlayerInElevator = player;
            player.ElevatorInUse = this;
        }
        else if (ElevatorManager.PlayerInElevator == player)
        {
            player.CanMove = true;
            playerRenderer = player.SpriteGO.GetComponent<SpriteRenderer>();
            OpenDoors();
            
            player.transform.DOMove(doorPosition - Vector3.forward * 10, .2f).SetDelay(.2f);
            playerRenderer.DOColor(Color.white, .3f).SetDelay(.2f);
            ElevatorManager.PlayerInElevator = null;
            player.ElevatorInUse = null;
        }
        else
        {
            //TODO: DO Something if Elevator is occupied
        }
    }
    
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (canBeAccessed)
        {
            if (other.gameObject == player)
            {
                OpenDoors();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (canBeAccessed)
        {
            if (other.gameObject == player)
            {
                CloseDoors();
            }
        }
    }
}
