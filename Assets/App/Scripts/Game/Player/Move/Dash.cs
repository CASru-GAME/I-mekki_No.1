using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

namespace Game.Player.Move
{
    public class Dash : MonoBehaviour
    {
        private Rigidbody2D rb;
        private bool isYAxisFixed = false;
        private Jump jump = new Jump();
        [SerializeField] private float airTime = 1f; // 空中にいる時間を設定するプロパティ

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        public void PerformDash(GameObject gameObject, InputAction.CallbackContext context)
        {
            if (jump.IsGrounded(gameObject))
            {
                return;
            }
            if (context.phase == InputActionPhase.Started)
            {
                StartCoroutine(FixYAxisForDuration(airTime));
            }
        }

        private IEnumerator FixYAxisForDuration(float duration)
        {
            isYAxisFixed = true;
            float originalY = rb.position.y;

            float elapsedTime = 0f;
            while (elapsedTime < duration)
            {
                rb.position = new Vector2(rb.position.x, originalY);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            isYAxisFixed = false;
        }

        void FixedUpdate()
        {
            if (isYAxisFixed)
            {
                // y軸を固定するために速度をゼロに設定
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
            }
        }
    }
}