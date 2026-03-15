using UnityEngine;
using DG.Tweening;
using App.Common.SE;

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
        private bool isDragging = false;
        [SerializeField] private SEPlayer _sePlayer;

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

            if (Input.touchCount > 0) //タッチの判定
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began)
                {
                    StartDrag(touch.position);
                }
                else if (touch.phase == TouchPhase.Ended)
                {
                    DragEnd(touch.position);
                }
            }
            else //マウスの判定
            {
                if (Input.GetMouseButtonDown(0))
                {
                    StartDrag(Input.mousePosition);
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
        }

        void DragEnd(Vector2 endPos)
        {
            if (!isDragging) return;

            isDragging = false;

            float diffX = endPos.x - _dragStartPos.x;

            if (Mathf.Abs(diffX) < _swipeThreshold) return;

            if (diffX < 0)
            {
                Onclick("right");
                _sePlayer.PlaySE();
            }
            else
            {
                Onclick("left");
                _sePlayer.PlaySE();
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
