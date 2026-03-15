using NUnit.Framework;
using System.IO;
using UnityEngine;
using App.Common._Data;

namespace App.Common._Data.Tests
{
    public class _JsonDataManagerTests
    {
        [Test]
        public void SaveDictionaryData_SavesFileSuccessfully()
        {
            // Arrange
            _PlayerStatistics.isDictionaryOpen[0] = true;
            _PlayerStatistics.isDictionaryOpen[5] = true;

            // Act
            _JsonDataManager.SaveDictionaryData();

            // Assert
            string filePath = Application.persistentDataPath + "/isDictionaryOpen.json";
            Assert.IsTrue(File.Exists(filePath), "セーブファイルが作成されていません");

            _JsonDataManager.LoadDictionaryData();
            Assert.IsTrue(_PlayerStatistics.isDictionaryOpen[0], "インデックス0が正しく保存されていません");
            Assert.IsTrue(_PlayerStatistics.isDictionaryOpen[5], "インデックス5が正しく保存されていません");
            Assert.IsFalse(_PlayerStatistics.isDictionaryOpen[1], "インデックス1がfalseではありません");
        }

        [Test]
        public void LoadDictionaryData_LoadsDataCorrectly()
        {
            // Arrange
            _PlayerStatistics.isDictionaryOpen[0] = true;
            _PlayerStatistics.isDictionaryOpen[3] = true;
            _JsonDataManager.SaveDictionaryData();

            // 読み込み前にリセット
            for (int i = 0; i < _PlayerStatistics.isDictionaryOpen.Length; i++)
            {
                _PlayerStatistics.isDictionaryOpen[i] = false;
            }

            // Act
            _JsonDataManager.LoadDictionaryData();

            // Assert
            Assert.IsTrue(_PlayerStatistics.isDictionaryOpen[0], "インデックス0が正しく読み込まれていません");
            Assert.IsTrue(_PlayerStatistics.isDictionaryOpen[3], "インデックス3が正しく読み込まれていません");
            Assert.IsFalse(_PlayerStatistics.isDictionaryOpen[1], "インデックス1がfalseではありません");
        }

        [Test]
        public void LoadDictionaryData_CreatesFileIfNotExists()
        {
            // Arrange
            string filePath = Application.persistentDataPath + "/isDictionaryOpen.json";
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            // Act
            _JsonDataManager.LoadDictionaryData();

            // Assert
            Assert.IsTrue(File.Exists(filePath), "ファイルが作成されていません");
        }
    }
}
