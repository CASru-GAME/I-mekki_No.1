using UnityEngine;
using DG.Tweening;

namespace Assets.App.Scripts.Title
{
    public class SelectLevel : MonoBehaviour
    {
        [SerializeField] private GameObject _rightArrow;
        [SerializeField] private GameObject _leftArrow;
        [SerializeField] private float _moveDistance;

        public void Onclick(string _direction)
        {
            //右矢印が押されたら右に移動
            //左矢印が押されたら左に移動

            Vector3 moveDirection = Vector3.zero;

            if (_direction == "right")
            {
                moveDirection = new Vector3(-_moveDistance, 0, 0);
            }
            else if (_direction == "left")
            {
                moveDirection = new Vector3(_moveDistance, 0, 0);
            }

            // DoTweenで移動アニメーションを実行
            this.transform.DOMove(this.transform.position + moveDirection, 0.5f).OnComplete(() =>
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
            });
        }
    }
}
