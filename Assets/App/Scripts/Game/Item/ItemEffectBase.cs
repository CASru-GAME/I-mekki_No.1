using UnityEngine;

namespace App.Game.Item
{
    public abstract class ItemEffectBase : ScriptableObject
    {
        [SerializeField] private string itemId;
        [SerializeField] private Sprite icon;
        [SerializeField] private float effectDuration;
        [SerializeField] private AudioClip soundEffect;
        [Range(0f, 1f)]
        [SerializeField] private float soundEffectVolume = 1f;

        public string ItemId => itemId;
        public Sprite Icon => icon;
        public float EffectDuration => effectDuration;
        public bool ShowsActiveIcon => icon != null && effectDuration > 0f;
        public AudioClip SoundEffect => soundEffect;
        public float SoundEffectVolume => soundEffectVolume;
        public bool HasSoundEffect => soundEffect != null && soundEffectVolume > 0f;

        public abstract void Apply(ItemEffectContext context);
    }
}
