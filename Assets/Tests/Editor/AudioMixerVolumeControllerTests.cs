using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;
using App.Common.MainCamera;

namespace App.Tests.Editor
{
    public class AudioMixerVolumeControllerTests
    {
        private const string MixerPath = "Assets/App/Audio/AudioMixer.mixer";
        private const string MasterVolumeKey = "MasterVolume";
        private const string BgmVolumeKey = "BGMVolume";
        private const string SeVolumeKey = "SEVolume";

        private GameObject testObject;
        private AudioMixerVolumeController controller;

        [SetUp]
        public void SetUp()
        {
            PlayerPrefs.DeleteKey(MasterVolumeKey);
            PlayerPrefs.DeleteKey(BgmVolumeKey);
            PlayerPrefs.DeleteKey(SeVolumeKey);
            PlayerPrefs.Save();

            testObject = new GameObject("AudioMixerVolumeControllerTests");
            controller = testObject.AddComponent<AudioMixerVolumeController>();

            var mixer = AssetDatabase.LoadAssetAtPath<AudioMixer>(MixerPath);
            Assert.IsNotNull(mixer, $"AudioMixer が見つかりません: {MixerPath}");
            controller.audioMixer = mixer;
        }

        [TearDown]
        public void TearDown()
        {
            if (testObject != null)
            {
                Object.DestroyImmediate(testObject);
            }

            PlayerPrefs.DeleteKey(MasterVolumeKey);
            PlayerPrefs.DeleteKey(BgmVolumeKey);
            PlayerPrefs.DeleteKey(SeVolumeKey);
            PlayerPrefs.Save();
        }

        [Test]
        public void SaveVolume_SavesPlayerPrefsKeys()
        {
            controller.SetMasterVolume(23f);
            controller.SetBGMVolume(47f);
            controller.SetSEVolume(89f);

            controller.SaveVolume();

            Assert.IsTrue(PlayerPrefs.HasKey(MasterVolumeKey));
            Assert.IsTrue(PlayerPrefs.HasKey(BgmVolumeKey));
            Assert.IsTrue(PlayerPrefs.HasKey(SeVolumeKey));

            Assert.That(PlayerPrefs.GetFloat(MasterVolumeKey), Is.EqualTo(23f).Within(0.2f));
            Assert.That(PlayerPrefs.GetFloat(BgmVolumeKey), Is.EqualTo(47f).Within(0.2f));
            Assert.That(PlayerPrefs.GetFloat(SeVolumeKey), Is.EqualTo(89f).Within(0.2f));
        }

        [Test]
        public void LoadVolume_LoadsSavedValuesToCache_AndSaveKeepsThem()
        {
            PlayerPrefs.SetFloat(MasterVolumeKey, 12f);
            PlayerPrefs.SetFloat(BgmVolumeKey, 34f);
            PlayerPrefs.SetFloat(SeVolumeKey, 56f);
            PlayerPrefs.Save();

            controller.LoadVolume();
            controller.SaveVolume();

            Assert.That(PlayerPrefs.GetFloat(MasterVolumeKey), Is.EqualTo(12f).Within(0.2f));
            Assert.That(PlayerPrefs.GetFloat(BgmVolumeKey), Is.EqualTo(34f).Within(0.2f));
            Assert.That(PlayerPrefs.GetFloat(SeVolumeKey), Is.EqualTo(56f).Within(0.2f));
        }
    }
}