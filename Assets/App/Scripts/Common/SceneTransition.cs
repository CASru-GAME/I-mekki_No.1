using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace App.Scripts.Common
{
    public class SceneTransition : MonoBehaviour
    {
        public static SceneTransition Instance;

        [SerializeField] private GameObject[] _bar = new GameObject[11];
        [SerializeField] private Image _fadeImage;
        private float moveDistance;
        private float duration = 0.5f;
        private float delay = 0.1f;
        private Vector3[] startPos;
        private Sequence seq;

        void Awake()
        {
            // Singletonチェック
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        void Start()
        {
            RectTransform canvasRect = GetComponentInParent<Canvas>().GetComponent<RectTransform>();
            moveDistance = canvasRect.rect.width * 1.5f;

            startPos = new Vector3[_bar.Length];

            for (int i = 0; i < _bar.Length; i++)
            {
                if (_bar[i] == null) continue;
                startPos[i] = _bar[i].transform.position;
            }

            FadeTransition();
        }

        public void BarTransition()
        {
            seq?.Kill();
            seq = DOTween.Sequence();

            //閉じる
            for (int i = 0; i < _bar.Length; i++)
            {
                Transform t = _bar[i].transform;

                seq.Insert(
                    i * delay,
                    t.DOMoveX(startPos[i].x + moveDistance, duration)
                    .SetEase(Ease.OutCubic)
                );
            }

            float closeTime = (_bar.Length - 1) * delay + duration;

            //開く
            for (int i = 0; i < _bar.Length; i++)
            {
                Transform t = _bar[i].transform;

                seq.Insert(
                    closeTime + i * delay,
                    t.DOMoveX(startPos[i].x, duration)
                    .SetEase(Ease.InCubic)
                );
            }
        }

        public void FadeTransition()
        {
            seq?.Kill();
            seq = DOTween.Sequence();

            seq.Append(
                _fadeImage.DOFade(1f, 3f)
            );

            seq.Append(
                _fadeImage.DOFade(0f, 3f)
            );

            seq.SetUpdate(true);
        }
    }
}