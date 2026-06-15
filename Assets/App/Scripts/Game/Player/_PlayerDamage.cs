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
        private Coroutine invincibleCoroutine;
        private float effectTime = 6.0f;

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
            if (isInvincible || coroutineRunner == null)
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

            Physics2D.IgnoreLayerCollision(playerLayer, enemyLayer, false);
            isInvincible = false;
            damageCoroutine = null;
        }

        // 無敵状態の処理
        private IEnumerator InvincibleCoroutine()
        {
            isInvincible = true;

            Debug.Log("Active invinciblility");
            
            Physics2D.IgnoreLayerCollision(playerLayer, enemyLayer, true);

            yield return new WaitForSeconds(effectTime);

            Physics2D.IgnoreLayerCollision(playerLayer, enemyLayer, false);
            isInvincible = false;
            damageCoroutine = null;

            Debug.Log("End invinciblility");
        }

        public void StartInvincibility()
        {
            if (isInvincible || coroutineRunner == null)
            {
                return;
            }

            if (invincibleCoroutine != null)
            {
                coroutineRunner.StopCoroutine(invincibleCoroutine);
            }

            invincibleCoroutine = coroutineRunner.StartCoroutine(InvincibleCoroutine());
        }
    }
}
