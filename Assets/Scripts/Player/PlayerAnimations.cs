using System.Collections.Generic;
using ImportedScripts;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Assertions;

namespace ClothesShopToy
{
    public class PlayerAnimations : MonoBehaviour
    {
        [Header("Inspector References")]
        [SerializeField] private SpriteRenderer playerSpriteRenderer;
        [SerializeField] private InputMovement2D inputMovement;
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
            Assert.IsNotNull(inputMovement);
        }

        private void Update()
        {
            UpdateAnimatorParameters();
        }
        #endregion
        
        #region Private Methods
        private void UpdateAnimatorParameters()
        {
            bool walking = inputMovement.IsMovementLocked == false && inputMovement.MovementMagnitude > 0f;
            if (walking)
                receivedInput = inputMovement.MoveInput;

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