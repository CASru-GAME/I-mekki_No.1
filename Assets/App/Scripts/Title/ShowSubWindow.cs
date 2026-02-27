using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace App.Scripts.Title
{
    public class ShowSubWindow : MonoBehaviour
    {
        [SerializeField] private GameObject _window;
        [SerializeField] private GameObject _shadow;
        private RectTransform _windowTransfom;

        void Start()
        {
            _windowTransfom = _window.GetComponent<RectTransform>();
        }

        public void OnClickDictonary()
        {
            _windowTransfom.localScale = Vector3.zero; // 初期サイズを0に設定
            _window.SetActive(true); // ウインドウをアクティブに
            _shadow.SetActive(true); //影をアクティブに
            _shadow.GetComponent<Image>().DOFade(0.5f, 0.5f);
            _windowTransfom.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack); // サイズを拡大してアニメーション
        }

        public void OnClickDelete()
        {
            _windowTransfom.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InBack).OnComplete(() =>
            {
                _window.SetActive(false);
                _shadow.GetComponent<Image>().DOFade(0f, 0.5f).OnComplete(() =>
                {
                    _shadow.SetActive(false);
                });
            });
        }
    }
}