using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using DG.Tweening;

namespace App.Scripts.Game.Player.Move
{
    public class Jump
    {
        private GameObject player;
        private Tween jumpTween;

        public Jump(GameObject player)
        {
            this.player = player;
        }

        public void PerformJump(float jumpForce, float maxJumpForce, InputAction.CallbackContext context)
        {
            // プレイヤーが地面についているか確認
            if (IsGrounded())
            {
                if (context.phase == InputActionPhase.Started)
                {
                    if (context.interaction is PressInteraction || context.interaction is HoldInteraction)
                    {
                        Debug.Log("Jump Started");
                        player.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(player.GetComponent<Rigidbody2D>().linearVelocity.x, jumpForce);
                        jumpTween = player.transform.DOMoveY(player.transform.position.y + jumpForce, 0.5f).SetEase(Ease.OutQuad);
                    }
                }
                if (context.phase == InputActionPhase.Performed && context.interaction is HoldInteraction hold)
                {
                    Debug.Log("Jump Hold");
                    float holdTime = (float)context.duration;
                    float adjustedJumpForce = Mathf.Lerp(jumpForce, maxJumpForce, holdTime / hold.duration);
                    player.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(player.GetComponent<Rigidbody2D>().linearVelocity.x, adjustedJumpForce);
                    jumpTween = player.transform.DOMoveY(player.transform.position.y + adjustedJumpForce, 0.5f).SetEase(Ease.OutQuad);
                }
            }
        }

        public void CancelJump()
        {
            if (jumpTween != null && jumpTween.IsActive())
            {
                jumpTween.Kill();
            }
        }

        public bool IsGrounded()
        {
            // プレイヤーのコライダーのサイズを取得
            Collider2D collider = player.GetComponent<Collider2D>();
            if (collider == null)
            {
                Debug.LogError("Player does not have a Collider2D component.");
                return false;
            }

            // プレイヤーの足元にレイキャストを飛ばして地面に接触しているか確認
            Vector2 origin = new Vector2(player.transform.position.x, player.transform.position.y - collider.bounds.extents.y);
            RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.down, 0.1f);
            Debug.DrawRay(origin, Vector2.down * 0.1f, Color.red);

            if (hit.collider != null)
            {
                //Debug.Log("Grounded");
                return true;
            }
            return false;
        }
    }
}