using System.Collections.Generic;
using UnityEngine;
using App.Common._Data;

namespace App.Game.Process
{
    public class ProcessSystem : MonoBehaviour
    {
        private _ChangeGameScene _changeGameScene = new _ChangeGameScene();

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
