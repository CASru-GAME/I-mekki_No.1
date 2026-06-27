using UnityEngine;
using TMPro;
using DG.Tweening;
using App.Scripts.Title;

namespace App.Game.UI
{
    public class ShowNotification : MonoBehaviour
    {
        [SerializeField] private RectTransform _panel;
        [SerializeField] private TextMeshProUGUI _titleText;

        private float _slideDuration = 0.5f;
        private float _stayTime = 2f;

        private float _offsetXRatio = 2.0f; // 画面幅の150%だけ右に置いておく

        private Vector2 _shownPos;
        private Vector2 _hiddenPos;

        [SerializeField] private DictionaryDatabase _database;
        [SerializeField] private bool isTest = false;

        void Start()
        {
            // 表示位置（現在の anchoredPosition）
            _shownPos = _panel.anchoredPosition;

            // 画面比率 → ピクセルに変換
            float _offsetX = Screen.width * _offsetXRatio;

            // 非表示位置（右外）
            _hiddenPos = _shownPos + new Vector2(_offsetX, 0);

            // 初期状態は非表示位置
            _panel.anchoredPosition = _hiddenPos;
        }

        void Update()
        {
            if(isTest)
            {
                Show(1);
                isTest = false;
            }
        }

        public void Show(int index)
        {
            var _item = _database.Items[index];
            _titleText.text = _item.Title;

            _panel.DOKill();

            Sequence _seq = DOTween.Sequence();

            // スライドイン
            _seq.Append(_panel.DOAnchorPos(_shownPos, _slideDuration).SetEase(Ease.OutCubic));

            // 停止
            _seq.AppendInterval(_stayTime);

            // スライドアウト
            _seq.Append(_panel.DOAnchorPos(_hiddenPos, _slideDuration).SetEase(Ease.InCubic));
        }
    }
}