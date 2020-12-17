using UnityEngine;

namespace Spire.Inventory
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "New Tool", menuName = "Item/Tool")]
    public class Tool : Item
    {
        public ItemCategory itemCategory = ItemCategory.Tool;
    }
}