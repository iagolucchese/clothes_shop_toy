using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Assertions;

namespace ClothesShopToy
{
    public class EquippedOutfitVisuals : MonoBehaviour
    {
        [System.Serializable]
        public class SlotSpritePair
        {
            public EquipmentSlot slot;
            public Animator outfitAnimator;
        }
        
        [SerializeField] private EquipmentHolder equipmentHolder;
        [SerializeField] private List<SlotSpritePair> slotSprites;
        
        #region Unity Messages
        private void Awake()
        {
            Assert.IsNotNull(equipmentHolder);

            equipmentHolder.OnItemEquipped += UpdateOutfitVisualForSlot;
            equipmentHolder.OnItemUnequipped += UpdateOutfitVisualForSlot;
        }

        private void OnEnable()
        {
            UpdateAllOutfits();
        }

        private void OnDestroy()
        {
            if (equipmentHolder != null)
            {
                equipmentHolder.OnItemEquipped -= UpdateOutfitVisualForSlot;
                equipmentHolder.OnItemUnequipped -= UpdateOutfitVisualForSlot;
            }
        }
        #endregion

        #region Private Methods
        [Button]
        private void UpdateAllOutfits()
        {
            foreach (SlotEquipmentPair slotPair in equipmentHolder.EquipmentSlots)
            {
                if (slotPair == null) continue;
                UpdateOutfitVisualForSlot(equipmentHolder, slotPair.slot, slotPair.item);
            }
        }
        
        private void UpdateOutfitVisualForSlot(EquipmentHolder holder, EquipmentSlot slot, ItemEquipmentAsset equipmentAsset)
        {
            if (slot == null || equipmentAsset == null) return;
            foreach (SlotSpritePair slotSpritePair in slotSprites)
            {
                if (slotSpritePair == null) continue;
                if (slotSpritePair.slot != slot) continue;
                if (slotSpritePair.outfitAnimator != null)
                    slotSpritePair.outfitAnimator.runtimeAnimatorController = equipmentAsset.OutfitAnimator;
            }
        }
        #endregion
    }
}