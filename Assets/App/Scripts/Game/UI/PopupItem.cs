using UnityEngine;

namespace App.Game.UI
{
    [System.Serializable]
    public class PopupItem
    {
        [SerializeField] private string popupText;

        [SerializeField] private Color textColor;

        public string PopupText => popupText;
        public Color TextColor => textColor;
    }
}

