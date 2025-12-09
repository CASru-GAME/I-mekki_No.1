using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using DG.Tweening;
using UnityEditor.Callbacks;

namespace App.Game.Player.Move
{
    public class Jump
    {
        private GameObject player;
        private Tween jumpTween;
        private float jumpCooldown = 0.3f; // ジャンプ後のクールタイム（秒）
        private float lastJumpTime = -1f;
        private bool isJumping = false;
        private float jumpForce = 30f;
        private float FirstjumpForce = 5f;
        private int maxJumpCount = 25;
        private int count = 0;
        public Jump(GameObject player, float jumpForceValue, float firstJumpForceValue, int maxJumpCountValue)
        {
            this.player = player;
            this.jumpForce = jumpForceValue;
            this.FirstjumpForce = firstJumpForceValue;
            this.maxJumpCount = maxJumpCountValue;
        }

        public void PerformJump(InputAction.CallbackContext context)
        {
            if (Time.time - lastJumpTime < jumpCooldown){
                isJumping = false;
                return;
            }
            
            if (IsGrounded())
            {
                if (context.phase == InputActionPhase.Started){
                    Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
                    rb.AddForce(new Vector2(0, FirstjumpForce), ForceMode2D.Impulse);
                    count = 0;
                    isJumping = true;
                    lastJumpTime = Time.time;
                }
            }
            // ジャンプボタンを離したら上昇終了
                if (context.phase == InputActionPhase.Canceled)
                {
                    isJumping = false;
                    count = 0;
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
            if (count >= maxJumpCount)
            {
                isJumping = false;
            }
            // ジャンプ中は毎フレーム上向きの力を加える
            if (isJumping)
            {
                count ++;
                Debug.Log("Applying Jump Force");
                Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
                rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Force);
            }
            // プレイヤーのY座標を固定する処理をここに追加
            if (IsTouchingCeiling())
            {
                CancelJump();
                isJumping = false;
                count = 0;
            }
        }
    }
}