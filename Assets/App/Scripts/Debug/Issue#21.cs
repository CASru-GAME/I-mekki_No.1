using UnityEngine;
using App.Common._Data;
using App.Game.Process;

namespace App._Debug_.Issue21
{
    public class Issue21 : MonoBehaviour
    {
        [SerializeField] private _JsonDataManager jsonDataManager = new _JsonDataManager();
        private void Start()
        {
            // Save data
            jsonDataManager.SaveData();
            Debug.Log("Save data completed.");

            // Load data
            jsonDataManager.LoadData();
            Debug.Log("Load data completed.");

            // Display loaded data
            for (int i = 0; i < ProcessSystem.isDictionary.Count; i++)
            {
                Debug.Log($"isDictionary[{i}]: {ProcessSystem.isDictionary[i]}");
            }
        }
    }
}