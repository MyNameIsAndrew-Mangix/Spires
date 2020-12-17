using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Spire.Inventory
{
    public enum ItemCategory
    {
        MeleeWeapon = 0,
        Gun = 1,
        Armor = 2,
        Healing = 3,
        EnemyDrop = 4,
        Tool = 5
    }

    public enum SlotType
    {
        NotEquipable = 0,
        Headwear = 1,
        Eyewear = 2,
        Torso = 3,
        Legs = 4,
        Feet = 5,
        Back = 6,
        PrimaryWeapon = 7,
        SecondaryWeapon = 8,
        Tool = 9

    }

    [System.Serializable]
    public class Item : ScriptableObject
    {
        public new string name;                     //What the item will be called.
        public string description;                  //What the item's flavor text will be.
        public Image image;                         //What the item's icon will be in inventory.
        public Sprite sprite = null;                //What the item will look like in-game.
        public SlotType slotType = SlotType.NotEquipable;   //What slot the item goes into if equippable
        public int id;                              //Item ID
        public float weight;                        // How much the item will weigh for encumbrance. Will be lbs.
        public float slotWidth;                     //How many spaces the item will take up in the inventory grid on x axis
        public float slotHeight;                    //How many spaces the item will take up in the inventory grid on y axis
        public bool isIndestructible = false;       //Can the play destroy this item?
        public bool isQuestItem = false;            //Is this item a quest item? If it's a quest item, will be indestructible.
    }
}