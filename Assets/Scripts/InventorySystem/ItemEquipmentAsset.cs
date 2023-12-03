using System.Collections.Generic;
using UnityEngine;

namespace ClothesShopToy
{
    [CreateAssetMenu(fileName = "New " + nameof(ItemEquipmentAsset), menuName = "Items/" + nameof(ItemEquipmentAsset))]
    public class ItemEquipmentAsset : ItemAsset
    {
        [Header("Outfit")]
        [SerializeField] private RuntimeAnimatorController outfitAnimator;
        [SerializeField] private List<EquipmentSlot> validSlots;
        [SerializeField] private List<EquipmentSlot> hidesOtherSlots;

        public RuntimeAnimatorController OutfitAnimator => outfitAnimator;
        public List<EquipmentSlot> ValidSlots => validSlots;
        public List<EquipmentSlot> HidesOtherSlots => hidesOtherSlots;
    }
}