using UnityEngine;
using DG.Tweening;
using System;

namespace App.Game.Enemy
{
    public class _EnemyCrushed : MonoBehaviour
    {
        [SerializeField] protected float horizontalSpeed;
        [SerializeField] protected float FadeOutDur = 0.2f;
        protected int direction = 1;
        protected Rigidbody2D rb;
        protected Vector2 myPos;
        protected CapsuleCollider2D capsuleCollider2D; //Colliderの種類は必要に応じて変更する

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        }

        protected void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Wall"))
            {
                direction = direction * -1;
            }
        }

        public void FadeOut()
        {
            horizontalSpeed = 0f;
            Sequence fadeOut = DOTween.Sequence();
            fadeOut.Append(transform.DOScale(0,FadeOutDur));
            fadeOut.OnComplete(() =>
            {
                Destroy(gameObject);
            });
        }
    }
}
