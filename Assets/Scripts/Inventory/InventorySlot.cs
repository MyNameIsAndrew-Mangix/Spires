using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spire.Inventory
{
    public class InventorySlot
    {
        public Item item;
        public bool occupied;
        public Rect position;

        private void DrawSlot()
        {
            //GUI.DrawTexture(position, item.image);
    }

    }


}