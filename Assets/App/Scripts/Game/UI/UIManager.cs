using TMPro;
using UnityEngine;
using App.Common._Data;

namespace App.Game.UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI dictionaryNumber;
        [SerializeField] private TextMeshProUGUI coinNumber;
        [SerializeField] private TextMeshProUGUI lifeNumber;

        private void Update()
        {
            dictionaryNumber.text = _PlayerStatistics.DictionaryNum.ToString();
            coinNumber.text = _PlayerStatistics.Coins.ToString();
            lifeNumber.text = _PlayerStatus.GetHp().ToString();
        }
    }
}
