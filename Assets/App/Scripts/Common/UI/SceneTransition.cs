using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using DG.Tweening;

namespace App.Scripts.Common.UI
{
    public class SceneTransition : MonoBehaviour
    {
        public static SceneTransition Instance;

        [SerializeField] private RectTransform[] _bars;
        [SerializeField] private Image _fadeImage;
        [SerializeField] private Image _blockerImage;
        [SerializeField] private TransitionColorChange _colorChanger;

        private float duration = 0.5f;
        private float delay = 0.1f;

        private float startAnchoredX;
        private float safeWidth;

        private Sequence barSeq;
        private Sequence fadeSeq;

        private bool isActive = false;
        private RectTransform canvasRect;

        void Awake()
        {
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
            Canvas canvas = GetComponentInParent<Canvas>();
            if (canvas == null)
            {
                Debug.LogError("SceneTransition: Canvas not found.");
                return;
            }

            canvasRect = canvas.GetComponent<RectTransform>();

            UpdateBarSize();

            if (_bars.Length > 0 && _bars[0] != null)
            {
                startAnchoredX = _bars[0].anchoredPosition.x;
            }
        }

        void OnRectTransformDimensionsChange()
        {
            if (canvasRect == null) return;
            UpdateBarSize();
        }

        void UpdateBarSize()
        {
            float canvasWidth = canvasRect.rect.width;

            // 画面外まで覆う安全幅
            safeWidth = canvasWidth * 1.5f;

            foreach (var bar in _bars)
            {
                if (bar == null) continue;

                bar.SetSizeWithCurrentAnchors(
                    RectTransform.Axis.Horizontal,
                    safeWidth
                );
            }
        }

        public void BarTransition(int type)
        {
            barSeq?.Kill();
            barSeq = DOTween.Sequence().SetUpdate(true);
            float canvasHalf = canvasRect.rect.width / 2f;

            if (type == 0) // 閉じる
            {
                for (int i = 0; i < _bars.Length; i++)
                {
                    if (_bars[i] == null) continue;

                    float barHalf = _bars[i].rect.width / 2f;
                    float targetX = barHalf;

                    barSeq.Insert(
                        i * delay,
                        _bars[i].DOAnchorPosX(targetX, duration)
                            .SetEase(Ease.OutCubic)
                    );
                }
            }
            else // 開く
            {
                for (int i = 0; i < _bars.Length; i++)
                {
                    if (_bars[i] == null) continue;

                    barSeq.Insert(
                        i * delay,
                        _bars[i].DOAnchorPosX(startAnchoredX, duration)
                            .SetEase(Ease.InCubic)
                    );
                }

                barSeq.OnComplete(() =>
                {
                    _blockerImage.raycastTarget = false;
                    isActive = false;
                });
            }
        }

        public void FadeTransition(int type)
        {
            fadeSeq?.Kill();
            fadeSeq = DOTween.Sequence().SetUpdate(true);

            if (type == 0)
            {
                fadeSeq.Append(_fadeImage.DOFade(1f, 2f));
            }
            else
            {
                fadeSeq.Append(_fadeImage.DOFade(0f, 2f));
                fadeSeq.OnComplete(() =>
                {
                    _blockerImage.raycastTarget = false;
                    isActive = false;
                });
            }
        }

        public void LoadSceneWithTransition(string sceneName, int colorFlag)
        {
            if (_colorChanger != null)
            {
                _colorChanger.ChangeColor(colorFlag);
            }

            StartCoroutine(TransitionAndLoad(sceneName));
        }

        private IEnumerator TransitionAndLoad(string sceneName)
        {
            if (isActive) yield break;

            isActive = true;
            _blockerImage.raycastTarget = true;

            BarTransition(0);

            float waitTime = duration + (_bars.Length - 1) * delay;
            yield return new WaitForSecondsRealtime(waitTime);

            yield return SceneManager.LoadSceneAsync(sceneName);

            BarTransition(1);
        }
    }
}