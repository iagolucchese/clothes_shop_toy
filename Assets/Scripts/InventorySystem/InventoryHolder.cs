using System.Collections.Generic;
using ImportedScripts;
using UnityEngine;

namespace ClothesShopToy
{
    public class InventoryHolder : MonoBehaviour
    {
        public delegate void InventoryItemEvent(InventoryHolder holder, ItemAsset item);
        public event InventoryItemEvent OnItemAdded;
        public event InventoryItemEvent OnItemRemoved;
        
        [SerializeField] private List<ItemAsset> itemsOnInventory;

        public List<ItemAsset> ItemsOnInventory => itemsOnInventory;
        public int AmountOfItemsInInventory => itemsOnInventory.SafeCount();
        
        #region Unity Messages
        private void OnEnable()
        {
            itemsOnInventory ??= new List<ItemAsset>();
        }
        #endregion

        #region Public Methods
        public bool AddItemToInventory(ItemAsset newItem)
        {
            itemsOnInventory.Add(newItem);
            OnItemAdded?.Invoke(this, newItem);
            return true;
        }

        public bool RemoveItemFromInventory(ItemAsset removeItem)
        {
            if (itemsOnInventory.IsInvalidOrEmpty()) return false;
            if (itemsOnInventory.Remove(removeItem))
            {
                OnItemRemoved?.Invoke(this, removeItem);
                return true;
            }
            return false;
        }
        #endregion

        #region Private Methods

        #endregion
    }
}