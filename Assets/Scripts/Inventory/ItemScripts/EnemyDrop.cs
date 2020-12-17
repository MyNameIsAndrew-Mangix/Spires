using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spire.Inventory
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "New Enemy Drop", menuName = "Item/Enemy Drop")]
    public class EnemyDrop : Item
    {
        public ItemCategory itemCategory = ItemCategory.EnemyDrop;
    }
}

