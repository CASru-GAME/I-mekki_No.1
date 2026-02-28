using UnityEngine;

namespace App.Scripts.Title
{
    [CreateAssetMenu(
        fileName = "DictionaryDatabase",
        menuName = "Assets/App/ScriptableObject/Dictionary")]
    public class DictionaryDatabase : ScriptableObject
    {
        [SerializeField] private DictionaryItem[] _items;
        public DictionaryItem[] Items => _items;
    }
}
