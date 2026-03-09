using UnityEngine;
using App.Common._Data;

namespace App.Common
{
    public class StaticDataManager : MonoBehaviour
    {
        private static StaticDataManager instance;

        public void Awake()
        {
            // すでにインスタンスが存在する場合は、このオブジェクトを破棄
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
                return;
            }

            // インスタンスを設定し、シーン間で保持
            instance = this;
            DontDestroyOnLoad(gameObject);

            // 既存の初期化処理
            _PlayerStatistics.ClearStageData();
            _PlayerStatus.ClearAll();
            _JsonDataManager.LoadDictionaryData();
        }

        public void OnApplicationQuit()
        {
            _JsonDataManager.SaveDictionaryData();
        }

        public static StaticDataManager Instance
        {
            get { return instance; }
        }
    }
}
