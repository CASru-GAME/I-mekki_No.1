using UnityEngine;

namespace App.Game.Item
{
    public abstract class ItemEffectBase : ScriptableObject
    {
        [SerializeField] private string itemId;

        public string ItemId => itemId;

        public abstract void Apply(ItemEffectContext context);
    }
}
