using App.Common._Data;
using App.Game.UI;
using UnityEngine;

namespace App.Game.Field
{
    public class Dictionary : MonoBehaviour
    {
        [SerializeField] private int dictionaryNumber;
        [SerializeField] private PopupManager _popupManager;

        public void Start()
        {
            if (dictionaryNumber < 0 || dictionaryNumber > _PlayerStatistics.DictionaryNumMax)
            {
                Debug.LogError($"Invalid dictionary number: {dictionaryNumber}. It should be between 0 and {_PlayerStatistics.DictionaryNumMax - 1}.");
                return;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            TryCollect(collision);
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            TryCollect(collision);
        }

        private void TryCollect(Collider2D collision)
        {
            if (collision.GetComponent<App.Game.Player.Player>() == null) return;

            _PlayerStatistics.AddDictionaryNum(1);
            _PlayerStatistics.isDictionaryOpen[dictionaryNumber] = true;

            //取得ポップアップの表示
            if(_popupManager != null)
            {
                var dictionaryPos = transform.position;
                PopupItem _item = _popupManager.PopupDatabase.Items[dictionaryNumber];
                _popupManager.ShowPopup(dictionaryPos, _item.PopupText, _item.TextColor);
            }

            _JsonDataManager.SaveDictionaryData();
            gameObject.SetActive(false);

            collision.GetComponent<App.Game.Player.PlayerSE>().PlayDictionary();
        }
    }
}
