using UnityEngine;
using DG.Tweening;

namespace App.Scripts.Game.Enemy
{
    public class EnemyCrushed : MonoBehaviour
    {
        [SerializeField] protected float horizontalSpeed;
        [SerializeField] protected float FadeOutDur = 1.0f;
        [SerializeField] protected float FadeOutDist = -2.0f;
        protected int direction = 1;
        protected Rigidbody2D rb;
        protected Vector2 myPos;
        protected CapsuleCollider2D capsuleCollider2D; //Colliderの種類は必要に応じて変更する

        void Start()
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
            fadeOut.Append(transform.DOMoveY(myPos.y + FadeOutDist,FadeOutDur));
            fadeOut.Join(transform.DOScale(0,FadeOutDur));
        }
    }
}
