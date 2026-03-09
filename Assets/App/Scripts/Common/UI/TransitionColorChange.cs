using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.Common.UI
{
    public class TransitionColorChange : MonoBehaviour
    {
        [SerializeField] private GameObject[] _bar = new GameObject[11];

        private readonly string[][] _colorPatternsHex =
        {
            // Light Blue
            new[] { "FFFFFF", "EDFCFE", "CCF8FE", "ACECF5", "8DE1ED" },
            // Green: easy
            new[] { "FFFFFF", "E0FCE3", "ACE9B3", "8AE394", "5DDC6C" },
            // Yellow: normal
            new[] { "FFFFFF", "FFFBE5", "FAF0B4", "FFEC7C", "FFE359" },
            // Blue: hard
            new[] { "FFFFFF", "E6F6FC", "B6E7FE", "91D8FC", "6ECBFC" }
        };

        public void ChangeColor(int patternIndex)
        {
            if (patternIndex < 0 || patternIndex >= _colorPatternsHex.Length)
            {
                Debug.LogError("Invalid pattern index");
                return;
            }

            var pattern = _colorPatternsHex[patternIndex];

            for (int i = 0; i < _bar.Length; i++)
            {
                var image = _bar[i].GetComponent<Image>();
                if (image == null) continue;

                int colorIndex = i / 2;
                if (i == 0 || i == 1 || i == 2) colorIndex = 0;
                else if (i == 3 || i == 4) colorIndex = 1;
                else if (i == 5 || i == 6) colorIndex = 2;
                else if (i == 7 || i == 8) colorIndex = 3;
                else colorIndex = 4;

                image.color = HexToColor(pattern[colorIndex]);
            }
        }

        private Color HexToColor(string hex)
        {
            ColorUtility.TryParseHtmlString("#" + hex, out var color);
            return color;
        }

    }
}