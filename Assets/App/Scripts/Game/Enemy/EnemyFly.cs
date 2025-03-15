using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.Scripts.Game.Enemy
{
    public class EnemyFly : MonoBehaviour
    {
        [SerializeField] private float flyHeight = 1.0f;
        [SerializeField] private float flyWidtht = 1.0f;
        [SerializeField] private float flyPitch = 1.0f;
        private float t = 0f;
        private float startPositionX;
        private float startPositionY;
        private float positionX;
        private float positionY;
        Rigidbody2D rb;
        void Start()
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
            t += Time.fixedDeltaTime;
            positionX = startPositionX + flyWidtht * Mathf.Sin(flyPitch * t);
            positionY = startPositionY + flyHeight * Mathf.Sin(flyPitch * t);
            Vector2 movementEnemyGoStraight = new Vector2(positionX, positionY);
            transform.position = movementEnemyGoStraight;
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            
        }
    }
}
