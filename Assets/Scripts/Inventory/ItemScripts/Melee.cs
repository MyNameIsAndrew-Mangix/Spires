using UnityEngine;

namespace Spire.Inventory
{
    public enum MeleeWeaponType
    {
        KBar, //also bayonet
        PowerFist,
        PowerSword,
        PowerGlaive, //or kwandao or pudao
        PowerHammer,

    }

    //might make power weapons in to vibro(bladed) or rocket-powered(blunt)

    [System.Serializable]
    [CreateAssetMenu(fileName = "New Melee Weapon", menuName = "Item/Melee")]
    public class Melee : Item
    {
        public int baseDamage;
        public int staminaCost;
        public ItemCategory itemCategory = ItemCategory.MeleeWeapon;
    }
}