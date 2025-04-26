using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

namespace App.Scripts.Title
{
    public class BookWindowShow : MonoBehaviour
    {
        [SerializeField] private GameObject _bookWindow;
        [SerializeField] private GameObject _shadow;
        [SerializeField] private GameObject _bookExplations;
        private CanvasGroup _bookExpCanvasGroup;
        private RectTransform _windowTransfom;
        private BookWindow _bookWindowScript;

        private void Start()
        {
            if (_bookWindow != null)
            {
                _bookWindowScript = _bookWindow.GetComponent<BookWindow>();
            }
            _windowTransfom = _bookWindow.GetComponent<RectTransform>();
            _bookExpCanvasGroup = _bookExplations.GetComponent<CanvasGroup>();
        }

        public void OnClickDelete()
        {
            _bookExpCanvasGroup.DOFade(0f, 0.25f);
            _windowTransfom.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InBack).OnComplete(() =>
            {
                _bookWindow.SetActive(false);
                _shadow.SetActive(false);
                _bookExplations.SetActive(false);
                _shadow.GetComponent<Image>().DOFade(0f, 0.5f);
            });
        }

        public void OnClickBook(int _bookNumber)
        {
            _windowTransfom.localScale = Vector3.zero; 
            _bookWindow.SetActive(true); 
            _shadow.SetActive(true); 
            _shadow.GetComponent<Image>().DOFade(0.5f, 0.5f);
            _windowTransfom.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack).OnComplete(() =>
            {
                if (_bookWindowScript != null)
                {
                    _bookWindowScript.DecideBookNumber(_bookNumber);
                    _bookWindowScript.CheckShowArrow();
                }
                _bookExplations.SetActive(true);
                _bookExpCanvasGroup.DOFade(1f, 0.25f);
            });
        }
    }
}
