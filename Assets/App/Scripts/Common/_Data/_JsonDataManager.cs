using System;
using System.IO;
using UnityEngine;

namespace App.Common._Data
{
    public static class _JsonDataManager
    {
        private const string FileName = "isDictionaryOpen.json";
        private const string PlayerPrefsKey = "isDictionaryOpen_json";

        private static _DictionaryWrapper dictionaryWrapper = new _DictionaryWrapper();

        public static void SaveDictionaryData()
        {
            Debug.Log("Saving dictionary data...");
            dictionaryWrapper.isDictionaryOpen = CopyOrCreateArray(_PlayerStatistics.isDictionaryOpen);

            string json = JsonUtility.ToJson(dictionaryWrapper, true);

#if UNITY_WEBGL && !UNITY_EDITOR
            PlayerPrefs.SetString(PlayerPrefsKey, json);
            PlayerPrefs.Save();
            Debug.Log("Dictionary data saved to PlayerPrefs (WebGL).");
#else
            string filePath = Path.Combine(Application.persistentDataPath, FileName);
            string directoryPath = Path.GetDirectoryName(filePath);

            if (!string.IsNullOrEmpty(directoryPath) && !Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            File.WriteAllText(filePath, json);
            Debug.Log("Dictionary data saved to: " + filePath);
#endif
        }

        public static void LoadDictionaryData()
        {
            Debug.Log("Loading dictionary data...");
            string json = null;

#if UNITY_WEBGL && !UNITY_EDITOR
            if (PlayerPrefs.HasKey(PlayerPrefsKey))
            {
                json = PlayerPrefs.GetString(PlayerPrefsKey);
            }
#else
            string filePath = Path.Combine(Application.persistentDataPath, FileName);
            string directoryPath = Path.GetDirectoryName(filePath);

            if (!string.IsNullOrEmpty(directoryPath) && !Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            if (File.Exists(filePath))
            {
                json = File.ReadAllText(filePath);
                Debug.Log("Dictionary data loaded from: " + filePath);
            }
#endif

            var loadedWrapper = string.IsNullOrEmpty(json)
                ? null
                : JsonUtility.FromJson<_DictionaryWrapper>(json);

            if (loadedWrapper?.isDictionaryOpen == null || loadedWrapper.isDictionaryOpen.Length == 0)
            {
                InitializeDefaultAndSave();
                return;
            }

            int targetLength = _PlayerStatistics.DictionaryNumMax;
            EnsurePlayerStatisticsArray(targetLength);

            int copyLength = Mathf.Min(_PlayerStatistics.isDictionaryOpen.Length, loadedWrapper.isDictionaryOpen.Length);
            Array.Copy(loadedWrapper.isDictionaryOpen, _PlayerStatistics.isDictionaryOpen, copyLength);

            for (int i = copyLength; i < _PlayerStatistics.isDictionaryOpen.Length; i++)
            {
                _PlayerStatistics.isDictionaryOpen[i] = false;
            }

            dictionaryWrapper.isDictionaryOpen = CopyOrCreateArray(_PlayerStatistics.isDictionaryOpen);
            Debug.Log("Dictionary data loaded.");
        }

        private static void InitializeDefaultAndSave()
        {
            EnsurePlayerStatisticsArray(_PlayerStatistics.DictionaryNumMax);

            for (int i = 0; i < _PlayerStatistics.isDictionaryOpen.Length; i++)
            {
                _PlayerStatistics.isDictionaryOpen[i] = false;
            }

            dictionaryWrapper.isDictionaryOpen = CopyOrCreateArray(_PlayerStatistics.isDictionaryOpen);
            SaveDictionaryData();
            Debug.Log("Dictionary data was missing/invalid. Initialized with defaults.");
        }

        private static void EnsurePlayerStatisticsArray(int length)
        {
            if (_PlayerStatistics.isDictionaryOpen == null || _PlayerStatistics.isDictionaryOpen.Length != length)
            {
                _PlayerStatistics.isDictionaryOpen = new bool[length];
            }
        }

        private static bool[] CopyOrCreateArray(bool[] source)
        {
            if (source == null || source.Length != _PlayerStatistics.DictionaryNumMax)
            {
                return new bool[_PlayerStatistics.DictionaryNumMax];
            }

            bool[] copy = new bool[source.Length];
            Array.Copy(source, copy, source.Length);
            return copy;
        }
    }

    [Serializable]
    public class _DictionaryWrapper
    {
        public bool[] isDictionaryOpen = new bool[_PlayerStatistics.DictionaryNumMax];
    }
}
