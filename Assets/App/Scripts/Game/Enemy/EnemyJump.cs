using UnityEngine;

namespace App.Scripts.Game.Enemy
{
    public class EnemyJump : EnemyCrushed
    {
        [SerializeField] private float jumpHeight = 1.0f;
        [SerializeField] private float jumpPitch = 1.0f;
        private float t = 0f;
        private float startPositionY;

        void Start()
        {
            startPositionY = transform.position.y;
        }

        void FixedUpdate()
        {
            UpdatePosition();
        }

        private void UpdatePosition()
        {
            Vector2 currentPosition = transform.position; // 現在の位置を取得
            t += Time.fixedDeltaTime;
            if (currentPosition.y < startPositionY)
            {
                Vector2 newPosition = new Vector2(
                    horizontalSpeed * direction * Time.fixedDeltaTime + currentPosition.x,
                    startPositionY - jumpHeight * jumpPitch * (t - Mathf.PI / jumpPitch) - jumpHeight * Mathf.Pow(jumpPitch, 2) / 2 * Mathf.Pow((t - Mathf.PI / jumpPitch), 2)
                ); // 新しい位置を計算
                transform.position = newPosition; // 位置を更新
            }
            else
            {
                Vector2 newPosition = new Vector2(
                    horizontalSpeed * direction * Time.fixedDeltaTime + currentPosition.x,
                    startPositionY + jumpHeight * Mathf.Sin(jumpPitch * t)
                ); // 新しい位置を計算
                transform.position = newPosition; // 位置を更新
            }
        }

        private new void OnCollisionEnter2D(Collision2D collision)
        {
            if (t < Mathf.PI / (2 * jumpPitch))
            {
                t = Mathf.PI / jumpPitch - t;
            }
            else
            {
                t = 0f;
                startPositionY = transform.position.y;
            }
            base.OnCollisionEnter2D(collision);
        }
    }
}
