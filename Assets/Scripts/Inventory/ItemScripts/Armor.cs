using UnityEngine;
using Spire.Core;

namespace Spire.Inventory
{
    public enum ArmorCategory
    {
        Clothing,
        Light,
        Medium,
        Heavy,
        Exoskeleton
    }
    public enum ArmorSlot
    {
        Helmet,
        Eyewear,
        Torso,
        Pants,
        Shoes
    }
    public enum AuraEffect
    {
        Null,
        StatBonus,
        StatPenalty,
        StatusEffect,
        DPS
    }
    [System.Serializable]
    public struct ItemStatBonus
    {
        public int Strength;
        public int Agility;
        public int Endurance;
        public int Wits;
        public int Intimidation;
        public int Perseverance;
    }
    [System.Serializable]
    [CreateAssetMenu(fileName = "New Armor", menuName = "Item/Armor")]
    public class Armor : Item
    {
        public ArmorCategory armorCategory;
        public ArmorSlot armorSlot;
        public AuraEffect auraEffect;

        [Range(-100, 100)]
        public int damageReductionPCT;
        [Range(-100, 100)]
        public int evasiveness;
        public float batteryCharge;
        public ItemCategory itemCategory = ItemCategory.Armor;
        public bool isSetItem = false;              //Is item part of a set? TODO: Make set bonuses.
        public bool hasStatBonus;
        [DrawIf("hasStatBonus", true)]
        public ItemStatBonus itemStatBonus;

        /* 
         * TODO: JUST MAKE A NEW TYPE OF ITEM SPECIFICALLY FOR EXOSKELETONS
         * Exoskeleton backpack slot is battery
         * exoskeleton armor is like iron man suit
         * exoskeleton research that emitts a sound that only the aliens can hear, causing them to be agitated by the wearer and focus the, (increased threat / taunting enemies)
         * maybe just clothing, light, medium, and heavy armor but exoskeleton variants for light-heavy, lighter the exoskeleton the less power they need. i.e. more inventory from backpack.
         * Or the more powerful the battery, the heavier it is, but it reduces stamina cost more than lighter batteries.
         */
    }
}