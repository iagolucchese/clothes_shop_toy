using System.Collections.Generic;
using ImportedScripts;
using UnityEngine;
using UnityEngine.Assertions;

namespace ClothesShopToy.UI
{
    public class PlayerInventoryScreen : ScreenManagerBase
    {
        [SerializeField] private InventoryHolder inventory;
        [SerializeField] private EquipmentHolder equipmentHolder;
        [SerializeField] private List<InventoryItemUI> allInventorySlots;
        
        #region Unity Messages
        protected override void Awake()
        {
            base.Awake();
            Assert.IsNotNull(inventory);
            Assert.IsNotNull(equipmentHolder);
            Assert.IsTrue(allInventorySlots.IsValidAndNotEmpty());

            inventory.OnItemAdded += InventoryItemEventCallback;
            inventory.OnItemRemoved += InventoryItemEventCallback;
            InventoryItemUI.OnItemClicked += ItemClickedCallback;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            InventoryItemUI.OnItemClicked -= ItemClickedCallback;
            if (inventory != null)
            {
                inventory.OnItemAdded += InventoryItemEventCallback;
                inventory.OnItemRemoved += InventoryItemEventCallback;
            }
        }
        #endregion

        #region Public Methods
        #endregion

        #region Private Methods
        private void ItemClickedCallback(InventoryItemUI itemUI)
        {
            int clickedItemIndex = allInventorySlots.IndexOf(itemUI);
            if (clickedItemIndex >= inventory.AmountOfItemsInInventory)
            {
                throw new System.IndexOutOfRangeException("Tried to click an item, but index came out of bounds.");
            }
            
            ItemEquipmentAsset clickedItem = inventory.ItemsOnInventory[clickedItemIndex] as ItemEquipmentAsset;
            if (clickedItem == null) return;

            equipmentHolder.ToggleItemEquipped(clickedItem);
            UpdateInventoryIcons();
        }
        
        protected override void ToggleScreen(bool show)
        {
            UpdateInventoryIcons();
            base.ToggleScreen(show);
        }

        private void InventoryItemEventCallback(InventoryHolder holder, ItemAsset item)
        {
            UpdateInventoryIcons();
        }

        private void UpdateInventoryIcons()
        {
            List<ItemAsset> allItems = inventory.ItemsOnInventory;
            for (int index = 0; index < allInventorySlots.Count; index++)
            {
                InventoryItemUI itemUI = allInventorySlots[index];
                if (index >= allItems.SafeCount())
                {
                    itemUI.gameObject.SetActive(false);
                    continue;
                }
                
                ItemAsset item = allItems[index];
                if (item == null) continue;
                
                itemUI.SetIcon(item.ItemIcon);
                itemUI.SetEquipped( equipmentHolder.IsItemEquipped(item) );
                itemUI.gameObject.SetActive(true);
            }
        }
        #endregion
    }
}