using System.Collections.Generic;
using System.IO;
using App.Game.Process;
using UnityEngine;
using System.Linq;

namespace App.Common._Data
{
    public static class _JsonDataManager
    {
        private static _DictionaryWrapper dictionaryWrapper = new _DictionaryWrapper();
        public static void SaveDictionaryData()
        {
            dictionaryWrapper.isDictionaryOpen = _PlayerStatistics.isDictionaryOpen;
            string filePath = Application.persistentDataPath + "/isDictionaryOpen.json";
            string directoryPath = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            var json = JsonUtility.ToJson(dictionaryWrapper, true);
            File.WriteAllText(filePath, json);
        }
        public static void LoadDictionaryData()
        {
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
