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
            Debug.Log("Timed item effect started.", context.Player);
            yield return new WaitForSeconds(duration);
            Debug.Log("Timed item effect ended.", context.Player);
        }
    }
}
