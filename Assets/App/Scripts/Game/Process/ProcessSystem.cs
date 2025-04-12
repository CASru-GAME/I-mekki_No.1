using System.Collections.Generic;
using UnityEngine;
using App.Common._Data;

namespace App.Game.Process
{
    public class ProcessSystem : MonoBehaviour
    {
        private _ChangeGameScene _changeGameScene = new _ChangeGameScene();
        [SerializeField] public static List<bool> isDictionary = new List<bool>();
        public _JsonDataManager jsonDataManager = new _JsonDataManager();
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
