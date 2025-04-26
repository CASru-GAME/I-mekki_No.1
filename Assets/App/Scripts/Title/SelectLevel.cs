using UnityEngine;
using DG.Tweening;

namespace App.Scripts.Title
{
    public class SelectLevel : MonoBehaviour
    {
        [SerializeField] private GameObject _rightArrow;
        [SerializeField] private GameObject _leftArrow;
        [SerializeField] private float _moveDistance;

        [SerializeField] private float _swipeThreshold = 50f; // スワイプの閾値（ピクセル数）
        private Vector2 _startTouchPosition; // タッチ開始位置
        private Vector2 _endTouchPosition; // タッチ終了位置

        private bool _runAnimation = false;

        void FixedUpdate()
        {
            //指でスライドしたときにも画面遷移できるようにする
            // タッチ開始
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                _startTouchPosition = Input.GetTouch(0).position;
            }

            // タッチ終了
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                _endTouchPosition = Input.GetTouch(0).position;
                DetectSwipeDirection();
            }
        }

        private void DetectSwipeDirection()
        {
            float _horizontalSwipe = _endTouchPosition.x - _startTouchPosition.x;

            if (Mathf.Abs(_horizontalSwipe) > _swipeThreshold)
            {
                if (_horizontalSwipe > 0)
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
            //右矢印が押されたら右に移動
            //左矢印が押されたら左に移動
            _runAnimation = true;
            Vector3 _moveDirection = Vector3.zero;
            if (_direction == "right")
            {
                _moveDirection = new Vector3(-_moveDistance, 0, 0);
            }
            else if (_direction == "left")
            {
                _moveDirection = new Vector3(_moveDistance, 0, 0);
            }

            // DoTweenで移動アニメーションを実行
            this.transform.DOMove(this.transform.position + _moveDirection, 0.5f).OnComplete(() =>
            {
                // アニメーション終了後に矢印の状態を更新
                if(this.transform.position.x >= 0)
                {
                    _leftArrow.SetActive(false);
                }
                else
                {
                    _leftArrow.SetActive(true);
                }
                if (this.transform.position.x <= -_moveDistance*2+500)
                {
                    _rightArrow.SetActive(false);
                }
                else
                {
                    _rightArrow.SetActive(true);
                }
                _runAnimation = false;
            });
        }
    }
}
