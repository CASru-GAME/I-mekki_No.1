using UnityEngine;

namespace App.Common.SE
{
    public class SEPlayer : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip seClip;

        public void PlaySE()
        {
            if (seClip == null) return;
            audioSource.PlayOneShot(seClip);
        }
    }
}
