using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using DG.Tweening;

namespace App.Scripts.Game.Player.Move
{
    public class Dash
    {
        private Rigidbody2D rb;
        private bool isYAxisFixed = false;
        private Jump jump;
        private float airTime;
        private bool canDash = true; // ダッシュが使用可能かどうかを管理するフラグ

        public Dash(Rigidbody2D rigidbody2D, Jump jumpInstance, float airTimeDuration)
        {
            rb = rigidbody2D;
            jump = jumpInstance;
            airTime = airTimeDuration;
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

                // DOTween.Toを使用して速度をアニメーション化
                /*float dashDistance = 101f; // ダッシュの距離
                float dashDuration = 20f; // ダッシュの持続時間
                Vector2 startVelocity = rb.linearVelocity;
                Vector2 endVelocity = new Vector2(dashDistance / dashDuration, rb.linearVelocity.y);

                DOTween.To(() => rb.linearVelocity, x => rb.linearVelocity = x, endVelocity, dashDuration)
                    .SetEase(Ease.OutCirc)
                    .OnComplete(() => rb.linearVelocity = new Vector2(0, rb.linearVelocity.y)); // ダッシュ終了後に速度をゼロに設定
*/             
                rb.transform.DOMoveX(rb.transform.position.x + 1f, 0.25f)
                //.SetLoops(2, LoopType.Yoyo)
                .SetEase(Ease.OutCirc);
                /*rb.transform.DOMoveX(rb.transform.position.x + 1f, 0.5f)
                .SetLoops(1, LoopType.Yoyo)
                    .SetEase(Ease.OutCirc);*/
                await FixYAxisForDuration(airTime);
            }
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
            }

            // 地面に着いたらダッシュを再度使用可能にする
            if (jump.IsGrounded())
            {
                canDash = true;
            }
        }
    }
}