using UnityEngine;
using UnityEngine.UI;
namespace App.Common.MainCamera
{
    public class VolumeSliderManager : MonoBehaviour
    {
        [SerializeField] private AudioMixerVolumeController volumeController;
        [SerializeField] private Slider BGMVolumeSlider;
        [SerializeField] private Slider SEVolumeSlider;
        private void Start()
        {
            if (volumeController == null)
            {
                Debug.LogError("AudioMixerVolumeController が割り当てられていません。");
                return;
            }

            volumeController.LoadVolume();
            BGMVolumeSlider.value = volumeController.GetBGMVolume();
            SEVolumeSlider.value = volumeController.GetSEVolume();

            BGMVolumeSlider.onValueChanged.AddListener(volumeController.SetBGMVolume);
            SEVolumeSlider.onValueChanged.AddListener(volumeController.SetSEVolume);
        }

        private void OnDestroy()
        {
            if (volumeController != null)
            {
                volumeController.SaveVolume();
            }
        }
    }
}
