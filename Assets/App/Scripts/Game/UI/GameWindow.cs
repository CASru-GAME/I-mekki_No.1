using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace App.Game.UI
{
    public class GameWindow : MonoBehaviour
    {
        //メニューボタンに付ける
        [SerializeField] private GameObject _window;
        [SerializeField] private GameObject _shadow;
        private RectTransform _windowTransfom;

        void Start()
        {
            _windowTransfom = _window.GetComponent<RectTransform>();
        }

        public void OnClickMenu()
        {
            Time.timeScale = 0f; //ゲーム停止

            _windowTransfom.localScale = Vector3.zero;
            _window.SetActive(true);
            _shadow.SetActive(true);

            _shadow.GetComponent<Image>().DOFade(0.5f, 0.5f)
                .SetUpdate(true);

            _windowTransfom.DOScale(Vector3.one, 0.5f)
                .SetEase(Ease.OutBack)
                .SetUpdate(true);
        }

        public void OnClickDelete()
        {
            _windowTransfom.DOScale(Vector3.zero, 0.5f)
                .SetEase(Ease.InBack)
                .SetUpdate(true)
                .OnComplete(() =>
                {
                    _window.SetActive(false);
                    _shadow.GetComponent<Image>().DOFade(0f, 0.5f).SetUpdate(true);
                    _shadow.SetActive(false);

                    Time.timeScale = 1f;
                });
        }
    }
}
