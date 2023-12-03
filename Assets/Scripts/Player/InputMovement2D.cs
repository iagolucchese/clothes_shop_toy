using System;
using ClothesShopToy.UI;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;

namespace ClothesShopToy
{
    public class InputMovement2D : MonoBehaviour
    {
        [Header("Movement Parameters")]
        [SerializeField, Min(0f)] private float moveSpeed = 2f;
        [Header("References")]
        [SerializeField] private InputActionReference movementAction;
        [SerializeField] private new Rigidbody2D rigidbody2D;
        [Header("Debug")]
        [SerializeField, ReadOnly] private Vector2 moveInput;
        [SerializeField, ReadOnly] private float movementMagnitude;
        [SerializeField, ReadOnly] private int movementLocks;

        public int MovementLocks
        {
            get => movementLocks;
            set => movementLocks = value < 0 ? 0 : value;
        }
        public bool IsMovementLocked => MovementLocks > 0;
        public Vector2 MoveInput => moveInput;
        public float MovementMagnitude => movementMagnitude;

        #region Unity Messages
        private void Awake()
        {
            Assert.IsNotNull(movementAction);
            Assert.IsNotNull(movementAction.action);
            Assert.IsNotNull(rigidbody2D);
            
            movementAction.action.Enable();
            moveInput = Vector2.zero;
            movementMagnitude = 0f;
        }

        private void Start()
        {
            MovementLocks = 0;
            ScreenManagerBase.OnScreenOpened += AddMovementLock;
            ScreenManagerBase.OnScreenClosed += RemoveMovementLock;
        }

        private void OnDestroy()
        {
            ScreenManagerBase.OnScreenOpened -= AddMovementLock;
            ScreenManagerBase.OnScreenClosed -= RemoveMovementLock;
        }

        private void FixedUpdate()
        {
            PlayerMovementLoop();
        }

        private void Reset()
        {
            rigidbody2D = GetComponentInChildren<Rigidbody2D>();
        }
        #endregion

        #region Private Methods
        private void AddMovementLock() => MovementLocks++;
        private void RemoveMovementLock() => MovementLocks--;
        
        private void PlayerMovementLoop()
        {
            moveInput = movementAction.action.ReadValue<Vector2>();
            movementMagnitude = moveInput.magnitude;
            if (movementMagnitude <= 0f) return;
            if (IsMovementLocked) return;

            Vector2 movementTargetPosition = rigidbody2D.position + (moveInput * (moveSpeed * Time.fixedDeltaTime));
            rigidbody2D.MovePosition(movementTargetPosition);
        }
        #endregion
    }
}