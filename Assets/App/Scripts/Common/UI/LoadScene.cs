using UnityEngine;
using App.Common;

namespace App.Scripts.Common.UI
{
    public class LoadScene : MonoBehaviour
    {
        public void LoadNextScene(string sceneName)
        {
            if (!string.IsNullOrEmpty(sceneName))
            {
                SceneTransition.Instance.LoadSceneWithTransition(sceneName, 0);
            }
        }

        public void LoadDifficultySelectScene()
        {
            SceneLoader.Instance.LoadDifficultySelectScene();
        }

        public void LoadNextWithFlagInGameScene(int difficulty)
        {
            SceneLoader.Instance.LoadNextWithFlagInGameScene(difficulty);
        }

        public void LoadResultScene()
        {
            Time.timeScale = 1f; // ゲームの時間を通常に戻す
            SceneLoader.Instance.LoadResultScene();
        }
    }
}
