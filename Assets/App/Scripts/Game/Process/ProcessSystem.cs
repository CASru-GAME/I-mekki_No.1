using UnityEngine;

namespace App.Game.Process
{
    public class ProcessSystem : MonoBehaviour
    {
        private _ChangeGameScene _changeGameScene = new _ChangeGameScene();
        private void Start()
        {
            //　初期化など
        }

        public void ChangeGameScene()
        {
            _changeGameScene.LoadNextScene();
        }

        public void ReloadGameScene()
        {
            _changeGameScene.ReloadThisScene();
        }
    }
}
