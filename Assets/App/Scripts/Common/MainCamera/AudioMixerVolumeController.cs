using UnityEngine;
using UnityEngine.Audio;
using App.Common.MainCamera.ValueObject;

namespace App.Common.MainCamera
{
    public class AudioMixerVolumeController : MonoBehaviour
    {
        [SerializeField] public AudioMixer audioMixer;

        private const string MasterParam = "Master";
        private const string BgmParam = "BGM";
        private const string SeParam = "SE";

        private const string MASTERVOLUMEKEY = "MasterVolume";
        private const string BGMVOLUMEKEY = "BGMVolume";
        private const string SEVOLUMEKEY = "SEVolume";

        private const float DefaultLinearVolume = 75f;
        private const float MinDb = -80f;
        private const float MaxDb = 20f;

        // Save時に使う「信頼できる現在値（0-100）」
        private float _masterLinear = DefaultLinearVolume;
        private float _bgmLinear = DefaultLinearVolume;
        private float _seLinear = DefaultLinearVolume;

        public void LoadVolume()
        {
            _masterLinear = PlayerPrefs.GetFloat(MASTERVOLUMEKEY, DefaultLinearVolume);
            _bgmLinear = PlayerPrefs.GetFloat(BGMVOLUMEKEY, DefaultLinearVolume);
            _seLinear = PlayerPrefs.GetFloat(SEVOLUMEKEY, DefaultLinearVolume);

            TrySetMixerFloat(MasterParam, ConvertLinearToDecibel(_masterLinear));
            TrySetMixerFloat(BgmParam, ConvertLinearToDecibel(_bgmLinear));
            TrySetMixerFloat(SeParam, ConvertLinearToDecibel(_seLinear));
        }

        public void SaveVolume()
        {
            // Mixerから再取得せず、保持中の線形値を保存
            PlayerPrefs.SetFloat(MASTERVOLUMEKEY, _masterLinear);
            PlayerPrefs.SetFloat(BGMVOLUMEKEY, _bgmLinear);
            PlayerPrefs.SetFloat(SEVOLUMEKEY, _seLinear);
            PlayerPrefs.Save();
        }

        private float ConvertLinearToDecibel(float linear)
        {
            linear = Mathf.Clamp(linear, 0f, 100f);
            return Mathf.Lerp(MinDb, MaxDb, linear / 100f);
        }

        private float ConvertDecibelToLinear(float decibel)
        {
            return Mathf.InverseLerp(MinDb, MaxDb, decibel) * 100f;
        }

        private bool TrySetMixerFloat(string parameterName, float value)
        {
            if (audioMixer == null) return false;
            return audioMixer.SetFloat(parameterName, value);
        }

        private bool TryGetMixerFloat(string parameterName, out float value)
        {
            value = 0f;
            if (audioMixer == null) return false;
            return audioMixer.GetFloat(parameterName, out value);
        }

        public void SetMasterVolume(float volume)
        {
            var v = new Volume(volume);
            _masterLinear = v.CurrentValue;
            TrySetMixerFloat(MasterParam, ConvertLinearToDecibel(_masterLinear));
        }

        public void SetBGMVolume(float volume)
        {
            var v = new Volume(volume);
            _bgmLinear = v.CurrentValue;
            TrySetMixerFloat(BgmParam, ConvertLinearToDecibel(_bgmLinear));
        }

        public void SetSEVolume(float volume)
        {
            var v = new Volume(volume);
            _seLinear = v.CurrentValue;
            TrySetMixerFloat(SeParam, ConvertLinearToDecibel(_seLinear));
        }

        public float GetMasterVolume()
        {
            if (TryGetMixerFloat(MasterParam, out float db))
            {
                _masterLinear = ConvertDecibelToLinear(db);
            }
            return _masterLinear;
        }

        public float GetBGMVolume()
        {
            if (TryGetMixerFloat(BgmParam, out float db))
            {
                _bgmLinear = ConvertDecibelToLinear(db);
            }
            return _bgmLinear;
        }

        public float GetSEVolume()
        {
            if (TryGetMixerFloat(SeParam, out float db))
            {
                _seLinear = ConvertDecibelToLinear(db);
            }
            return _seLinear;
        }
    }
}
