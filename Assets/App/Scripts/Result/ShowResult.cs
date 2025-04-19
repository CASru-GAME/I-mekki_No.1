using UnityEngine;
using DG.Tweening;

namespace App.Scripts.Result
{
    public class ShowResult : MonoBehaviour
    {
        [SerializeField] private GameObject _resultTitle;
        [SerializeField] private GameObject _resultView;
        [SerializeField] private GameObject _goTitleButton;
        [SerializeField] private Vector3 _resultTitlePos;
        [SerializeField] private Vector3 _resultViewPos;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            _goTitleButton.transform.localScale = Vector3.zero;

            //リザルトの表示
            _resultTitle.transform.DOMove(_resultTitle.transform.position + _resultTitlePos, 0.5f).OnComplete(() =>
            {
                _resultView.transform.DOMove(_resultView.transform.position + _resultViewPos, 0.5f).OnComplete(() =>
                {
                    //リザルトの表示が終わったら、ボタンを表示
                    _goTitleButton.transform.DOScale(1.0f, 0.5f).SetEase(Ease.OutBounce);
                });
            });
        }

        public void OnclickGoTitle()
        {
            //タイトル場面にもどる
        }
    }
}
