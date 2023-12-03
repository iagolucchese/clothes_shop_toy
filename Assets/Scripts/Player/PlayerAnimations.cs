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
            Vector2 input = movementInput.action.ReadValue<Vector2>();
            bool walking = input.sqrMagnitude > 0f;
            
            mainAnimator.SafeSetParameter(walkParam, walking);
            outfitAnimators.ForEach(animator => animator.SafeSetParameter(walkParam, walking));
            
            if (!walking) return;

            mainAnimator.SafeSetParameter(horizontalParam, input.x);
            mainAnimator.SafeSetParameter(verticalParam, input.y);
            outfitAnimators.ForEach(animator => animator.SafeSetParameter(horizontalParam, input.x));
            outfitAnimators.ForEach(animator => animator.SafeSetParameter(verticalParam, input.y));
        }
        #endregion
    }
}