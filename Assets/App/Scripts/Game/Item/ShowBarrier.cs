using UnityEngine;
using DG.Tweening;

namespace App.Game.Item
{
    public class ShowBarrier : MonoBehaviour
    {
        [SerializeField] private float duration = 0.5f;
        private Vector3 defaultScale;
        private SpriteRenderer spriteRenderer;

        void Start()
        {
            defaultScale = transform.localScale;
            transform.localScale = Vector3.zero;

            spriteRenderer = GetComponent<SpriteRenderer>();

            Sequence seq = DOTween.Sequence();

            seq.Append(transform.DOScale(defaultScale*1.1f, duration * 0.7f)
                .SetEase(Ease.OutBack));

            seq.Append(transform.DOScale(defaultScale, duration * 0.3f)
                .SetEase(Ease.OutQuad));
        }

        public void DeleteBarrier()
        {
            spriteRenderer.DOFade(0f, duration*0.5f)
                .OnComplete(() => Destroy(transform.parent.gameObject));
        }
    }
}