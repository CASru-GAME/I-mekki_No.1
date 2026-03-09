using UnityEngine;
using App.Common._Data;

namespace App.Common
{
    public class SceneLoader : MonoBehaviour
    {
        private static SceneLoader instance;
        public SceneNames sceneNames;
        public void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
                return;
            }
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public void LoadDifficultySelectScene()
        {
            _GameStatus._stage = _GameStatus.stage.BeforeStage;
            _GameStatus._gameStatus = _GameStatus.gameStatus.SelectDifficulty;
            _PlayerStatus.ClearAll();
            _PlayerStatistics.ClearStageData();
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneNames.DifficultySelectScene);
        }

        public void LoadNextWithFlagInGameScene(int difficulty)
        {
            _PlayerStatus.ClearAll();
            _GameStatus._gameStatus = _GameStatus.gameStatus.InGame;
            if (_GameStatus._stage == _GameStatus.stage.BeforeStage)
            {
                _GameStatus._stage = _GameStatus.stage.Stage1;
                UnityEngine.SceneManagement.SceneManager.LoadScene(sceneNames.GetNextGameSceneName((_GameStatus.difficulty)difficulty, _GameStatus._stage));
            }
            else if (_GameStatus._stage == _GameStatus.stage.Stage1)
            {
                _GameStatus._stage = _GameStatus.stage.Stage2;
                UnityEngine.SceneManagement.SceneManager.LoadScene(sceneNames.GetNextGameSceneName((_GameStatus.difficulty)difficulty, _GameStatus._stage));
            }
            else if (_GameStatus._stage == _GameStatus.stage.Stage2)
            {
                _GameStatus._stage = _GameStatus.stage.Stage3;
                UnityEngine.SceneManagement.SceneManager.LoadScene(sceneNames.GetNextGameSceneName((_GameStatus.difficulty)difficulty, _GameStatus._stage));
            }
            else
            {
                _GameStatus._stage = _GameStatus.stage.BeforeStage;
                _GameStatus._gameStatus = _GameStatus.gameStatus.Result;
                UnityEngine.SceneManagement.SceneManager.LoadScene(sceneNames.ResultScene);
            }
        }

        public static SceneLoader Instance
        {
            get { return instance; }
        }
    }

    [System.Serializable]
    public class SceneNames
    {
        public string TitleScene = "TitleScene";
        public string DifficultySelectScene = "DifficultySelectScene";
        public string InGameSceneEasyOne = "InGameSceneEasy1";
        public string InGameSceneEasyTwo = "InGameSceneEasy2";
        public string InGameSceneEasyThree = "InGameSceneEasy3";
        public string InGameSceneNormalOne = "InGameSceneNormal1";
        public string InGameSceneNormalTwo = "InGameSceneNormal2";
        public string InGameSceneNormalThree = "InGameSceneNormal3";
        public string InGameSceneHardOne = "InGameSceneHard1";
        public string InGameSceneHardTwo = "InGameSceneHard2";
        public string InGameSceneHardThree = "InGameSceneHard3";
        public string ResultScene = "ResultScene";

        public string GetNextGameSceneName(_GameStatus.difficulty difficulty, _GameStatus.stage stage)
        {
            if (difficulty == _GameStatus.difficulty.Easy)
            {
                if (stage == _GameStatus.stage.Stage1)
                {
                    return InGameSceneEasyOne;
                }
                else if (stage == _GameStatus.stage.Stage2)
                {
                    return InGameSceneEasyTwo;
                }
                else if (stage == _GameStatus.stage.Stage3)
                {
                    return InGameSceneEasyThree;
                }
            }
            else if (difficulty == _GameStatus.difficulty.Normal)
            {
                if (stage == _GameStatus.stage.Stage1)
                {
                    return InGameSceneNormalOne;
                }
                else if (stage == _GameStatus.stage.Stage2)
                {
                    return InGameSceneNormalTwo;
                }
                else if (stage == _GameStatus.stage.Stage3)
                {
                    return InGameSceneNormalThree;
                }
            }
            else if (difficulty == _GameStatus.difficulty.Hard)
            {
                if (stage == _GameStatus.stage.Stage1)
                {
                    return InGameSceneHardOne;
                }
                else if (stage == _GameStatus.stage.Stage2)
                {
                    return InGameSceneHardTwo;
                }
                else if (stage == _GameStatus.stage.Stage3)
                {
                    return InGameSceneHardThree;
                }
            }
            return null; // デフォルトのシーン名を返すか、エラー処理を行う
        }
    }
}
