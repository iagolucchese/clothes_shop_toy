using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace ClothesShopToy.Utils
{
    public class InputToUnityEvent : MonoBehaviour
    {
        [SerializeField] private InputActionReference inputAction;
        [SerializeField] private UnityEvent onAction;

        private void OnEnable()
        {
            inputAction.action.Enable();
            inputAction.action.started += StartedCallback;
        }

        private void OnDisable()
        {
            inputAction.action.started -= StartedCallback;
        }

        private void StartedCallback(InputAction.CallbackContext callbackContext)
        {
            if (gameObject.activeInHierarchy == false) return;
            onAction?.Invoke();
        }
    }
}