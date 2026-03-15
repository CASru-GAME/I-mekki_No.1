using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

namespace App.Scripts.Title
{
    public class SelectLevel : MonoBehaviour
    {
        [SerializeField] private GameObject _rightArrow;
        [SerializeField] private GameObject _leftArrow;
        [SerializeField] private GameObject[] _selectPanels; // 3つのパネルを配列で指定
        
        private bool isMove = false;
        private float _panelDistance;
        private int _currentPanelIndex = 0; // 現在のパネルインデックス

        private Vector2 _dragStartPos;
        private float _swipeThreshold = 100f; // スワイプ判定距離(px)
        private float _followLimit = 150f;    // ドラッグ中の追従最大距離(px)

        private Vector3 _initialPos;          // ドラッグ開始時の位置
        private bool isDragging = false;

        void Start()
        {
            // 最初の2つのパネル間の距離を計算
            if (_selectPanels.Length >= 2)
            {
                _panelDistance = Vector3.Distance(
                    _selectPanels[0].transform.position,
                    _selectPanels[1].transform.position
                );
            }
            _currentPanelIndex = 0;
            UpdateArrowState();
        }

        void Update()
        {
            if (isMove) return;

            // ===== スマホタッチ =====
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        StartDrag(touch.position);
                        break;

                    case TouchPhase.Moved:
                        DragMove(touch.position);
                        break;

                    case TouchPhase.Ended:
                        DragEnd(touch.position);
                        break;
                }
            }
            // ===== マウス操作 =====
            else
            {
                if (Input.GetMouseButtonDown(0))
                {
                    StartDrag(Input.mousePosition);
                }
                else if (Input.GetMouseButton(0))
                {
                    DragMove(Input.mousePosition);
                }
                else if (Input.GetMouseButtonUp(0))
                {
                    DragEnd(Input.mousePosition);
                }
            }
        }

        void StartDrag(Vector2 pos)
        {
            isDragging = true;
            _dragStartPos = pos;
            _initialPos = transform.position;
        }

        void DragMove(Vector2 currentPos)
        {
            if (!isDragging) return;

            float diffX = currentPos.x - _dragStartPos.x;

            float followX = Mathf.Clamp(diffX, -_followLimit, _followLimit);

            transform.position = _initialPos + new Vector3(followX, 0, 0);
        }

        void DragEnd(Vector2 endPos)
        {
            if (!isDragging) return;

            isDragging = false;

            float diffX = endPos.x - _dragStartPos.x;

            if (Mathf.Abs(diffX) >= _swipeThreshold)
            {
                if (diffX < 0)
                    Onclick("right");
                else
                    Onclick("left");
            }
            else
            {
                transform.DOMove(_initialPos, 0.25f).SetEase(Ease.OutQuad);
            }
        }

        public void Onclick(string _direction)
        {
            if(isMove) return;

            isMove = true;

            // パネル間の距離分移動
            Vector3 moveDirection = Vector3.zero;

            if (_direction == "right" && _currentPanelIndex < _selectPanels.Length - 1)
            {
                moveDirection = new Vector3(-_panelDistance, 0, 0);
                _currentPanelIndex++;
            }
            else if (_direction == "left" && _currentPanelIndex > 0)
            {
                moveDirection = new Vector3(_panelDistance, 0, 0);
                _currentPanelIndex--;
            }
            else
            {
                isMove = false;
                return;
            }

            // DoTweenで移動アニメーションを実行
            this.transform.DOMove(this.transform.position + moveDirection, 0.5f).OnComplete(() =>
            {
                UpdateArrowState();
                isMove = false;
            });
        }

        private void UpdateArrowState()
        {
            // 左矢印：最初のパネルなら非表示
            _leftArrow.SetActive(_currentPanelIndex > 0);

            // 右矢印：最後のパネルなら非表示
            _rightArrow.SetActive(_currentPanelIndex < _selectPanels.Length - 1);

            Debug.Log("Current Panel Index: " + _currentPanelIndex);
        }
    }
}
