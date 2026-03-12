using App.Scripts.Common.UI;

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
        static private int dictionaryNum = 21;
        static public int DictionaryNumMax => dictionaryNum;
        static public bool[] isDictionaryOpen = new bool[dictionaryNum];
        static private bool _gameCleared;
        static public bool GameCleared => _gameCleared;
        static public void ClearStageData()
        {
            _coins = 0;
            _dictionaryNum = 0;
            _clearedStageNum = 0;
            _gameCleared = false;
            UnityEngine.Debug.Log("Player statistics cleared: Coins, DictionaryNum, ClearedStageNum reset to 0.");
        }
        static public void AddCoins(int coins)
        {
            _coins += coins;
            UnityEngine.Debug.Log($"Coins added: {coins}, Total: {_coins}");
        }
        static public void AddDictionaryNum(int dictionaryNum)
        {
            _dictionaryNum += dictionaryNum;
            UnityEngine.Debug.Log($"Dictionary items added: {dictionaryNum}, Total: {_dictionaryNum}");
        }
        static public void AddClearedStageNum(int clearedStageNum)
        {
            _clearedStageNum += clearedStageNum;
            UnityEngine.Debug.Log($"Cleared stages added: {clearedStageNum}, Total: {_clearedStageNum}");
        }
        static public void SetGameCleared(bool gameCleared)
        {
            _gameCleared = gameCleared;
            SceneTransition.Instance.LoadSceneWithTransition(SceneLoader.Instance.sceneNames.ResultScene, 0);
            SceneLoader.Instance.GetTitleBGM().Play();
        }
    }
}
