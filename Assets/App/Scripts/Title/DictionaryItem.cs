using UnityEngine;

namespace App.Scripts.Title
{
    [System.Serializable]
    public class DictionaryItem
    {
        [SerializeField] private string title;
        [SerializeField, TextArea(3, 10)] private string description;

        public string Title => title;
        public string Description => description;
    }
}
