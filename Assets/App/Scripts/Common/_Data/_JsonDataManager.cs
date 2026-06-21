using System;
using UnityEngine;

namespace App.Common._Data
{
    public static class _JsonDataManager
    {
        private const string PlayerPrefsKey = "isDictionaryOpen_json";

        private static _DictionaryWrapper dictionaryWrapper = new _DictionaryWrapper();

        public static void SaveDictionaryData()
        {

            dictionaryWrapper.isDictionaryOpen = CopyOrCreateArray(_PlayerStatistics.isDictionaryOpen);
            string json = JsonUtility.ToJson(dictionaryWrapper, true);

            PlayerPrefs.SetString(PlayerPrefsKey, json);
            PlayerPrefs.Save();

        }

        public static void LoadDictionaryData()
        {

            if (!PlayerPrefs.HasKey(PlayerPrefsKey))
            {
                InitializeDefaultAndSave();
                return;
            }

            string json = PlayerPrefs.GetString(PlayerPrefsKey);
            var loadedWrapper = string.IsNullOrEmpty(json)
                ? null
                : JsonUtility.FromJson<_DictionaryWrapper>(json);

            if (loadedWrapper?.isDictionaryOpen == null || loadedWrapper.isDictionaryOpen.Length == 0)
            {
                InitializeDefaultAndSave();
                return;
            }

            EnsurePlayerStatisticsArray(_PlayerStatistics.DictionaryNumMax);

            int copyLength = Mathf.Min(_PlayerStatistics.isDictionaryOpen.Length, loadedWrapper.isDictionaryOpen.Length);
            Array.Copy(loadedWrapper.isDictionaryOpen, _PlayerStatistics.isDictionaryOpen, copyLength);

            for (int i = copyLength; i < _PlayerStatistics.isDictionaryOpen.Length; i++)
            {
                _PlayerStatistics.isDictionaryOpen[i] = false;
            }

            dictionaryWrapper.isDictionaryOpen = CopyOrCreateArray(_PlayerStatistics.isDictionaryOpen);
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
