using UnityEngine;
using App.Common.MainCamera;

namespace App.Debug.Issue6
{
    public class Debug : MonoBehaviour
    {
        [SerializeField] private float _masterVolume = 0f;
        [SerializeField] private float _bgmVolume = 0f;
        [SerializeField] private float _seVolume = 0f;
        [SerializeField] private AudioMixerVolumeController audioMixerVolumeController;
        private void Start()
        {
            audioMixerVolumeController.SetMasterVolume(_masterVolume);
            audioMixerVolumeController.SetBGMVolume(_bgmVolume);
            audioMixerVolumeController.SetSEVolume(_seVolume);
        }
    }
}
