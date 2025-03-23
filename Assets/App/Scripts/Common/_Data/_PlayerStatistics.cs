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
            _dictionallyNum = 0;
            _clearedStageNum = 0;
        }
        static public void AddCoins(int coins)
        {
            if (_coins == null)
            {
                _coins = 1;
            }
            else
            {
                _coins += coins;
            }
        }
        static public void AddDictionaryNum(int dictionaryNum)
        {
            if (_dictionaryNum == null)
            {
                _dictionaryNum = 1;
            }
            else
            {
                _dictionaryNum += dictionaryNum;
            }
        }
        static public void AddClearedStageNum(int clearedStageNum)
        {
            if (_clearedStageNum == null)
            {
                _clearedStageNum = 1;
            }
            else
            {
                _clearedStageNum += clearedStageNum;
            }
        }
    }
}