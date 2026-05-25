using UnityEngine;
using TMPro;
using DG.Tweening;

namespace App.Game.UI
{
    public class PopupText : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;

        [SerializeField] private float _moveY = 80f;
        [SerializeField] private float _duration = 1.5f;

        private RectTransform _rectTransform;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        public void Show(string message, Color color)
        {
            _text.text = message;
            _text.color = color;

            _rectTransform.anchoredPosition = Vector2.zero;

            // alpha初期化
            Color c = _text.color;
            c.a = 1f;
            _text.color = c;

            Sequence seq = DOTween.Sequence();

            // 上移動
            seq.Join(
                _rectTransform.DOAnchorPosY(_moveY, _duration)
                .SetEase(Ease.OutCubic)
            );

            // フェードアウト
            seq.Join(
                _text.DOFade(0f, _duration)
            );

            seq.OnComplete(() =>
            {
                Destroy(gameObject);
            });
        }
    }
}