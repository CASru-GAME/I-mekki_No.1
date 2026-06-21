using UnityEngine;

namespace App.Game.Item.Effects
{
    [CreateAssetMenu(
        fileName = "InvincibleItemEffect",
        menuName = "Assets/App/ScriptableObject/ItemEffects/Invincible")]
    public class InvincibleItemEffect : ItemEffectBase
    {
        public override void Apply(ItemEffectContext context)
        {
            var player = context.Player.GetComponent<App.Game.Player.Player>();
            player?.ActiveInvincibility();
        }
    }
}