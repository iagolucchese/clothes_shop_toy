﻿using System;
using ClothesShopToy.UI;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace ClothesShopToy
{
    [RequireComponent(typeof(Collider2D))]
    public class PlayerInteractArea : MonoBehaviour
    {
        public delegate void InteractAreaEvent(PlayerInteractArea interactArea);
        public static event InteractAreaEvent OnPlayerInteract;
        
        [SerializeField] private InputActionReference interactAction;
        [SerializeField] private GameObject interactVisual;
        [SerializeField, Tag] private string playerTag = "Player";
        [SerializeField, ReadOnly] private bool isPlayerInArea;
        [SerializeField, ReadOnly] private bool interactionBlocked;
        [SerializeField] private UnityEvent onInteract;
        
        #region Unity Messages
        private void Awake()
        {
            Assert.IsNotNull(interactAction);

            interactAction.action.Enable();
            ScreenManagerBase.OnScreenOpened += BlockInteraction;
            ScreenManagerBase.OnScreenClosed += UnlockInteraction;
        }

        private void OnDestroy()
        {
            if (interactAction != null)
                interactAction.action.started -= InteractActionCallback;
            ScreenManagerBase.OnScreenOpened -= BlockInteraction;
            ScreenManagerBase.OnScreenClosed -= UnlockInteraction;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(playerTag) == false) return;
            isPlayerInArea = true;
            interactAction.action.started -= InteractActionCallback;
            interactAction.action.started += InteractActionCallback;

            if (interactVisual != null)
                interactVisual.SetActive(true);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag(playerTag) == false) return;
            isPlayerInArea = false;
            interactAction.action.started -= InteractActionCallback;
            
            if (interactVisual != null)
                interactVisual.SetActive(false);
        }
        #endregion

        #region Private Methods
        private void BlockInteraction() => interactionBlocked = true;
        private void UnlockInteraction() => interactionBlocked = false;
        
        private void InteractActionCallback(InputAction.CallbackContext callbackContext)
        {
            if (isPlayerInArea == false) return;
            if (interactionBlocked) return;
            
            OnPlayerInteract?.Invoke(this);
            onInteract?.Invoke();
        }
        #endregion
    }
}