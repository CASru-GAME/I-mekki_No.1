using UnityEngine;
using DG.Tweening;
using System;

namespace App.Scripts.Game.Enemy
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
            GameObject otherObject = collision.gameObject;
            if (collision.gameObject.CompareTag("Player"))
            {
                Vector2 otherPos = otherObject.transform.position;
                Vector2 myPos = transform.position;
                if (myPos.y < otherPos.y)
                {
                    horizontalSpeed = 0f;
                    FadeOut();
                }
            }
            else if (collision.gameObject.CompareTag("Wall"))
            {
                direction = direction * -1;
            }
        }

        protected void FadeOut()
        {
            Sequence fadeOut = DOTween.Sequence();
            fadeOut.Append(transform.DOScale(0,FadeOutDur));
            fadeOut.OnComplete(() =>
            {
                Destroy(gameObject);
            });
        }
    }
}
