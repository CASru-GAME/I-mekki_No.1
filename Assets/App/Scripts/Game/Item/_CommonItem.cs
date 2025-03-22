using UnityEngine;

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
            Destroy(item);
        }

        protected void OnTriggerEnter2D(Collider2D collision)
        {
            // Playerのclassを持っているかどうかでplayerの判定
            /*if (collision.gameObject.GetComponent<App.Game.Player.Player>() != null)*/  ItemEffect();
            DeleteItem(this.gameObject);
        }
    }
}