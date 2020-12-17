using Spire.Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Spire.Inventory
{

    [System.Serializable]
    [CreateAssetMenu(fileName = "New Healing Item", menuName = "Item/Healing")]
    public class Healing : Item
    {
        public float amountToHeal;
        public ItemCategory itemCategory = ItemCategory.Healing;
    }
}