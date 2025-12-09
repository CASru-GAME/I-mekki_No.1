using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using DG.Tweening;

namespace App.Game.Player.Move
{
    public class Dash
    {
        private Rigidbody2D rb;
        private bool isYAxisFixed = false;
        private Jump jump;
        private float airTime;
        private bool canDash = true; // ダッシュが使用可能かどうかを管理するフラグ
        private Tween dashTween; // ダッシュアニメーション用のTweenを保持する変数
        private float PlayerSpeed;

        public Dash(Rigidbody2D rigidbody2D, Jump jumpInstance, float airTimeDuration, float playerSpeed)
        {
            rb = rigidbody2D;
            jump = jumpInstance;
            airTime = airTimeDuration;
            PlayerSpeed = playerSpeed;
        }

        public async UniTask PerformDash(InputAction.CallbackContext context)
        {
            if (!canDash || jump.IsGrounded())
            {
                return;
            }
            if (context.phase == InputActionPhase.Started)
            {
                canDash = false; // ダッシュを使用したのでフラグを無効にする
                jump.CancelJump(); // ジャンプをキャンセル

                // DOTweenを使用してダッシュアニメーションを開始
                /*dashTween = rb.transform.DOMoveX(rb.transform.position.x + 1, 0.25f)
                    .SetLoops(2, LoopType.Yoyo)
                    .SetEase(Ease.OutCirc)
                    .OnKill(() => {
                        rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
                        rb.transform.position = new Vector3(rb.transform.position.x + PlayerSpeed * 0.5f, rb.transform.position.y, 0);
                }); // キャンセル時に速度をリセット*/
                var seq = DOTween.Sequence();
                dashTween = rb.transform.DOMoveX(rb.transform.position.x + PlayerSpeed * 0.75f, 0.5f)
                    .SetLoops(1, LoopType.Yoyo)
                    .SetEase(Ease.OutCirc)
                    .OnKill(() => {
                        rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
                    });
                seq.Append(dashTween);
                dashTween = rb.transform.DOMoveX(rb.transform.position.x + PlayerSpeed * 0.75f, 0.25f)
                    .SetLoops(1, LoopType.Yoyo)
                    .SetEase(Ease.OutCirc)
                    .OnKill(() => {
                        rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
                    });
                seq.Append(dashTween);
                await FixYAxisForDuration(airTime);
            }
        }
        public bool IsTouchingWall(){
            // プレイヤーのコライダーのサイズを取得
            Collider2D collider = rb.GetComponent<Collider2D>();
            if (collider == null)
            {
                Debug.LogError("Player does not have a Collider2D component.");
                return false;
            }

            // プレイヤーの位置を取得
            Vector2 playerPosition = rb.position;

            // プレイヤーのコライダーの境界を取得
            Bounds bounds = collider.bounds;
            Vector2 origin = new Vector2(playerPosition.x + bounds.extents.x + 0.1f, playerPosition.y);
            // コライダーの境界を使用して、壁に接触しているかどうかを確認
            RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.right, bounds.extents.x + 0.1f);
            return hit.collider != null;
        }
        public void CancelDash()
        {
            // ダッシュをキャンセルするための処理
            if (dashTween != null && dashTween.IsActive())
            {
                dashTween.Kill(); // アニメーションを停止
            }

            // x軸の速度をゼロに設定し、y軸に小さな下向きの力を加える
            rb.linearVelocity = new Vector2(0, Mathf.Min(rb.linearVelocity.y, -1f)); // 落下を促すためにy軸の速度を調整
            isYAxisFixed = false; // y軸の固定を解除
        }
        private async UniTask FixYAxisForDuration(float duration)
        {
            isYAxisFixed = true;
            float originalY = rb.position.y;

            float elapsedTime = 0f;
            while (elapsedTime < duration)
            {
                rb.position = new Vector2(rb.position.x, originalY);
                elapsedTime += Time.deltaTime;
                await UniTask.Yield(PlayerLoopTiming.FixedUpdate);
            }

            isYAxisFixed = false;
        }

        public void FixedUpdate()
        {
            if (isYAxisFixed)
            {
                // y軸を固定するために速度をゼロに設定
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
                // 壁に接触したらダッシュをキャンセル
            if (IsTouchingWall())
            {
                Debug.Log("Wall detected, canceling dash.");
                CancelDash();
            }
            }

            // 地面に着いたらダッシュを再度使用可能にする
            if (jump.IsGrounded())
            {
                canDash = true;
            }
        }
    }
}