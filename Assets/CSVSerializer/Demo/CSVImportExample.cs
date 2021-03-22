// using System.Collections;
// using System.Collections.Generic;
// using Spire.Stats;
// using UnityEngine;
// #if UNITY_EDITOR
// using UnityEditor;
// #endif

// public class CSVImportExample : ScriptableObject
// {
//     [System.Serializable]
//     public class Sample
//     {
//         public int year;
//         public string make;
//         public string model;
//         public string description;
//         public float price;
//     }
//     public Sample[] m_Sample;
// }

// #if UNITY_EDITOR
// public class CSVImportExamplePostprocessor : AssetPostprocessor
// {
//     static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
//     {
//         foreach (string str in importedAssets)
//         {
//             if (str.IndexOf("/stat_spreadsheet.csv") != -1)
//             {
//                 TextAsset data = AssetDatabase.LoadAssetAtPath<TextAsset>(str);
//                 string assetfile = str.Replace(".csv", ".asset");
//                 AttributeBlock ab = AssetDatabase.LoadAssetAtPath<AttributeBlock>(assetfile);
//                 if (ab == null)
//                 {
//                     ab = new AttributeBlock();
//                     AssetDatabase.CreateAsset(ab, assetfile);
//                 }
//                 ab.attributes = CSVSerializer.Deserialize<Attribute>(data.text);

//                 EditorUtility.SetDirty(ab);
//                 AssetDatabase.SaveAssets();
// #if DEBUG_LOG || UNITY_EDITOR
//                 Debug.Log("Reimported Asset: " + str);
// #endif
//             }
//         }
//     }
// }
// #endif