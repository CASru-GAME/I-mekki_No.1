using System.Collections.Generic;

namespace App.Common._Data
{
    public static class _PlayerStatistics
    {
        static private int _coins;
        static public int Coins => _coins;
        static private int _dictionaryNum;
        static public int DictionaryNum => _dictionaryNum;
        static private int _clearedStageNum;
        static public int ClearedStageNum => _clearedStageNum;
        static private int dictionaryNum = 24;
        static public int DictionaryNumMax => dictionaryNum;
        static public bool[] isDictionaryOpen = new bool[dictionaryNum];
        static public void ClearStageData()
        {
            _coins = 0;
            _dictionaryNum = 0;
            _clearedStageNum = 0;
        }
        static public void AddCoins(int coins)
        {
            _coins += coins;
        }
        static public void AddDictionaryNum(int dictionaryNum)
        {
            _dictionaryNum += dictionaryNum;
        }
        static public void AddClearedStageNum(int clearedStageNum)
        {
            _clearedStageNum += clearedStageNum;
        }

    }
}
