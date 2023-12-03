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
        [SerializeField, ReadOnly] private bool lockedMovement;
        
        #region Unity Messages
        private void Awake()
        {
            Assert.IsNotNull(movementAction);
            Assert.IsNotNull(movementAction.action);
            Assert.IsNotNull(rigidbody2D);
            
            movementAction.action.Enable();
            lockedMovement = false;
            moveInput = Vector2.zero;
            movementMagnitude = 0f;
        }

        private void OnDestroy()
        {
            
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

        #region Public Methods

        #endregion

        #region Private Methods
        private void PlayerMovementLoop()
        {
            moveInput = movementAction.action.ReadValue<Vector2>();
            movementMagnitude = moveInput.magnitude;
            if (movementMagnitude <= 0f) return;
            if (lockedMovement) return;

            Vector2 movementTargetPosition = rigidbody2D.position + (moveInput * (moveSpeed * Time.fixedDeltaTime));
            rigidbody2D.MovePosition(movementTargetPosition);
        }
        #endregion
    }
}