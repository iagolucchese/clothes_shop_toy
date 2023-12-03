using System.Collections.Generic;
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
        [SerializeField] private InputActionReference movementInput;
        [SerializeField] private Animator mainAnimator;
        [SerializeField] private List<Animator> outfitAnimators;
        [Header("Animator Parameters")]
        [SerializeField, AnimatorParam("mainAnimator")] private int horizontalParam;
        [SerializeField, AnimatorParam("mainAnimator")] private int verticalParam;
        [SerializeField, AnimatorParam("mainAnimator")] private int walkParam;
        private Vector2 receivedInput;
        
        #region Unity Messages
        private void Awake()
        {
            Assert.IsNotNull(playerSpriteRenderer);
            Assert.IsNotNull(outfitAnimators);
            Assert.IsNotNull(movementInput);
            Assert.IsNotNull(movementInput.action);
            
            movementInput.action.Enable();
        }

        private void Update()
        {
            UpdateAnimatorParameters();
        }
        #endregion
        
        #region Private Methods
        private void UpdateAnimatorParameters()
        {
            Vector2 newInput = movementInput.action.ReadValue<Vector2>();
            bool walking = newInput.sqrMagnitude > 0f;
            if (walking)
                receivedInput = newInput;

            mainAnimator.SafeSetParameter(walkParam, walking);
            mainAnimator.SafeSetParameter(horizontalParam, receivedInput.x);
            mainAnimator.SafeSetParameter(verticalParam, receivedInput.y);
            
            outfitAnimators.ForEach(animator => animator.SafeSetParameter(walkParam, walking));
            outfitAnimators.ForEach(animator => animator.SafeSetParameter(horizontalParam, receivedInput.x));
            outfitAnimators.ForEach(animator => animator.SafeSetParameter(verticalParam, receivedInput.y));
        }
        #endregion
    }
}