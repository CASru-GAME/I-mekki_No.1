using UnityEngine;

namespace App.Scripts.Game.Enemy.Issue3
{
    public class EnemyGoStraight : MonoBehaviour
    {
        [SerializeField] private float HorizontalSpeed = 1.0f;
        private float t = 0f;

        void FixedUpdate()
        {
            t += Time.deltaTime;
            float x = HorizontalSpeed * t;
            transform.position = new Vector2(x, 0f);
        }
    }
}
