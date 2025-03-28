using UnityEngine;

namespace App.Scripts.Game.Enemy
{
    public class EnemyCrushed : MonoBehaviour
    {
        [SerializeField] protected float horizontalSpeed;
        protected int direction = 1;
        protected float tX = 0f;
        protected float tY = 0f;
        protected float startPositionX;
        protected float startPositionY;
        protected float positionX;
        protected float positionY;
        protected Rigidbody2D rb;
        protected CapsuleCollider2D capsuleCollider2D; //Colliderの種類は必要に応じて変更する
        
        void Start()
        {
            Vector2 posi = transform.position;
            startPositionX = posi.x;
            startPositionY = posi.y;
            positionX = startPositionX;
            positionY = startPositionY;
            rb = GetComponent<Rigidbody2D>();
            capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        }

        protected void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Wall"))
            {
                // 衝突したオブジェクトのY座標を取得
                float playerY = collision.transform.position.y;
                // 自身のY座標を取得
                float enemyY = transform.position.y;
                // 自身のY座標が低い場合、CapsuleCollider2Dをオフにする
                if (enemyY < playerY)
                {
                    capsuleCollider2D.enabled = false;
                    horizontalSpeed = 0f;
                }
            }
        }
    }
}
