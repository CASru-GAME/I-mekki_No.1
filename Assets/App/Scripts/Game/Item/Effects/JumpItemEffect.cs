using UnityEngine;
using UnityEngine.Serialization;

namespace App.Game.Item.Effects
{
    [CreateAssetMenu(
        fileName = "JumpItemEffect",
        menuName = "Assets/App/ScriptableObject/ItemEffects/Jump")]
    public class JumpItemEffect : ItemEffectBase
    {
        [SerializeField] private GameObject effectPrefab;
        [FormerlySerializedAs("maxJumpHeight")]
        [SerializeField] private float maxJumpHeightBonus = 1f;
        [FormerlySerializedAs("minJumpHeight")]
        [SerializeField] private float minJumpHeightBonus = 0.3f;

        public override void Apply(ItemEffectContext context)
        {
            context.Player.GetComponent<global::App.Game.Player.Player>()?.ActivateJumpEffect(
                EffectDuration,
                maxJumpHeightBonus,
                minJumpHeightBonus);
            context.Runner.ShowFootEffectForDuration(ItemId, effectPrefab, EffectDuration);
        }
    }
}
