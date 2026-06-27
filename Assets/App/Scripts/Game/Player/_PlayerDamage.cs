using UnityEngine;
using App.Common._Data;
using App.Game.Item;
using System.Collections;

namespace App.Game.Player
{
    public class _PlayerDamage
    {
        private int playerLayer;
        private int enemyLayer;
        private float invincibleTime;
        private float flashDuration;
        private SpriteRenderer spriteRenderer;
        private PlayerSE se;
        private MonoBehaviour coroutineRunner;
        private bool isInvincible;
        private Coroutine damageCoroutine;
        private bool isItemInvincible;
        private Coroutine invincibleCoroutine;

        public _PlayerDamage(int playerLayer, int enemyLayer, float invincibleTime, float flashDuration, SpriteRenderer spriteRenderer, PlayerSE se, MonoBehaviour coroutineRunner)
        {
            this.playerLayer = playerLayer;
            this.enemyLayer = enemyLayer;
            this.invincibleTime = invincibleTime;
            this.flashDuration = flashDuration;
            this.spriteRenderer = spriteRenderer;
            this.se = se;
            this.coroutineRunner = coroutineRunner;

            Physics2D.IgnoreLayerCollision(playerLayer, enemyLayer, false);
        }

        public void TakeDamage()
        {
            if (isInvincible || isItemInvincible || coroutineRunner == null)
            {
                return;
            }

            _PlayerStatus.SubHp();

            if (damageCoroutine != null)
            {
                coroutineRunner.StopCoroutine(damageCoroutine);
            }
            damageCoroutine = coroutineRunner.StartCoroutine(DamageCoroutine());

            se.PlayDamage();
        }

        private IEnumerator DamageCoroutine()
        {
            isInvincible = true;
            Physics2D.IgnoreLayerCollision(playerLayer, enemyLayer, true);

            float elapsed = 0f;
            float blinkInterval = Mathf.Max(0.01f, flashDuration);
            bool halfTransparent = true;

            while (elapsed < invincibleTime)
            {
                if (spriteRenderer != null)
                {
                    spriteRenderer.color = halfTransparent
                        ? new Color(1f, 1f, 1f, 0.5f)
                        : new Color(1f, 1f, 1f, 1f);
                }

                float wait = Mathf.Min(blinkInterval, invincibleTime - elapsed);
                yield return new WaitForSeconds(wait);
                elapsed += wait;
                halfTransparent = !halfTransparent;
            }

            if (spriteRenderer != null)
            {
                spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
            }

            isInvincible = false;
            damageCoroutine = null;

            if (!isItemInvincible)
            {
                Physics2D.IgnoreLayerCollision(playerLayer, enemyLayer, false);
            }
        }

        // アイテムによる無敵状態の処理
        // ダメージ無敵とアイテム無敵が競合した場合は、アイテム無敵の発動を優先
        private IEnumerator InvincibleCoroutine(float duration)
        {
            isItemInvincible = true;

            Debug.Log("Active invinciblility");
            
            Physics2D.IgnoreLayerCollision(playerLayer, enemyLayer, true);

            //エフェクトの表示

            yield return new WaitForSeconds(duration);

            //エフェクトの非表示

            Physics2D.IgnoreLayerCollision(playerLayer, enemyLayer, false);
            isItemInvincible = false;
            invincibleCoroutine = null;

            Debug.Log("End invinciblility");
        }

        public void StartInvincibility(float duration)
        {
            if (coroutineRunner == null || duration <= 0f)
            {
                return;
            }

            if (damageCoroutine != null)
            {
                coroutineRunner.StopCoroutine(damageCoroutine);
                damageCoroutine = null;
                isInvincible = false;

                if (spriteRenderer != null)
                {
                    spriteRenderer.color = Color.white;
                }
            }

            if (invincibleCoroutine != null)
            {
                coroutineRunner.StopCoroutine(invincibleCoroutine);
            }

            invincibleCoroutine = coroutineRunner.StartCoroutine(InvincibleCoroutine(duration));
        }
    }
}
