using UnityEngine;
using DG.Tweening;

namespace App.Game.Item
{
    public class _CommonItem : MonoBehaviour
    {
        public virtual void ItemEffect()
        {
            // 共通のアイテム効果、アイテムごとにオーバーライドしてください
        }

        protected void DeleteItem(GameObject item)
        {
            // アニメーションして削除
            CanvasGroup canvasGroup = item.AddComponent<CanvasGroup>();
            canvasGroup.DOFade(0, 0.5f).OnComplete(() => Destroy(item));
            item.transform.DOScale(Vector3.zero, 0.5f);
        }

        protected void OnTriggerEnter2D(Collider2D collision)
        {
            // Playerのclassを持っているかどうかでplayerの判定
            /*if (collision.gameObject.GetComponent<App.Game.Player.Player>() != null)*/  ItemEffect();
            DeleteItem(this.gameObject);
        }
    }
}