using UnityEngine;

namespace App.Scripts.Game.Enemy
{
    public class EnemyJump : _EnemyCrushed
    {
        [SerializeField] private float jumpHeight = 1.0f;
        [SerializeField] private float jumpPitch = 1.0f;
        private float t = 0f;
        private float startPosY;

        void Start()
        {
            startPosY = transform.position.y;
        }

        void FixedUpdate()
        {
            UpdatePosition();
        }

        private void UpdatePosition()
        {
            Vector2 currentPos = transform.position; // 現在の位置を取得
            t += Time.fixedDeltaTime;
            if (currentPos.y < startPosY)
            {
                Vector2 newPos = new Vector2(
                    horizontalSpeed * direction * Time.fixedDeltaTime + currentPos.x,
                    startPosY - jumpHeight * jumpPitch * (t - Mathf.PI / jumpPitch) - jumpHeight * Mathf.Pow(jumpPitch, 2) / 2 * Mathf.Pow((t - Mathf.PI / jumpPitch), 2)
                ); // 新しい位置を計算
                transform.position = newPos; // 位置を更新
            }
            else
            {
                Vector2 newPos = new Vector2(
                    horizontalSpeed * direction * Time.fixedDeltaTime + currentPos.x,
                    startPosY + jumpHeight * Mathf.Sin(jumpPitch * t)
                ); // 新しい位置を計算
                transform.position = newPos; // 位置を更新
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
                startPosY = transform.position.y;
            }
            base.OnCollisionEnter2D(collision);
        }
    }
}
