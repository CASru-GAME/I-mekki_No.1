namespace App.Common._Data
{
    public static class _PlayerStatistics
    {
        static private int _coins;
        public static int Coins => _coins;
        static private int _dictionaryNum;
        public static int DictionaryNum => _dictionaryNum;
        static private int _clearedStageNum;
        public static int ClearedStageNum => _clearedStageNum;
        static public void ClearAll()
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
