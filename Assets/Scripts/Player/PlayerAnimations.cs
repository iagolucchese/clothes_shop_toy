using ImportedScripts;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;

namespace ClothesShopToy
{
    public class PlayerAnimations : MonoBehaviour
    {
        [Header("Inspector References")]
        [SerializeField] private SpriteRenderer playerSpriteRenderer;
        [SerializeField] private Animator playerAnimator;
        [SerializeField] private InputActionReference movementInput;
        [Header("Animator Parameters")]
        [SerializeField, AnimatorParam("playerAnimator")] private int horizontalParam;
        [SerializeField, AnimatorParam("playerAnimator")] private int verticalParam;
        [SerializeField, AnimatorParam("playerAnimator")] private int walkParam;
        
        #region Unity Messages
        private void Awake()
        {
            Assert.IsNotNull(playerSpriteRenderer);
            Assert.IsNotNull(playerAnimator);
            Assert.IsNotNull(movementInput);
            Assert.IsNotNull(movementInput.action);
            
            movementInput.action.Enable();
        }

        private void OnDestroy()
        {
            
        }

        private void Update()
        {
            UpdateAnimatorParameters();
        }
        #endregion

        #region Public Methods

        #endregion

        #region Private Methods
        private void UpdateAnimatorParameters()
        {
            Vector2 input = movementInput.action.ReadValue<Vector2>();
            bool walking = input.sqrMagnitude > 0f;
            playerAnimator.SafeSetParameter(walkParam, walking);
            
            if (!walking) return;

            playerAnimator.SafeSetParameter(horizontalParam, input.x);
            playerAnimator.SafeSetParameter(verticalParam, input.y);
        }
        #endregion
    }
}