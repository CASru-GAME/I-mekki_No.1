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

        [SerializeField] private GameObject[] _bar = new GameObject[11];
        [SerializeField] private Image _fadeImage;
        [SerializeField] private Image _blockerImage;
        private float moveDistance;
        private float duration = 0.5f;
        private float delay = 0.1f;
        private Vector3[] startPos;
        private Sequence barSeq;
        private Sequence fadeSeq;

        private bool isActive = false;

        [SerializeField] private TransitionColorChange _colorChanger;

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
            RectTransform canvasRect = GetComponentInParent<Canvas>().GetComponent<RectTransform>();
            moveDistance = canvasRect.rect.width * 1.5f;

            startPos = new Vector3[_bar.Length];

            for (int i = 0; i < _bar.Length; i++)
            {
                if (_bar[i] == null) continue;
                startPos[i] = _bar[i].transform.position;
            }
        }

        public void BarTransition(int type)
        {
            barSeq?.Kill();
            barSeq = DOTween.Sequence();

            if(type == 0)
            {
                //閉じる
                for (int i = 0; i < _bar.Length; i++)
                {
                    Transform t = _bar[i].transform;

                    barSeq.Insert(
                        i * delay,
                        t.DOMoveX(startPos[i].x + moveDistance, duration)
                        .SetEase(Ease.OutCubic)
                    );
                }
            }

            if(type == 1)
            {
                //開く
                for (int i = 0; i < _bar.Length; i++)
                {
                    Transform t = _bar[i].transform;

                    barSeq.Insert(
                        i * delay,
                        t.DOMoveX(startPos[i].x, duration)
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
            fadeSeq = DOTween.Sequence();

            if(type == 0)
            {
                fadeSeq.Append(
                    _fadeImage.DOFade(1f, 2f)
                );
            }

            if(type == 1)
            {
                fadeSeq.Append(
                    _fadeImage.DOFade(0f, 2f)
                );
                fadeSeq.OnComplete(() =>
                {
                    _blockerImage.raycastTarget = false;
                    isActive = false;
                });
            }
        }

        public void LoadSceneWithTransition(string sceneName, int colorFlag)
        {
            _colorChanger.ChangeColor(colorFlag);
            StartCoroutine(TransitionAndLoad(sceneName));
        }

        private IEnumerator TransitionAndLoad(string sceneName)
        {
            if(isActive) yield break;
            isActive = true;
            _blockerImage.raycastTarget = true;

            // 閉じる
            BarTransition(0);

            // アニメーションが終わるまで待機
            float waitTime = duration + (_bar.Length - 1) * delay;
            yield return new WaitForSeconds(waitTime);

            // シーン読み込み
            yield return SceneManager.LoadSceneAsync(sceneName);

            // 開く
            BarTransition(1);
        }
    }
}