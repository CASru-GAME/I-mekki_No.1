using UnityEngine;
using UnityEngine.Audio;
using App.Common.MainCamera.ValueObject;

namespace App.Common.MainCamera
{
    public class AudioMixerVolumeController : MonoBehaviour
    {
        public AudioMixer audioMixer;
        private string _volumeParameterName1 = "Master";
        private string _volumeParameterName2 = "BGM";
        private string _volumeParameterName3 = "SE";
        private const string MASTERVOLUMEKEY = "MasterVolume";
        private const string BGMVOLUMEKEY = "BGMVolume";
        private const string SEVOLUMEKEY = "SEVolume";
        public void LoadVolume()
        {
            float masterVolume = PlayerPrefs.GetFloat(MASTERVOLUMEKEY, 75f);
            float bgmVolume = PlayerPrefs.GetFloat(BGMVOLUMEKEY, 75f);
            float seVolume = PlayerPrefs.GetFloat(SEVOLUMEKEY, 75f);

            audioMixer.SetFloat(_volumeParameterName1, ConvertLinearToDecibel(masterVolume));
            audioMixer.SetFloat(_volumeParameterName2, ConvertLinearToDecibel(bgmVolume));
            audioMixer.SetFloat(_volumeParameterName3, ConvertLinearToDecibel(seVolume));
        }
        public void SaveVolume()
        {
            float masterVolume;
            float bgmVolume;
            float seVolume;

            audioMixer.GetFloat(_volumeParameterName1, out masterVolume);
            audioMixer.GetFloat(_volumeParameterName2, out bgmVolume);
            audioMixer.GetFloat(_volumeParameterName3, out seVolume);

            PlayerPrefs.SetFloat(MASTERVOLUMEKEY, ConvertDecibelToLinear(masterVolume));
            PlayerPrefs.SetFloat(BGMVOLUMEKEY, ConvertDecibelToLinear(bgmVolume));
            PlayerPrefs.SetFloat(SEVOLUMEKEY, ConvertDecibelToLinear(seVolume));

            PlayerPrefs.Save();
        }
        private float ConvertLinearToDecibel(float Linear)
        {
            // volumeは0から100の範囲で渡されると仮定
            return Mathf.Lerp(-80f, 20f, Linear / 100f);
        }
        private float ConvertDecibelToLinear(float decibel)
        {
            // decibelは-80から20の範囲で渡されると仮定
            return Mathf.InverseLerp(-80f, 20f, decibel) * 100f;
        }
        public void SetMasterVolume(float volume)
        {
            Volume volumeObj = new Volume(volume);
            audioMixer.SetFloat(_volumeParameterName1, ConvertLinearToDecibel(volumeObj.CurrentValue));
        }
        public void SetBGMVolume(float volume)
        {
            Volume volumeObj = new Volume(volume);
            audioMixer.SetFloat(_volumeParameterName2, ConvertLinearToDecibel(volumeObj.CurrentValue));
        }
        public void SetSEVolume(float volume)
        {
            Volume volumeObj = new Volume(volume);
            audioMixer.SetFloat(_volumeParameterName3, ConvertLinearToDecibel(volumeObj.CurrentValue));
        }
        public float GetMasterVolume()
        {
            float volume;
            audioMixer.GetFloat(_volumeParameterName1, out volume);
            return ConvertDecibelToLinear(volume);
        }
        public float GetBGMVolume()
        {
            float volume;
            audioMixer.GetFloat(_volumeParameterName2, out volume);
            return ConvertDecibelToLinear(volume);
        }
        public float GetSEVolume()
        {
            float volume;
            audioMixer.GetFloat(_volumeParameterName3, out volume);
            return ConvertDecibelToLinear(volume);
        }
    }
}
