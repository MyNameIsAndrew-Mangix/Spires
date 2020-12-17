using System.Collections.Generic;
using UnityEngine;

namespace Spire.Inventory
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "New Backpack", menuName = "Item/Backpack")]
    public class Backpack : Item
    {

        public float curWeight;
        public float maxWeight;
        public ArmorCategory armorCategory;
        private List<Item> _items;
    }
}