using System.Collections.Generic;
using UnityEngine;

namespace ClothesShopToy
{
    [CreateAssetMenu(fileName = "New " + nameof(ItemEquipmentAsset), menuName = "Items/" + nameof(ItemEquipmentAsset))]
    public class ItemEquipmentAsset : ItemAsset
    {
        [Header("Outfit")]
        [SerializeField] private List<EquipmentSlot> validSlots;
        [SerializeField] private RuntimeAnimatorController outfitAnimator;
        
        public List<EquipmentSlot> ValidSlots => validSlots;
        public RuntimeAnimatorController OutfitAnimator => outfitAnimator;
    }
}