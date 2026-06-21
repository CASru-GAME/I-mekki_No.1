using UnityEngine;

namespace App.Game.Item.Effects
{
    [CreateAssetMenu(
        fileName = "JumpItemEffect",
        menuName = "Assets/App/ScriptableObject/ItemEffects/Jump")]
    public class JumpItemEffect : ItemEffectBase
    {
        

        public override void Apply(ItemEffectContext context)
        {
            //Debug.Log(message, context.Player);
            context.Player.GetComponent<global::App.Game.Player.Player>()?.ActivateJumpEffect();
            Debug.Log("Jump effect applied.", context.Player);
        }
    }
}
