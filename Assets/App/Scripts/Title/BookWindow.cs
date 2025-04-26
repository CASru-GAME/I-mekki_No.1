using UnityEngine;
using DG.Tweening;

namespace App.Scripts.Title
{
    public class BookWindow : MonoBehaviour
    {
        [SerializeField] private GameObject _rightArrow;
        [SerializeField] private GameObject _leftArrow;
        [SerializeField] private float _moveDistance;
        [SerializeField] private GameObject _mainPos;
        [SerializeField] private GameObject _waitPosRight;
        [SerializeField] private GameObject _waitPosLeft;

        [SerializeField] private float swipeThreshold = 50f; // スワイプの閾値（ピクセル数）
        private Vector2 startTouchPosition; // タッチ開始位置
        private Vector2 endTouchPosition; // タッチ終了位置

        [SerializeField] private GameObject[] _books; 
        [SerializeField]private int _bookNumber = 0;
        private bool _runAnimation = false;

        public void DecideBookNumber(int _number)
        {
            _bookNumber = _number;
            _books[_number].transform.position = _mainPos.transform.position;
            for(int i = 0; i < _books.Length; i++)
            {
                if (i != _number)
                {
                    _books[i].transform.position = _waitPosRight.transform.position;
                }
            }
        }

        public void CheckShowArrow()
        {
            _leftArrow.SetActive(_bookNumber > 0);
            _rightArrow.SetActive(_bookNumber < _books.Length - 1);
        }

        void FixedUpdate()
        {
            //指でスライドしたときにも画面遷移できるようにする
            // タッチ開始
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                startTouchPosition = Input.GetTouch(0).position;
            }

            // タッチ終了
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                endTouchPosition = Input.GetTouch(0).position;
                DetectSwipeDirection();
            }
        }

        private void DetectSwipeDirection()
        {
            float horizontalSwipe = endTouchPosition.x - startTouchPosition.x;

            if (Mathf.Abs(horizontalSwipe) > swipeThreshold)
            {
                if (horizontalSwipe > 0)
                {
                    OnClick("left"); // 右スワイプで左へ移動
                }
                else
                {
                    OnClick("right"); // 左スワイプで右へ移動
                }
            }
        }

        public void OnClick(string _direction)
        {
            if (_runAnimation) return; 
            Vector3 _moveDirection = Vector3.zero;
            
            if (_direction == "right" && _bookNumber < _books.Length - 1)
            {
                _moveDirection = new Vector3(-_moveDistance, 0, 0);
            }
            else if (_direction == "left" && _bookNumber > 0)
            {
                _moveDirection = new Vector3(_moveDistance, 0, 0);
            }

            // 表示されるエントリーを更新
            UpdateDisplayedEntry(_moveDirection);
        }

        private void UpdateDisplayedEntry(Vector3 _moveDirection)
        {
            if(_moveDirection.x >= 0) //ページを右へ移動、左矢印を押した
            {
                _runAnimation = true;
                _books[_bookNumber-1].transform.position = _waitPosLeft.transform.position;
                _books[_bookNumber-1].transform.DOMove(_books[_bookNumber-1].transform.position + _moveDirection, 0.5f);
                _books[_bookNumber].transform.DOMove(_books[_bookNumber].transform.position + _moveDirection, 0.5f).OnComplete(() =>
                {
                    _bookNumber --;
                    CheckShowArrow();
                    _runAnimation = false;
                });
                
            }
            else if(_moveDirection.x < 0) //ページを左へ移動、右矢印を押した
            {
                _runAnimation = true;
                _books[_bookNumber+1].transform.DOMove(_books[_bookNumber+1].transform.position + _moveDirection, 0.5f);
                _books[_bookNumber].transform.DOMove(_books[_bookNumber].transform.position + _moveDirection, 0.5f).OnComplete(() =>
                {
                    _books[_bookNumber].transform.position = _waitPosRight.transform.position;
                    _bookNumber ++;
                    CheckShowArrow();
                    _runAnimation = false;
                });
            }
        }

    }
}