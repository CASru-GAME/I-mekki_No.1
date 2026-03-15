using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

namespace App.Title
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
            //RectTransformを取得
            if (_bookWindow != null)
            {
                _bookWindowScript = _bookWindow.GetComponent<BookWindow>();
            }
            _windowTransfom = _bookWindow.GetComponent<RectTransform>();
            _bookExpCanvasGroup = _bookExplations.GetComponent<CanvasGroup>();
        }

        public void OnClickDelete()
        {
            //×ボタンでウインドウを消す
            _bookWindowScript.CheckShowArrow(1);
            _bookExpCanvasGroup.DOFade(0f, 0.25f);
            _windowTransfom.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InBack).OnComplete(() =>
            {
                _shadow.SetActive(false);
                _bookExplations.SetActive(false);
                _shadow.GetComponent<Image>().DOFade(0f, 0.5f);
            });
        }

        public void OnClickBook(int _bookNumber)
        {
            //本をクリックしたときにウインドウを表示
            _windowTransfom.localScale = Vector3.zero; 
            _shadow.SetActive(true); 
            _shadow.GetComponent<Image>().DOFade(0.5f, 0.5f);
            _windowTransfom.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack).OnComplete(() =>
            {
                //bookNumberを渡す
                _bookWindowScript.DecideBookNumber(_bookNumber);
                
                _bookExplations.SetActive(true);
                _bookExpCanvasGroup.DOFade(1f, 0.25f).OnComplete(() =>
                {
                    _bookWindowScript.CheckShowArrow(0);
                });
            });
        }
    }
}
