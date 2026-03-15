using System.IO;
using UnityEngine;

namespace App.Common._Data
{
    public static class _JsonDataManager
    {
        private static _DictionaryWrapper dictionaryWrapper = new _DictionaryWrapper();
        public static void SaveDictionaryData()
        {
            Debug.Log("Saving dictionary data...");
            dictionaryWrapper.isDictionaryOpen = _PlayerStatistics.isDictionaryOpen;
            string filePath = Application.persistentDataPath + "/isDictionaryOpen.json";
            string directoryPath = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            var json = JsonUtility.ToJson(dictionaryWrapper, true);
            File.WriteAllText(filePath, json);
            Debug.Log("Dictionary data saved to: " + filePath);
        }
        public static void LoadDictionaryData()
        {
            Debug.Log("Loading dictionary data...");
            string filePath = Application.persistentDataPath + "/isDictionaryOpen.json";
            string directoryPath = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
                SaveDictionaryData();
            }
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                dictionaryWrapper = JsonUtility.FromJson<_DictionaryWrapper>(json);
                for (int i = 0; i < dictionaryWrapper.isDictionaryOpen.Length; i++)
                {
                    _PlayerStatistics.isDictionaryOpen[i] = dictionaryWrapper.isDictionaryOpen[i];
                }
            }
            else
            {
                for (int i = 0; i< dictionaryWrapper.isDictionaryOpen.Length; i++)
                {
                    dictionaryWrapper.isDictionaryOpen[i] = false;
                }
                SaveDictionaryData();
            }
            Debug.Log("Dictionary data loaded from: " + filePath);
        }
    }
}

namespace App.Common._Data
{
    public class _DictionaryWrapper
    {
        public bool[] isDictionaryOpen = new bool[_PlayerStatistics.DictionaryNumMax];

        public _DictionaryWrapper()
        {
            isDictionaryOpen = _PlayerStatistics.isDictionaryOpen;
        }
    }
}
