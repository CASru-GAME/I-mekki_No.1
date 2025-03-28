using UnityEngine;

namespace App.Scripts.Game.Enemy
{
    public class EnemyGoStraight : EnemyCrushed
    {
        void FixedUpdate()
        {
            UpdatePosition();
        }

        private void UpdatePosition()
        {
            Vector2 currentPosition = transform.position; // 現在の位置を取得
            Vector2 newPosition = new Vector2(horizontalSpeed * direction * Time.fixedDeltaTime + currentPosition.x, currentPosition.y); // 新しい位置を計算
            transform.position = newPosition; // 位置を更新
        }

        private new void OnCollisionEnter2D(Collision2D collision)
        {
            base.OnCollisionEnter2D(collision);
            //独自の衝突時処理があれば記入する
        }
    }
}
