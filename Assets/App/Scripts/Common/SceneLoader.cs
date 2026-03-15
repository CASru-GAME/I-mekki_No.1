using UnityEngine;
using App.Common._Data;
using App.Scripts.Common.UI;

namespace App.Common
{
    public class SceneLoader : MonoBehaviour
    {
        private static SceneLoader instance;
        public SceneNames sceneNames;

        private int colorFlag = 0;
        private TitleBGM titleBGM;

        public void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
                return;
            }
            instance = this;
            DontDestroyOnLoad(gameObject);
            titleBGM = GetComponent<TitleBGM>();
        }
        public TitleBGM GetTitleBGM()
        {
            return titleBGM;
        }
        public void LoadScene(string sceneName)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
        }

        public void LoadDifficultySelectScene()
        {
            colorFlag = 0;

            _GameStatus._stage = _GameStatus.stage.BeforeStage;
            _GameStatus._gameStatus = _GameStatus.gameStatus.SelectDifficulty;
            _PlayerStatus.ClearAll();
            _PlayerStatistics.ClearStageData();
            SceneTransition.Instance.LoadSceneWithTransition(sceneNames.DifficultySelectScene, colorFlag);
        }

        public void LoadNextWithFlagInGameScene(int difficulty)
        {
            //タイトル用のBGMを止める
            titleBGM.Stop();
            _PlayerStatistics.AddClearedStageNum(1);

            //シーン切り替えの色の設定
            colorFlag = (_GameStatus.difficulty)difficulty == _GameStatus.difficulty.Easy ? 1
                      : (_GameStatus.difficulty)difficulty == _GameStatus.difficulty.Normal ? 2
                      : (_GameStatus.difficulty)difficulty == _GameStatus.difficulty.Hard ? 3
                      : 0;

            _PlayerStatus.ClearAll();
            _GameStatus._gameStatus = _GameStatus.gameStatus.InGame;
            if (_GameStatus._stage == _GameStatus.stage.BeforeStage)
            {
                _GameStatus._stage = _GameStatus.stage.Stage1;
                SceneTransition.Instance.LoadSceneWithTransition(sceneNames.GetNextGameSceneName((_GameStatus.difficulty)difficulty, _GameStatus._stage), colorFlag);
            }
            else if (_GameStatus._stage == _GameStatus.stage.Stage1)
            {
                _GameStatus._stage = _GameStatus.stage.Stage2;
                SceneTransition.Instance.LoadSceneWithTransition(sceneNames.GetNextGameSceneName((_GameStatus.difficulty)difficulty, _GameStatus._stage), colorFlag);
            }
            else if (_GameStatus._stage == _GameStatus.stage.Stage2)
            {
                _GameStatus._stage = _GameStatus.stage.Stage3;
                SceneTransition.Instance.LoadSceneWithTransition(sceneNames.GetNextGameSceneName((_GameStatus.difficulty)difficulty, _GameStatus._stage), colorFlag);
            }
            else
            {
                _GameStatus._stage = _GameStatus.stage.BeforeStage;
                _GameStatus._gameStatus = _GameStatus.gameStatus.Result;
                SceneTransition.Instance.LoadSceneWithTransition(sceneNames.ResultScene, colorFlag);
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
