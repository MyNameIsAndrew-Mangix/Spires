using System.Collections.Generic;
using UnityEngine;

namespace Spire.Inventory
{
    public class ItemDatabase : MonoBehaviour
    {
        public List<Item> items = new List<Item>();
        void Awake()
        {
           
        }

        //public BaseItem GetItemByID(int id)
        //{
        //    return items.Find(item => item.id == id);
        //}
    }
}