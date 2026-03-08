using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

namespace App.Scripts.Common
{
    public class LoadScene : MonoBehaviour
    {
        public void LoadNextScene(string sceneName)
        {
            if (!string.IsNullOrEmpty(sceneName))
            {
                SceneTransition.Instance.LoadSceneWithTransition(sceneName);
            }
            else
            {
                Debug.Log("シーン名が入力されていません");
            }
        }
    }
}