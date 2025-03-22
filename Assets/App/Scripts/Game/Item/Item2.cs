using UnityEngine;

namespace App.Game.Item
{
    // 便宜上、アイテム2としていますが、アイテム名が決まったら変更してください
    public class Item2 : _CommonItem
    {
        [SerializeField] private int _invincibleTime = 5;
        public override void ItemEffect()
        {
            // アイテム2の効果
            /* 一定時間無敵
            　　staticにint型の変数を用意して、その値を==_invincibleTimeする
            　　その後、その値が1以上の場合は、ダメージを無効にしてください*/
            DeleteItem(this.gameObject);
        }
    }
}