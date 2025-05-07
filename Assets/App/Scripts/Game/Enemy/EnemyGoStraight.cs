using UnityEngine;

namespace App.Game.Enemy
{
    public class EnemyGoStraight : _EnemyCrushed
    {
        void FixedUpdate()
        {
            UpdatePosition();
        }

        private void UpdatePosition()
        {
            Vector2 currentPos = transform.position; // 現在の位置を取得
            Vector2 newPos = new Vector2(horizontalSpeed * direction * Time.fixedDeltaTime + currentPos.x, currentPos.y); // 新しい位置を計算
            transform.position = newPos; // 位置を更新
        }

        private new void OnCollisionEnter2D(Collision2D collision)
        {
            base.OnCollisionEnter2D(collision);
        }
    }
}
