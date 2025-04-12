using UnityEngine;
using DG.Tweening;

namespace App.Scripts.Game.Enemy
{
    public class EnemyFly : _EnemyCrushed
    {
        [SerializeField] private float flyHeight = 1.0f;
        [SerializeField] private float flyInterval = 1.0f;
        [SerializeField] private float flyPitch = 1.0f;
        private Vector2 currentPos;
        private Vector2 targetPos;
        private Sequence updatePos;
        private bool isLooping = true;

        private void Start()
        {
            currentPos = transform.position;
            targetPos = new Vector2(currentPos.x, currentPos.y + flyHeight);
            UpdatePositionY();
        }

        void FixedUpdate()
        {
            UpdatePositionX();
        }

        private void UpdatePositionY()
        {
            updatePos = DOTween.Sequence().SetLoops(-1,LoopType.Restart);
                updatePos.Append(transform.DOMoveY(targetPos.y,flyPitch));
                updatePos.Append(transform.DOMoveY(targetPos.y,flyInterval));
                updatePos.Append(transform.DOMoveY(currentPos.y,flyPitch));
                updatePos.Append(transform.DOMoveY(currentPos.y,flyInterval));
        }

        private void UpdatePositionX()
        {
            Vector2 currentPos = transform.position; // 現在の位置を取得
            Vector2 newPos = new Vector2(horizontalSpeed * direction * Time.fixedDeltaTime + currentPos.x, currentPos.y); // 新しい位置を計算
            transform.position = newPos; // 位置を更新
        }

        private new void OnCollisionEnter2D(Collision2D collision)
        {
            GameObject otherObject = collision.gameObject;
            if (collision.gameObject.CompareTag("Player"))
            {
                Vector2 otherPos = otherObject.transform.position;
                Vector2 myPos = transform.position;
                if (myPos.y < otherPos.y)
                {
                    updatePos.Kill();
                }
            }
            base.OnCollisionEnter2D(collision);
        }
    }
}
