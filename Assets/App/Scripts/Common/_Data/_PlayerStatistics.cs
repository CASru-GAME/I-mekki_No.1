namespace App.Common._Data
{
    public static class _PlayerStatistics
    {
        static private int _coins;
        public static int Coins => _coins;
        static private int _dictionallyNum;
        public static int DictionallyNum => _dictionallyNum;
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
            _coins += coins;
            if (_coins == null)
            {
                _coins = 0;
            }
        }
        static public void AddDictionallyNum(int dictionallyNum)
        {
            _dictionallyNum += dictionallyNum;
            if (_dictionallyNum == null)
            {
                _dictionallyNum = 0;
            }
        }
        static public void AddClearedStageNum(int clearedStageNum)
        {
            _clearedStageNum += clearedStageNum;
            if (_clearedStageNum == null)
            {
                _clearedStageNum = 0;
            }
        }
    }
}