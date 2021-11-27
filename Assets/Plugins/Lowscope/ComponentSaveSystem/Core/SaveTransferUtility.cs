using Lowscope.Saving.Data;
using Lowscope.Saving.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

namespace Lowscope.Saving.Core
{
    public class SaveTransferUtility
    {
        public static string ConvertSaveToWebJSON(SaveGame saveGame)
        {
            if (saveGame == null)
            {
                Debug.Log("Save Master: Could not convert save to JSON, no save or slot is loaded.");
                return "";
            }

            string data = "";
            var storageType = SaveSettings.Get().storageType;

            switch (storageType)
            {
                // Both storage types have the same serializable fields under the hood.
                case StorageType.JSON:
                case StorageType.Binary:
                    data = JsonUtility.ToJson(saveGame, SaveSettings.Get().useJsonPrettyPrint);
                    break;
                // SQLite requires conversion.
                case StorageType.SQLiteExperimental:

                    var save = (saveGame as SaveGameSqlite).ConvertTo(StorageType.JSON, "", false);
                    data = JsonUtility.ToJson(save, SaveSettings.Get().useJsonPrettyPrint);

                    break;
                default:
                    break;
            }

            data = data
                .Replace("\\\"", "\"")
                .Replace("\"{", "{")
                .Replace("}\"", "}");

            return data;
        }


        public static void OverwriteSaveWithWebJSON(string json, string fileName)
        {
            if (!SaveFileUtility.IsSaveFileNameUsed(fileName) || string.IsNullOrEmpty(json))
            {
                Debug.Log("SaveFileUtility: Failed to load save from web. No path or data specified.");
                return;
            }

            string filePath = SaveFileUtility.GetSaveFilePath(fileName);

            StringBuilder sb = ModifyJSONDataEntries(json, (s) =>
            {
                bool isJsonLine = false;

                if (s.Length > 5)
                {
                    if (s[0].Equals('{'))
                    {
                        isJsonLine = true;
                    }
                }

                if (isJsonLine)
                {
                    // Find first { and last } for each json entry.
                    // And add a " before, and after each "

                    s.Replace(@"""", @"\""")
                    .Insert(0, '"')
                    .Append('"');
                }

                return s;
            });


            string testPath = Path.Combine(SaveFileUtility.TempFolderPath, "issue.txt");

            if (!Directory.Exists(Path.GetDirectoryName(testPath)))
                Directory.CreateDirectory(Path.GetDirectoryName(testPath));

            File.WriteAllText(testPath, sb.ToString());

            SaveGame saveGame = JsonUtility.FromJson<SaveGameJSON>(sb.ToString());
            var storageType = SaveSettings.Get().storageType;

            if (storageType != StorageType.JSON)
            {
                saveGame = (saveGame as SaveGameJSON).ConvertTo(storageType, filePath, false);
            }

            saveGame.WriteSaveFile(saveGame, filePath);
        }

        /// <summary>
        /// Reads through all json & gets all data lines.
        /// </summary>
        /// <param name="json"></param>
        /// <param name="modifyLine"></param>
        /// <returns></returns>
        private static StringBuilder ModifyJSONDataEntries(string json, Func<StringBuilder, StringBuilder> modifyLine)
        {
            string startWord = "\"data\"" + ":";
            string endWord = "\"scene\"" + ":";

            List<int> startIndexes = AllIndexesOf(json, startWord);
            List<int> endIndexes = AllIndexesOf(json, endWord);

            int indexCount = endIndexes.Count;

            int endIndexSpace = SaveSettings.Get().useJsonPrettyPrint ? 14 : 1;
            int frontIndexSpace = SaveSettings.Get().useJsonPrettyPrint ? 0 : 1;

            StringBuilder sb = new StringBuilder();

            int low = 0;

            for (int i = 0; i < indexCount; i++)
            {
                int startIndex = startIndexes[i] + startWord.Length + 1;
                int endIndex = endIndexes[i];

                sb.Append(json.Substring(low, (startIndexes[i] + startWord.Length + 1) - low));

                // Modify string so it is escaped again.
                var getString = modifyLine.Invoke(new StringBuilder(json.Substring(startIndex - frontIndexSpace, (endIndex - startIndex) - endIndexSpace)));
                sb.Append(getString.ToString());

                low = endIndexes[i] - endIndexSpace;
            }

            int lastIndex = endIndexes[endIndexes.Count - 1] - endIndexSpace;
            int lastLength = json.Length - (endIndexes[endIndexes.Count - 1] - endIndexSpace);

            sb.Append(json.Substring(lastIndex, lastLength));

            return sb;
        }

        private static List<int> AllIndexesOf(string str, string searchstring)
        {
            List<int> values = new List<int>();

            int minIndex = str.IndexOf(searchstring);
            while (minIndex != -1)
            {
                values.Add(minIndex);
                minIndex = str.IndexOf(searchstring, minIndex + searchstring.Length);
            }

            return values;
        }
    }
}