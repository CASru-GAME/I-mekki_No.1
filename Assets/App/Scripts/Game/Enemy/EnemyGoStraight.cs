using UnityEngine;

namespace App.Scripts.Game.Enemy
{
    public class EnemyGoStraight : MonoBehaviour
    {
        [SerializeField] private float horizontalSpeed = 1.0f;
        private int direction = 1;
        private float tX = 0f;
        private float startPositionX;
        private float startPositionY;
        private float positionX;
        private float positionY;
        Rigidbody2D rb;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            Vector2 posi = transform.position;
            startPositionX = posi.x;
            startPositionY = posi.y;
            positionX = startPositionX;
            positionY = startPositionY;

        }
        void FixedUpdate()
        {
            UpdatePosition();
        }

        private void UpdatePosition()
        {
            tX += Time.fixedDeltaTime;
            positionX = startPositionX + direction * horizontalSpeed * tX;
            Vector2 movementEnemyGoStraight = new Vector2(positionX, positionY);
            transform.position = movementEnemyGoStraight;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Wall"))
            {
                tX = 0f; 
                startPositionX = positionX;
                direction = direction * -1;
            }
        }
    }
}
