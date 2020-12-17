using UnityEngine;

namespace Spire.Inventory
{
    public enum AmmoType
    {
        AP,
        Incendiary,
        Slug,
        Buckshot,
        Blunt //Tech tree ammotype
    }

    public enum GunCategory
    {
        LMG = 0, //big penalties for carrying LMG. Double-edged sword type of weapon.
        SniperRifle = 1,
        AssaultRifle = 2,
        SMG = 3,
        Shotgun = 4,
        HandGun = 5,
    }

    [System.Serializable]
    [CreateAssetMenu(fileName = "New Gun", menuName = "Item/Gun")]
    public class Gun : Item
    {
        public GunCategory gunCategory;
        public AmmoType ammoType;
        public uint damage;
        //private int emptyMagazines;
        public int magazineCapacity;
        public int ammo;
        public float fireRate;
        public ItemCategory itemCategory = ItemCategory.Gun;

        /* Guns: That's not an easy question to answer. Honestly you could name any fictional weapons however you want as there are many variations for naming weapons:

Ak47 = Kalashnikov's automatic rifle model of year 1947
HK36 = Heckler and Koch 4.6x36 mm rifle.
M16 = Just a random letter and number assignment given to the weapon by the US military. The US army often codes its rifles with an M (M1, M14, M24, M60, XM8 -The X identifying the weapon as a prototype- etc).

Then we have nicknamed weapons:

Desert Eagle (Differing models of the weapon are identified by Mk)
The Colt Single Action Army Handgun, better known as the legendary Colt Peacemaker
M1918 is probably unfamiliar to many (It stands for Model 1918), but anyone who's played MoH or CoD knows it as the BAR (Browning Automatic Rifle)
And there's always the famous French FAMAS

It varies from one manufacturer or owner/contractor to the next so you can come up with anything you want far as reality is concerned.
*/

        //private enum GunStock
        //{

        //}

    }
}
 