using App.Common._Data;
using UnityEngine;

namespace App.Game.Item.Effects
{
    [CreateAssetMenu(
        fileName = "HealItemEffect",
        menuName = "Assets/App/ScriptableObject/ItemEffects/Heal")]
    public class HealItemEffect : ItemEffectBase
    {
        public override void Apply(ItemEffectContext context)
        {
            _PlayerStatus.AddHp();
            Debug.Log($"Player healed. HP: {_PlayerStatus.GetHp()}", context.Player);
        }
    }
}
