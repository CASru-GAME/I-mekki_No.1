using UnityEngine;

namespace App.Scripts.Game.Enemy.Issue3
{
    public class EnemyJump : MonoBehaviour
    {
        [SerializeField] private float HorizontalSpeed = 1.0f;
        [SerializeField] private float JumpHeight = 1.0f;
        [SerializeField] private float JumpPitch = 1.0f;
        private float t = 0f;
        void FixedUpdate()
        {
            t += Time.deltaTime;
            Vector2 movementEnemyJump = new Vector2(HorizontalSpeed * t, JumpHeight * Mathf.Abs(Mathf.Sin(JumpPitch * t)));
            transform.position = movementEnemyJump;
        }

    }
}
