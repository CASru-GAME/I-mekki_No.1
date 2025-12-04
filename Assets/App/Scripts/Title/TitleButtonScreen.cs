using UnityEngine;
using DG.Tweening;

namespace App.Scripts.Title
{
    public class TitleButtonScreen : MonoBehaviour
    {
        [SerializeField] private GameObject _targetScreen;
        [SerializeField] private GameObject _nowScreen;
        [SerializeField] private GameObject _waitScreen;
        private Vector3 _mainPos;
        private Vector3 _nowPos;
        private Vector3 _waitPos;
        private Vector3 _defaultScale;
        private bool isAnimation = false;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            //開始時の場所を記憶
            _mainPos = _targetScreen.transform.position;
            _nowPos = _nowScreen.transform.position;
            _waitPos = _waitScreen.transform.position;

            _defaultScale = transform.localScale;
        }

        public void OnClickMenu()
        {
            if(isAnimation) return;

            isAnimation = true;

            //ボタンのアニメーション
            this.transform.DOScale(1.1f, 0.5f).SetEase(Ease.OutElastic).OnComplete(()=> 
            {
                transform.localScale = _defaultScale;
            });

            //メインメニュー上のボタンが押されたら、遷移先の画面を表示
            _nowScreen.transform.position = _waitPos;
            _nowScreen.transform.DOMove(_mainPos, 0.5f).SetEase(Ease.InOutQuad);

            isAnimation = false;
        }

        public void OnClickBack()
        {
            if(isAnimation) return;

            isAnimation = true;

            //戻るボタンが押されたら、遷移前の場所に戻る
            _nowScreen.transform.DOMove(_waitPos, 0.5f).OnComplete(() =>
            {
                _nowScreen.transform.position = _nowPos;
            });

            isAnimation = false;
        }
    }
}