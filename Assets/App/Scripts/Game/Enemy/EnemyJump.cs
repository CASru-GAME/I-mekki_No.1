using UnityEngine;

namespace App.Scripts.Game.Enemy
{
    public class EnemyJump : EnemyCrushed
    {
        [SerializeField] private float jumpHeight = 1.0f;
        [SerializeField] private float jumpPitch = 1.0f;

        void FixedUpdate()
        {
            UpdatePosition();
        }
        private void UpdatePosition()
        {
            tX += Time.fixedDeltaTime;
            tY += Time.fixedDeltaTime;
            if (positionY < startPositionY)
            {
                positionX = startPositionX + direction * horizontalSpeed * tX;
                positionY = startPositionY - jumpHeight * jumpPitch * (tY - Mathf.PI / jumpPitch) - jumpHeight * Mathf.Pow(jumpPitch, 2) / 2 * Mathf.Pow((tY - Mathf.PI / jumpPitch), 2);
                Vector2 movementEnemyJump = new Vector2(positionX, positionY);
                transform.position = movementEnemyJump;
            }
            else
            {
                positionX = startPositionX + direction * horizontalSpeed * tX;
                positionY = startPositionY + jumpHeight* Mathf.Sin(jumpPitch * tY);
                Vector2 movementEnemyJump = new Vector2(positionX, positionY);
                transform.position = movementEnemyJump;
                positionY = movementEnemyJump.y;
            }
        }

        private new void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Wall"))
            {
                tX = 0f; 
                startPositionX = positionX;
                direction = direction * -1;
            }
            else if(tY < Mathf.PI / (2 * jumpPitch))
            {
                tY = Mathf.PI / jumpPitch - tY;
            }
            else
            {
                tY = 0f;
                startPositionY = positionY;
                
            }
            base.OnCollisionEnter2D(collision);
        }
    }
}
