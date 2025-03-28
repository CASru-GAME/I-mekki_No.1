using UnityEngine;

namespace App.Scripts.Game.Enemy
{
    public class EnemyCrushed : MonoBehaviour
    {
        [SerializeField] protected float horizontalSpeed;
        protected int direction = 1;
        protected Rigidbody2D rb;
        protected CapsuleCollider2D capsuleCollider2D; //Colliderの種類は必要に応じて変更する

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        }

        protected void OnCollisionEnter2D(Collision2D collision)
        {
            GameObject otherObject = collision.gameObject;
            if (collision.gameObject.CompareTag("Player"))
            {
                Vector2 otherPosition = otherObject.transform.position;
                Vector2 myPosition = transform.position;
                if (myPosition.y < otherPosition.y)
                {
                    capsuleCollider2D.enabled = false;
                    horizontalSpeed = 0f;
                }
            }
            else if (collision.gameObject.CompareTag("Wall"))
            {
                direction = direction * -1;
            }
        }
    }
}
