using App.Common._Data;
using UnityEngine;

namespace App.Game.Item.Effects
{
    [CreateAssetMenu(
        fileName = "HealItemEffect",
        menuName = "Assets/App/ScriptableObject/ItemEffects/Heal")]
    public class HealItemEffect : ItemEffectBase
    {
        [SerializeField] private GameObject effectPrefab;

        public override void Apply(ItemEffectContext context)
        {
            _PlayerStatus.AddHp();
            context.Runner.SpawnOneShotEffect(effectPrefab);
        }
    }
}
