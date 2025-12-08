using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using DG.Tweening;

namespace App.Game.Player.Move
{
    public class Jump
    {
        private GameObject player;
        private Tween jumpTween;
        private float jumpCooldown = 0.3f; // ジャンプ後のクールタイム（秒）
        private float lastJumpTime = -1f;
        public Jump(GameObject player)
        {
            this.player = player;
        }

        public void PerformJump(float jumpForce, float maxJumpForce, InputAction.CallbackContext context)
        {
            if (Time.time - lastJumpTime < jumpCooldown)
                return;
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
                        lastJumpTime = Time.time;
                    }
                }
                if (context.phase == InputActionPhase.Performed && context.interaction is HoldInteraction hold)
                {
                    Debug.Log("Jump Hold");
                    float holdTime = (float)context.duration;
                    float adjustedJumpForce = Mathf.Lerp(jumpForce, maxJumpForce, holdTime / hold.duration);
                    player.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(player.GetComponent<Rigidbody2D>().linearVelocity.x, adjustedJumpForce);
                    jumpTween = player.transform.DOMoveY(player.transform.position.y + adjustedJumpForce, 0.5f).SetEase(Ease.OutQuad);
                    lastJumpTime = Time.time;
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
            Vector2 origin = new Vector2(player.transform.position.x, player.transform.position.y - collider.bounds.extents.y - 0.1f);
            RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.down, 0.2f);
            Debug.DrawRay(origin, Vector2.down * 0.2f, Color.red);

            if (hit.collider != null && !hit.collider.gameObject.CompareTag("Player"))
            {
                Debug.Log("Grounded");
                return true;
            }
            return false;
        }
        public bool IsTouchingCeiling(){
            // プレイヤーのコライダーのサイズを取得
            Collider2D collider = player.GetComponent<Collider2D>();
            if (collider == null)
            {
                Debug.LogError("Player does not have a Collider2D component.");
                return false;
            }

            // プレイヤーの頭上にレイキャストを飛ばして天井に接触しているか確認
            Vector2 origin = new Vector2(player.transform.position.x, player.transform.position.y + collider.bounds.extents.y + 0.1f);
            RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.up, 0.2f);
            Debug.DrawRay(origin, Vector2.up * 0.2f, Color.red);

            if (hit.collider != null)
            {
                return true;
            }
            return false;
        }
        public void FixedUpdate()
        {
            // プレイヤーのY座標を固定する処理をここに追加
            if (IsTouchingCeiling())
            {
                CancelJump();
            }
        }
    }
}