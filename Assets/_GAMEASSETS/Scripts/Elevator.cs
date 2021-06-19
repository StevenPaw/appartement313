using System;
using System.Collections;
using System.Collections.Generic;
using _GAMEASSETS.Scripts;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UIElements;

public class Elevator : MonoBehaviour
{
    [Header("Parts of Elevator")]
    [SerializeField] private GameObject elevatorFrame;
    [SerializeField] private GameObject elevatorDoorLeft;
    [SerializeField] private GameObject elevatorDoorRight;
    [SerializeField] private GameObject elevatorBackground;

    [Header("UI")] 
    [SerializeField] private GameObject buttonUp;
    [SerializeField] private GameObject buttonDown;
    [SerializeField] private Canvas buttonCanvas;
    
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
    
    public Vector3 DoorPosition
    {
        get => doorPosition;
        set => doorPosition = value;
    }
    
    private void Start()
    {
        buttonCanvas.worldCamera = Camera.main;
        //player = GameObject.FindWithTag(GameTags.PLAYER);
        //playerController = player.GetComponent<PlayerController>();
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

    public void EnterLeaveElevator(PlayerController player)
    {
        if (ElevatorManager.PlayerInElevator == null)
        {
            player.CanMove = false;
            player.ElevatorInUse = this;
            player.MoveDirection = 0;
            ElevatorManager.PlayerInElevator = player;

            playerRenderer = player.SpriteGO.GetComponent<SpriteRenderer>();

            playerRenderer.DOColor(elevatorLightColor, .3f);
            player.transform.DOMove(doorPosition + Vector3.forward * 1.5f, .2f).OnComplete(CloseDoors);

            if (ElevatorManager.GetUpElevator(Level) != this)
            {
                buttonUp.SetActive(true);
            }
            if (ElevatorManager.GetDownElevator(Level) != this)
            {
                buttonDown.SetActive(true);
            }
        }
        else if (ElevatorManager.PlayerInElevator == player)
        {
            player.CanMove = true;
            player.ElevatorInUse = null;
            ElevatorManager.PlayerInElevator = null;
            
            playerRenderer = player.SpriteGO.GetComponent<SpriteRenderer>();
            
            OpenDoors();
            player.transform.DOMove(doorPosition - Vector3.forward * 1f, .2f).SetDelay(.2f);
            playerRenderer.DOColor(Color.white, .3f).SetDelay(.2f);

            buttonUp.SetActive(false);
            buttonDown.SetActive(false);
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
            if (other.CompareTag(GameTags.PLAYER))
            {
                if (ElevatorManager.PlayerInElevator == null)
                {
                    OpenDoors();
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (canBeAccessed)
        {
            if (other.CompareTag(GameTags.PLAYER))
            {
                if (ElevatorManager.PlayerInElevator == null)
                {
                    CloseDoors();
                    
                    buttonUp.SetActive(false);
                    buttonDown.SetActive(false);
                }
            }
        }
    }

    public void UpdateButtons()
    {
        if (ElevatorManager.GetUpElevator(Level) != this)
        {
            buttonUp.SetActive(true);
        }
        if (ElevatorManager.GetDownElevator(Level) != this)
        {
            buttonDown.SetActive(true);
        }
    }

    public void OnButtonUpPress()
    {
        ElevatorManager.PlayerInElevator.goElevatorUp();
    }
    
    public void OnButtonDownPress()
    {
        ElevatorManager.PlayerInElevator.goElevatorDown();
    }
}
