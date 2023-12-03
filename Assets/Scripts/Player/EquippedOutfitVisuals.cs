using System.Collections.Generic;
using ImportedScripts;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Assertions;

namespace ClothesShopToy
{
    public class EquippedOutfitVisuals : MonoBehaviour
    {
        [System.Serializable]
        public class SlotAnimatorPair
        {
            public EquipmentSlot slot;
            public Animator outfitAnimator;
        }
        
        [SerializeField] private EquipmentHolder equipmentHolder;
        [SerializeField] private List<SlotAnimatorPair> slotSprites;
        
        #region Unity Messages
        private void Awake()
        {
            Assert.IsNotNull(equipmentHolder);

            equipmentHolder.OnItemEquipped += ItemEventCallback;
            equipmentHolder.OnItemUnequipped += ItemEventCallback;
        }

        private void OnEnable()
        {
            UpdateAllOutfits();
        }

        private void OnDestroy()
        {
            if (equipmentHolder != null)
            {
                equipmentHolder.OnItemEquipped -= ItemEventCallback;
                equipmentHolder.OnItemUnequipped -= ItemEventCallback;
            }
        }
        #endregion

        #region Private Methods
        [Button]
        private void UpdateAllOutfits()
        {
            List<EquipmentSlot> slotsToHide = new List<EquipmentSlot>();
            foreach (SlotEquipmentPair slotPair in equipmentHolder.EquipmentSlots)
            {
                if (slotPair == null) continue;
                UpdateOutfitVisualForSlot(slotPair.slot, slotPair.item);
                if (slotPair.item != null)
                    slotsToHide.AddIfNew(slotPair.item.HidesOtherSlots);
            }

            HideSlots(slotsToHide);
        }
        
        private void UpdateOutfitVisualForSlot(EquipmentSlot slot, ItemEquipmentAsset equipmentAsset)
        {
            if (slot == null) return;

            foreach (SlotAnimatorPair slotSpritePair in slotSprites)
            {
                if (slotSpritePair == null) continue;
                if (slotSpritePair.slot != slot) continue;

                if (equipmentAsset == null)
                    SetAnimatorsController(slotSpritePair, null);
                else
                    SetAnimatorsController(slotSpritePair, equipmentAsset.OutfitAnimator);
            }
        }
        
        private void HideSlots(List<EquipmentSlot> slotsToHide)
        {
            foreach (SlotAnimatorPair slotSpritePair in slotSprites)
            {
                if (slotsToHide.Contains(slotSpritePair.slot)) 
                    SetAnimatorsController(slotSpritePair, null);
            }
        }

        private void ItemEventCallback(EquipmentHolder holder, EquipmentSlot slot, ItemEquipmentAsset equipmentAsset)
        {
            UpdateAllOutfits();
        }

        private static void SetAnimatorsController(SlotAnimatorPair pair, RuntimeAnimatorController newController)
        {
            pair.outfitAnimator.runtimeAnimatorController = newController;
            pair.outfitAnimator.enabled = newController != null;
        }
        #endregion
    }
}