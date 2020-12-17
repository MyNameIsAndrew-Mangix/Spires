using UnityEngine;

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
    [System.Serializable]
    [CreateAssetMenu(fileName = "New Armor", menuName = "Item/Armor")]
    public class Armor : Item
    {

        public ArmorCategory armorCategory;
        public ArmorSlot armorSlot;
        [Range(-100, 100)]
        public int damageReductionPCT;
        [Range(-100, 100)]
        public int evasiveness;
        public float batteryCharge;
        public ItemCategory itemCategory = ItemCategory.Armor;
        public bool isSetItem = false;              //Is item part of a set? TODO: Make set bonuses.

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