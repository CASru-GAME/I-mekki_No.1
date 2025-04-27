using UnityEngine;
using DG.Tweening;
using App.Game.Enemy;
using App.Common._Data;

namespace App.Game.Player
{
    public class _PlayerDamage
    {
        private Collider2D PlayerCollider;
        private Collider2D PlayerStompCollider;
        private Rigidbody2D PlayerRigidbody;

        private float invincibleTime;
        private bool isInvincible = false;

        // コンストラクタに無敵時間を追加
        public _PlayerDamage(Collider2D playerCollider, Collider2D playerStompCollider, Rigidbody2D playerRigidbody, float invincibleTime)
        {
            PlayerCollider = playerCollider;
            PlayerStompCollider = playerStompCollider;
            PlayerRigidbody = playerRigidbody;
            this.invincibleTime = invincibleTime; // 無敵時間を初期化
        }

        public void OnCollisionEnter2D(Collision2D collision)
        {
            GameObject other = collision.gameObject;

            // 敵を踏みつけた場合
            if (PlayerStompCollider.IsTouching(other.GetComponent<Collider2D>()))
            {
                var enemy = other.GetComponent<_EnemyCrushed>();
                if (enemy != null)
                {
                    enemy.FadeOut(); // 敵に踏みつけたことを通知

                    // DOTweenでプレイヤーを跳ね返す
                    PlayerRigidbody.DOMoveY(PlayerRigidbody.position.y + 2f, 0.3f).SetEase(Ease.OutQuad);
                }
            }
            // 敵にぶつかった場合
            else if (PlayerCollider.IsTouching(other.GetComponent<Collider2D>()))
            {
                if (!isInvincible)
                {
                    TakeDamage();
                }
            }
        }

        private void TakeDamage()
        {
            // ダメージ処理
            Debug.Log("Player took damage!");

            // プレイヤーのHPを減少
            _PlayerStatus.SubHp();

            isInvincible = true;

            // 一時的に当たり判定を無効化
            PlayerCollider.enabled = false;
            PlayerStompCollider.enabled = false;

            // 無敵時間終了後に再度有効化
            PlayerRigidbody.gameObject.GetComponent<MonoBehaviour>().StartCoroutine(ResetInvincibility());
        }

        private System.Collections.IEnumerator ResetInvincibility()
        {
            yield return new WaitForSeconds(invincibleTime);
            isInvincible = false;
            PlayerCollider.enabled = true;
            PlayerStompCollider.enabled = true;
        }
    }
}