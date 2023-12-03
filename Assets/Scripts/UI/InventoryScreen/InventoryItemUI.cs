using System;
using UnityEngine;
using UnityEngine.UI;

namespace ClothesShopToy.UI
{
    public class InventoryItemUI : MonoBehaviour
    {
        public delegate void ItemUIEvent(InventoryItemUI itemUI);
        public static event ItemUIEvent OnItemClicked;
        
        [SerializeField] private Image itemIconImage;
        [SerializeField] private Image equippedOverlay;
        [SerializeField] private Button itemButton;

        private void OnEnable()
        {
            itemButton.onClick.AddListener(ItemClicked);
        }

        private void OnDisable()
        {
            itemButton.onClick.RemoveListener(ItemClicked);
        }

        private void ItemClicked()
        {
            OnItemClicked?.Invoke(this);
        }

        public void SetIcon(Sprite newIcon)
        {
            itemIconImage.sprite = newIcon;
        }

        public void SetEquipped(bool isEquipped)
        {
            equippedOverlay.enabled = isEquipped;
        }
    }
}