using System;
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

        [SerializeField] private InputActionAsset inputAction;
        private InputAction moveAction;
        private InputAction jumpAction;
        private InputAction crouchAction;
        private InputAction interactAction;

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
            Debug.Log("Interacted!");
            if (activeInteractable != null)
            {
                activeInteractable.onInteractEvent.Invoke(this);
                Debug.Log("Event invoked!");
            }
        }
    }
}