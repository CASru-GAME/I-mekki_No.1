using Unity.VisualScripting;
using UnityEngine;

namespace App.Scripts.Game.Enemy.Issue3
{
    public class EnemyJump : MonoBehaviour
    {
        [SerializeField] private float HorizontalSpeed = 1.0f;
        [SerializeField] private float JumpHeight = 1.0f;
        [SerializeField] private float JumpPitch = 1.0f;
        private int direction = 1;
        private float tX = 0f;
        private float tY = 0f;
        private float startPositionX;
        private float startPositionY;
        private float positionX ;
        private float positionY ;
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

        void UpdatePosition()
        {
            tX += Time.fixedDeltaTime;
            tY += Time.fixedDeltaTime;
            if (positionY < startPositionY)
            {
                positionX = startPositionX + direction * HorizontalSpeed * tX;
                positionY = startPositionY - JumpHeight * JumpPitch * (tY - Mathf.PI / JumpPitch) - JumpHeight * Mathf.Pow(JumpPitch, 2) / 2 * Mathf.Pow((tY - Mathf.PI / JumpPitch), 2);
                Vector2 movementEnemyJump = new Vector2(positionX, positionY);
                transform.position = movementEnemyJump;
            }
            else
            {
                positionX = startPositionX + direction * HorizontalSpeed * tX;
                positionY = startPositionY + JumpHeight* Mathf.Sin(JumpPitch * tY);
                Vector2 movementEnemyJump = new Vector2(positionX, positionY);
                transform.position = movementEnemyJump;
                positionY = movementEnemyJump.y;
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Wall"))
            {
                tX = 0f; 
                startPositionX = positionX;
                direction = direction * -1;
            }
            else if(tY < Mathf.PI / (2 * JumpPitch))
            {
                tY = Mathf.PI / JumpPitch - tY;
            }
            else
            {
                tY = 0f;
                startPositionY = positionY;
                
            }
        }
    }
}
