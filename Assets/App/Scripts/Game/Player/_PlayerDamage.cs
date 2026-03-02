using UnityEngine;
using DG.Tweening;
using App.Game.Enemy;
using App.Common._Data;
using System.Collections;
using System.Threading.Tasks;

namespace App.Game.Player
{
    public class _PlayerDamage
    {
        private int playerLayer;
        private int enemyLayer;
        private float invincibleTime;
        private float flashDuration;
        private SpriteRenderer spriteRenderer;

        public _PlayerDamage(int playerLayer, int enemyLayer, float invincibleTime, float flashDuration,SpriteRenderer spriteRenderer)
        {
            this.playerLayer = playerLayer;
            this.enemyLayer = enemyLayer;
            this.invincibleTime = invincibleTime;
            this.flashDuration = flashDuration;
            this.spriteRenderer = spriteRenderer;
        }
        public void TakeDamage()
        {
            Debug.Log("Player Damaged");
            _PlayerStatus.SubHp();

            //プレイヤーを無敵状態にする
            InvincibilityCoroutine();

            //プレイヤーを点滅させる
            FlashPlayer(this.spriteRenderer);
        }

        public async Task InvincibilityCoroutine()
        {
            // プレイヤーを無敵状態にする
            Physics2D.IgnoreLayerCollision(playerLayer, enemyLayer, true);
            // 無敵状態の持続時間を待つ
            await Task.Delay((int)(invincibleTime * 1000));
            // プレイヤーの無敵状態を解除する
            Physics2D.IgnoreLayerCollision(playerLayer, enemyLayer, false);
        }
        public async void FlashPlayer(SpriteRenderer spriteRenderer)
        {
            float flashDuration = this.flashDuration; // 点滅の間隔
            int flashCount = (int)((invincibleTime / flashDuration)/2); // 点滅の回数

            for (int i = 0; i < flashCount; i++)
            {
                // プレイヤーの透明度を切り替える
                spriteRenderer.color = new Color(1f, 1f, 1f, 0.5f); // 半透明
                await Task.Delay((int)(flashDuration * 1000)); // 点滅の間隔を待つ
                spriteRenderer.color = new Color(1f, 1f, 1f, 1f); // 元の色に戻す
                await Task.Delay((int)(flashDuration * 1000)); // 点滅の間隔を待つ
            }
        }
    }
}