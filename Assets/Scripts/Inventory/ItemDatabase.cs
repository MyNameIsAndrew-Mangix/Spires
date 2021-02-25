using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Spire.Inventory
{
    [ExecuteInEditMode]
    public class ItemDatabase : MonoBehaviour
    {
        public bool updateItemDatabase;
        public List<Item> itemList = new List<Item>();
        void Awake()
        {

        }

        /// <summary>
        /// Called when the script is loaded or a value is changed in the
        /// inspector (Called in the editor only).
        /// </summary>
        void OnValidate()
        {
            if (updateItemDatabase)
                UpdateDB();
        }

        private void UpdateDB()
        {
            string[] assetNames = AssetDatabase.FindAssets("t:Item", new[] { "Assets/ScriptableObjects" });
            itemList.Clear();
            foreach (string SOName in assetNames)
            {
                string SOPath = AssetDatabase.GUIDToAssetPath(SOName);
                Item item = AssetDatabase.LoadAssetAtPath<Item>(SOPath);
                itemList.Add(item);
            }
        }

    }
}
