using System.Collections.Generic;
using UnityEngine;

namespace App.Game.Item
{
    [CreateAssetMenu(
        fileName = "ItemEffectDatabase",
        menuName = "Assets/App/ScriptableObject/ItemEffectDatabase")]
    public class ItemEffectDatabase : ScriptableObject
    {
        [SerializeField] private List<ItemEffectBase> effects = new List<ItemEffectBase>();

        public bool TryGetEffect(string itemId, out ItemEffectBase effect)
        {
            effect = null;

            if (string.IsNullOrEmpty(itemId))
            {
                return false;
            }

            foreach (ItemEffectBase candidate in effects)
            {
                if (candidate == null || candidate.ItemId != itemId)
                {
                    continue;
                }

                effect = candidate;
                return true;
            }

            return false;
        }
    }
}
