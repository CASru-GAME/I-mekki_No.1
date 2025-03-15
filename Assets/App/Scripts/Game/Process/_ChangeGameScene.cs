using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

namespace App.Game.Process
{
    public class _ChangeGameScene
    {
        private enum difficulty
        {
            Easy,
            Normal,
            Hard
        }

        private enum stage
        {
            Stage1,
            Stage2,
            Stage3
        }

        private Dictionary<(difficulty, stage), string> nextScene = new Dictionary<(difficulty, stage), string>()
        {
            {(difficulty.Easy, stage.Stage1), "EasyStage2" },
            {(difficulty.Easy, stage.Stage2), "EasyStage3" },
            {(difficulty.Easy, stage.Stage3), "Result" },
            {(difficulty.Normal, stage.Stage1), "NormalStage2" },
            {(difficulty.Normal, stage.Stage2), "NormalStage3" },
            {(difficulty.Normal, stage.Stage3), "Result" },
            {(difficulty.Hard, stage.Stage1), "HardStage2" },
            {(difficulty.Hard, stage.Stage2), "HardStage3" },
            {(difficulty.Hard, stage.Stage3), "Result" },
        };

        private difficulty _difficulty;
        private stage _stage;

        public _ChangeGameScene()
        {
            //　staticの難易度とステージを取得
            //_difficulty = GameSystem.difficulty;
            //_stage = GameSystem.stage;
        }

        public void LoadNextScene()
        {
            string nextSceneName = nextScene[(_difficulty, _stage)];
            // SceneManager.LoadScene(nextSceneName);
        }

        public void ReloadThisScene()
        {
            // SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }


    }
}