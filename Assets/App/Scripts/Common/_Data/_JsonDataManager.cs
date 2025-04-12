using System.Collections.Generic;
using System.IO;
using App.Game.Process;
using UnityEngine;

namespace App.Common._Data
{
    public class _JsonDataManager
    {
        public static _isDictionaryWrapper isDictionaryWrapper = new _isDictionaryWrapper();
        public void SaveData()
        {
            SaveDictionaryData();
        }
        public void LoadData()
        {
            LoadDictionaryData();
        }
        private void SaveDictionaryData()
        {
            isDictionaryWrapper.isDictionary = ProcessSystem.isDictionary;
            string filePath = Application.persistentDataPath + "/isDictionary.json";
            Debug.Log("filePath: " + filePath);
            string directoryPath = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            if (ProcessSystem.isDictionary.Count == 0)
            {
                for (int i = 0; i < isDictionaryWrapper.maxCount; i++)
                {
                    ProcessSystem.isDictionary.Add(true);
                }
            }
            var json = JsonUtility.ToJson(isDictionaryWrapper, true);
            File.WriteAllText(filePath, json);
        }
        private void LoadDictionaryData()
        {
            string filePath = Application.persistentDataPath + "/isDictionary.json";
            string directoryPath = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                isDictionaryWrapper = JsonUtility.FromJson<_isDictionaryWrapper>(json);
                ProcessSystem.isDictionary = isDictionaryWrapper.isDictionary;
            }
            else
            {
                for (int i = 0; i< isDictionaryWrapper.maxCount; i++)
                {
                    ProcessSystem.isDictionary.Add(false);
                }
                SaveDictionaryData();
            }
        }
    }
}

namespace App.Common._Data
{
    public class _isDictionaryWrapper
    {
        public int maxCount = 24;
        public List<bool> isDictionary = ProcessSystem.isDictionary;
    }
}