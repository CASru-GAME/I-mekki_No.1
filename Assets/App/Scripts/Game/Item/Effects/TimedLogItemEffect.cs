using System.Collections;
using UnityEngine;

namespace App.Game.Item.Effects
{
    [CreateAssetMenu(
        fileName = "TimedLogItemEffect",
        menuName = "Assets/App/ScriptableObject/ItemEffects/TimedLog")]
    public class TimedLogItemEffect : ItemEffectBase
    {
        [SerializeField] private float duration = 5f;

        public override void Apply(ItemEffectContext context)
        {
            context.Runner.RunOrRestart(ItemId, EffectCoroutine(context));
        }

        private IEnumerator EffectCoroutine(ItemEffectContext context)
        {
            yield return new WaitForSeconds(duration);
        }
    }
}
