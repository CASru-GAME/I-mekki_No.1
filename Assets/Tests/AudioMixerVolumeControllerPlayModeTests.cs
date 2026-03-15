#if UNITY_INCLUDE_TESTS
using NUnit.Framework;
using UnityEngine;
using App.Common.MainCamera;

namespace App.Tests.PlayMode
{
    public class AudioMixerVolumeControllerPlayModeTests
    {
        private const string PrefabPath = "Tests/AudioMixerVolumeControllerTestPrefab";
        private const string MasterVolumeKey = "MasterVolume";
        private const string BgmVolumeKey = "BGMVolume";
        private const string SeVolumeKey = "SEVolume";

        private GameObject instance;
        private AudioMixerVolumeController controller;

        [SetUp]
        public void SetUp()
        {
            PlayerPrefs.DeleteKey(MasterVolumeKey);
            PlayerPrefs.DeleteKey(BgmVolumeKey);
            PlayerPrefs.DeleteKey(SeVolumeKey);
            PlayerPrefs.Save();

            var prefab = Resources.Load<GameObject>(PrefabPath);
            Assert.IsNotNull(prefab, "テスト用 prefab が見つかりません。Assets/Resources/Tests/AudioMixerVolumeControllerTestPrefab.prefab を作成してください。");

            instance = Object.Instantiate(prefab);
            controller = instance.GetComponent<AudioMixerVolumeController>();

            Assert.IsNotNull(controller, "AudioMixerVolumeController が prefab に付いていません。");
            Assert.IsNotNull(controller.audioMixer, "audioMixer が prefab に割り当てられていません。");
        }

        [TearDown]
        public void TearDown()
        {
            if (instance != null)
            {
                Object.Destroy(instance);
            }

            PlayerPrefs.DeleteKey(MasterVolumeKey);
            PlayerPrefs.DeleteKey(BgmVolumeKey);
            PlayerPrefs.DeleteKey(SeVolumeKey);
            PlayerPrefs.Save();
        }

        [Test]
        public void SetVolume_ReflectsToAudioMixer()
        {
            controller.SetMasterVolume(23f);
            controller.SetBGMVolume(47f);
            controller.SetSEVolume(89f);

            bool okM = controller.audioMixer.GetFloat("Master", out float masterDb);
            bool okB = controller.audioMixer.GetFloat("BGM", out float bgmDb);
            bool okS = controller.audioMixer.GetFloat("SE", out float seDb);

            Assert.IsTrue(okM, "Master の取得に失敗しました。Expose 名を確認してください。");
            Assert.IsTrue(okB, "BGM の取得に失敗しました。Expose 名を確認してください。");
            Assert.IsTrue(okS, "SE の取得に失敗しました。Expose 名を確認してください。");

            Assert.That(masterDb, Is.EqualTo(Mathf.Lerp(-80f, 20f, 23f / 100f)).Within(0.2f));
            Assert.That(bgmDb, Is.EqualTo(Mathf.Lerp(-80f, 20f, 47f / 100f)).Within(0.2f));
            Assert.That(seDb, Is.EqualTo(Mathf.Lerp(-80f, 20f, 89f / 100f)).Within(0.2f));
        }

        [Test]
        public void LoadVolume_ReflectsSavedValuesToAudioMixer()
        {
            PlayerPrefs.SetFloat(MasterVolumeKey, 12f);
            PlayerPrefs.SetFloat(BgmVolumeKey, 34f);
            PlayerPrefs.SetFloat(SeVolumeKey, 56f);
            PlayerPrefs.Save();

            controller.LoadVolume();

            bool okM = controller.audioMixer.GetFloat("Master", out float masterDb);
            bool okB = controller.audioMixer.GetFloat("BGM", out float bgmDb);
            bool okS = controller.audioMixer.GetFloat("SE", out float seDb);

            Assert.IsTrue(okM, "Master の取得に失敗しました。Expose 名を確認してください。");
            Assert.IsTrue(okB, "BGM の取得に失敗しました。Expose 名を確認してください。");
            Assert.IsTrue(okS, "SE の取得に失敗しました。Expose 名を確認してください。");

            Assert.That(masterDb, Is.EqualTo(Mathf.Lerp(-80f, 20f, 12f / 100f)).Within(0.2f));
            Assert.That(bgmDb, Is.EqualTo(Mathf.Lerp(-80f, 20f, 34f / 100f)).Within(0.2f));
            Assert.That(seDb, Is.EqualTo(Mathf.Lerp(-80f, 20f, 56f / 100f)).Within(0.2f));
        }
    }
}
#endif
