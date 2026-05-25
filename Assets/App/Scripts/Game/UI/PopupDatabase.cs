using UnityEngine;

namespace App.Game.UI
{
    [CreateAssetMenu(
        fileName = "PopupDatabase",
        menuName = "Assets/App/ScriptableObject/Popup")]
    public class PopupDatabase : ScriptableObject
    {
        [SerializeField] private PopupItem[] _items;
        public PopupItem[] Items => _items;
    }
}