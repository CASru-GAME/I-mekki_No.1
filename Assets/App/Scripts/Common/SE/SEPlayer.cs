using UnityEngine;

namespace App.Common.SE
{
    public class SEPlayer : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;
        
        public void PlaySE(AudioClip clip)
        {
            if (clip == null) return;
            audioSource.PlayOneShot(clip);
        }
    }
}
