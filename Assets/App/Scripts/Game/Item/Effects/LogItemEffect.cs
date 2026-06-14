using UnityEngine;

namespace App.Game.Item.Effects
{
    [CreateAssetMenu(
        fileName = "LogItemEffect",
        menuName = "Assets/App/ScriptableObject/ItemEffects/Log")]
    public class LogItemEffect : ItemEffectBase
    {
        [SerializeField] private string message = "Item effect triggered.";

        public override void Apply(ItemEffectContext context)
        {
            Debug.Log(message, context.Player);
        }
    }
}
