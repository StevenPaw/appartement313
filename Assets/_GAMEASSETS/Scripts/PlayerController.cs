using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _GAMEASSETS.Scripts
{
    public class PlayerController : MonoBehaviour
    {
        private CharacterController2D characterController;
        private float moveDirection;
        private bool isJumping;
        private bool isCrouching;
        private bool inElevator;

        [SerializeField] private bool canMove;
        [SerializeField] private GameObject spriteGO;
        [SerializeField] private InputActionAsset inputAction;
        private InputAction moveAction;
        private InputAction jumpAction;
        private InputAction crouchAction;
        private InputAction interactAction;

        [SerializeField] private Elevator elevatorInUse = null;

        private Interactable activeInteractable;

        public Interactable ActiveInteractable
        {
            get => activeInteractable;
            set => activeInteractable = value;
        }

        public bool CanMove
        {
            get => canMove;
            set => canMove = value;
        }

        public GameObject SpriteGO
        {
            get => spriteGO;
            set => spriteGO = value;
        }

        public Elevator ElevatorInUse
        {
            get => elevatorInUse;
            set => elevatorInUse = value;
        }

        public float MoveDirection
        {
            get => moveDirection;
            set => moveDirection = value;
        }

        private void Awake()
        {
            moveAction = inputAction["move"];
            jumpAction = inputAction["jump"];
            crouchAction = inputAction["crouch"];
            interactAction = inputAction["interact"];

            characterController = GetComponent<CharacterController2D>();
        }

        private void Update()
        {
            if (canMove)
            {
                if (jumpAction.triggered)
                {
                    isJumping = true;
                }
                else
                {
                    isJumping = false;
                }

                characterController.Move(moveDirection, isCrouching, isJumping);
            }
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            if (canMove)
            {
                moveDirection = context.ReadValue<float>();
            }
        }

        public void OnInteract(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                if (activeInteractable != null)
                {
                    activeInteractable.onInteractEvent.Invoke(this);
                }
            }
        }

        public void OnElevatorUp(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                goElevatorUp();
            }
        }

        public void goElevatorUp()
        {
            if (elevatorInUse != null)
            {
                int currentLevel = elevatorInUse.Level;
                Elevator targetElevator = ElevatorManager.GetUpElevator(currentLevel);
                //transform.position = targetElevator.gameObject.transform.position;
                transform.DOMove(targetElevator.DoorPosition + Vector3.forward * 1.5f, .5f);
                elevatorInUse = targetElevator;
                targetElevator.UpdateButtons();
            }
        }
        
        public void OnElevatorDown(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                goElevatorDown();
            }
        }

        public void goElevatorDown()
        {
            if (elevatorInUse != null)
            {
                int currentLevel = elevatorInUse.Level;
                Elevator targetElevator = ElevatorManager.GetDownElevator(currentLevel);
                //transform.position = targetElevator.gameObject.transform.position;
                transform.DOMove(targetElevator.DoorPosition + Vector3.forward * 1.5f, .5f);
                elevatorInUse = targetElevator;
                targetElevator.UpdateButtons();
            }
        }
    }
}