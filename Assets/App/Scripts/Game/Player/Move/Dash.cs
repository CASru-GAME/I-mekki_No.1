using UnityEngine;
using System.Collections;
using UnityEditor.Rendering.LookDev;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
namespace Game.Player.Move
{
    public class Dash : MonoBehaviour
    {
        private Rigidbody2D rb;
        private bool isYAxisFixed = false;
        Jump jump = new Jump();

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        public void PerformDash(GameObject gameObject,InputAction.CallbackContext context)
        {
            if (jump.IsGrounded(gameObject))
            {
                return;
            }
            if(context.phase == InputActionPhase.Started)
            StartCoroutine(FixYAxisForOneSecond());
        }

        private IEnumerator FixYAxisForOneSecond()
        {
            isYAxisFixed = true;
            float originalY = rb.position.y;

            float elapsedTime = 0f;
            while (elapsedTime < 1f)
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