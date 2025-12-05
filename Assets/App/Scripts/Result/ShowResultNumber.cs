using UnityEngine;
using App.Common._Data;
using TMPro;
using Unity.Multiplayer.Center.Common;

namespace App.Result
{
    public class ShowResultNumber : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _coinNumberText;
        [SerializeField] private TextMeshProUGUI _dictionaryNumberText;
        [SerializeField] private TextMeshProUGUI _clearedStageNumberText;

        void Start()
        {
            //プレイヤーの統計情報を取得して表示
            int coinNumber = _PlayerStatistics.Coins;
            int dictionaryNumber = _PlayerStatistics.DictionaryNum;
            int clearedStageNumber = _PlayerStatistics.ClearedStageNum;

            _coinNumberText.text = coinNumber.ToString();
            _dictionaryNumberText.text = dictionaryNumber.ToString();
            _clearedStageNumberText.text = clearedStageNumber.ToString();
        }

        void Update()
        {
            _clearedStageNumberText.text = _PlayerStatistics.ClearedStageNum.ToString();
            _dictionaryNumberText.text = _PlayerStatistics.DictionaryNum.ToString();
            _coinNumberText.text = _PlayerStatistics.Coins.ToString();
        }
    }
}
