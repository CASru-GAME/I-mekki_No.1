using UnityEngine;
using App.Common._Data;
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

        public _PlayerDamage(int playerLayer, int enemyLayer, float invincibleTime, float flashDuration, SpriteRenderer spriteRenderer, PlayerSE se, MonoBehaviour coroutineRunner)
        {
            this.playerLayer = playerLayer;
            this.enemyLayer = enemyLayer;
            this.invincibleTime = invincibleTime;
            this.flashDuration = flashDuration;
            this.spriteRenderer = spriteRenderer;
            this.se = se;
            this.coroutineRunner = coroutineRunner;
        }

        public void TakeDamage()
        {
            if (isInvincible || coroutineRunner == null)
            {
                return;
            }

            Debug.Log("Player Damaged");
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
    }
}