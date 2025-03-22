using UnityEngine;

namespace App.Game.Item
{
    // 便宜上、アイテム1としていますが、アイテム名が決まったら変更してください
    public class Item1 : _CommonItem
    {
        public override void ItemEffect()
        {
            // アイテム1の効果
            /* ダメージ一回無効
            　　staticにint型の変数を用意して、その値を++する
            　　その後、その値が1以上の場合は、ダメージを無効にしてください*/
            DeleteItem(this.gameObject);
        }
    }
}