using UnityEngine;

namespace App.Scripts.Game.Enemy
{
    public class EnemyGoStraight : EnemyCrushed
    {
        void FixedUpdate()
        //void Update()
        {
            UpdatePosition();
            if(Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("Space!");
                horizontalSpeed = 0f;
            }
        }

        private void UpdatePosition()
        {
            tX += Time.fixedDeltaTime;
            positionX = startPositionX + direction * horizontalSpeed * tX;
            Vector2 movementEnemyGoStraight = new Vector2(positionX, positionY);
            transform.position = movementEnemyGoStraight;
        }

        private new void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Wall"))
            {
                tX = 0f; 
                startPositionX = positionX;
                direction = direction * -1;
            }
            base.OnCollisionEnter2D(collision);
        }
    }
}
