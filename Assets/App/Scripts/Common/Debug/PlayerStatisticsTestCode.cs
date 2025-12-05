using UnityEngine;
using App.Common._Data;

namespace App.Common.Debug
{
    public class PlayerStatisticsTestCode : MonoBehaviour
    {
        public void CheckNumberOfAll()
        {
            UnityEngine.Debug.Log("Current number of coins: " + _PlayerStatistics.Coins);
            UnityEngine.Debug.Log("Current number of dictionary entries: " + _PlayerStatistics.DictionaryNum);
            UnityEngine.Debug.Log("Current number of cleared stages: " + _PlayerStatistics.ClearedStageNum);
        }

        public void ClearAllData()
        {
            _PlayerStatistics.ClearAll();
            UnityEngine.Debug.Log("All player statistics have been cleared.");
        }

        public void AddTestData()
        {
            _PlayerStatistics.AddCoins(10);
            _PlayerStatistics.AddDictionaryNum(5);
            _PlayerStatistics.AddClearedStageNum(2);
            UnityEngine.Debug.Log("Added test data: 10 coins, 5 dictionary entries, 2 cleared stages.");
        }
    }
}
