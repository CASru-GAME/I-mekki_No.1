using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.Game.Item
{
    public class ItemEffectRunner : MonoBehaviour
    {
        private readonly Dictionary<string, Coroutine> runningEffects = new Dictionary<string, Coroutine>();

        public Coroutine Run(IEnumerator routine)
        {
            return StartCoroutine(routine);
        }

        public Coroutine RunOrRestart(string key, IEnumerator routine)
        {
            Stop(key);

            Coroutine coroutine = StartCoroutine(RunAndClear(key, routine));
            runningEffects[key] = coroutine;
            return coroutine;
        }

        public void Stop(string key)
        {
            if (!runningEffects.TryGetValue(key, out Coroutine coroutine))
            {
                return;
            }

            if (coroutine != null)
            {
                StopCoroutine(coroutine);
            }

            runningEffects.Remove(key);
        }

        public void ShowActiveIcon(string itemId, Sprite icon, float duration)
        {
            if (string.IsNullOrEmpty(itemId) || icon == null || duration <= 0f)
            {
                return;
            }

            ActiveItemIconDisplay display = GetComponent<ActiveItemIconDisplay>();
            if (display == null)
            {
                display = gameObject.AddComponent<ActiveItemIconDisplay>();
            }

            display.Show(itemId, icon, duration);
            RunOrRestart($"icon:{itemId}", HideActiveIconAfter(display, itemId, duration));
        }

        public void ConfigureActiveIconDisplay(
            Vector2 offset,
            float iconSize,
            float spacing,
            int sortingOrderOffset,
            Vector2 timerOffset,
            int timerFontSize,
            float timerCharacterSize,
            Color timerColor,
            Font timerFont)
        {
            ActiveItemIconDisplay display = GetComponent<ActiveItemIconDisplay>();
            if (display == null)
            {
                display = gameObject.AddComponent<ActiveItemIconDisplay>();
            }

            display.Configure(
                offset,
                iconSize,
                spacing,
                sortingOrderOffset,
                timerOffset,
                timerFontSize,
                timerCharacterSize,
                timerColor,
                timerFont);
        }

        private IEnumerator HideActiveIconAfter(ActiveItemIconDisplay display, string itemId, float duration)
        {
            yield return new WaitForSeconds(duration);

            if (display != null)
            {
                display.Hide(itemId);
            }
        }

        private IEnumerator RunAndClear(string key, IEnumerator routine)
        {
            yield return routine;
            runningEffects.Remove(key);
        }
    }
}
