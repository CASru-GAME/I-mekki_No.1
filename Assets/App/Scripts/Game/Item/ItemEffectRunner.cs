using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.Game.Item
{
    public class ItemEffectRunner : MonoBehaviour
    {
        private readonly Dictionary<string, Coroutine> runningEffects = new Dictionary<string, Coroutine>();
        private readonly Dictionary<string, GameObject> activeEffectObjects = new Dictionary<string, GameObject>();
        private const int FrontEffectSortingOrderOffset = 2;
        private const int BackEffectSortingOrderOffset = -1;

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

        public void SpawnOneShotEffect(GameObject effectPrefab)
        {
            SpawnOneShotEffect(effectPrefab, BackEffectSortingOrderOffset, false);
        }

        public void SpawnOneShotEffect(GameObject effectPrefab, int sortingOrderOffset, bool preservePrefabWorldPosition)
        {
            if (effectPrefab == null)
            {
                return;
            }

            CreateAttachedEffect(effectPrefab, sortingOrderOffset, false, preservePrefabWorldPosition);
        }

        public void ShowEffectForDuration(string key, GameObject effectPrefab, float duration)
        {
            ShowEffectForDuration(key, effectPrefab, duration, BackEffectSortingOrderOffset, false, false);
        }

        public void ShowEffectForDuration(
            string key,
            GameObject effectPrefab,
            float duration,
            int sortingOrderOffset,
            bool preservePrefabWorldScale,
            bool preservePrefabWorldPosition)
        {
            if (string.IsNullOrEmpty(key) || effectPrefab == null || duration <= 0f)
            {
                return;
            }

            DestroyActiveEffect(key);

            GameObject effectObject = CreateAttachedEffect(
                effectPrefab,
                sortingOrderOffset,
                preservePrefabWorldScale,
                preservePrefabWorldPosition);
            activeEffectObjects[key] = effectObject;
            RunOrRestart($"effect:{key}", HideEffectAfter(key, effectObject, duration));
        }

        public void ShowBarrierEffectForDuration(string key, GameObject effectPrefab, float duration)
        {
            ShowEffectForDuration(
                key,
                effectPrefab,
                duration,
                FrontEffectSortingOrderOffset,
                true,
                true);
        }

        public void ShowFootEffectForDuration(string key, GameObject effectPrefab, float duration)
        {
            ShowEffectForDuration(
                key,
                effectPrefab,
                duration,
                BackEffectSortingOrderOffset,
                false,
                true);
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

        private GameObject CreateAttachedEffect(
            GameObject effectPrefab,
            int sortingOrderOffset,
            bool preservePrefabWorldScale,
            bool preservePrefabWorldPosition)
        {
            GameObject effectObject = Instantiate(effectPrefab, transform, false);
            PreservePrefabWorldTransform(
                effectObject.transform,
                preservePrefabWorldScale,
                preservePrefabWorldPosition);

            ConfigureEffectSorting(effectObject, sortingOrderOffset);
            return effectObject;
        }

        private void PreservePrefabWorldTransform(
            Transform effectTransform,
            bool preserveWorldScale,
            bool preserveWorldPosition)
        {
            if (!preserveWorldScale && !preserveWorldPosition)
            {
                return;
            }

            Vector3 parentScale = transform.lossyScale;

            if (preserveWorldPosition)
            {
                Vector3 localPosition = effectTransform.localPosition;
                effectTransform.localPosition = new Vector3(
                    DivideSafely(localPosition.x, parentScale.x),
                    DivideSafely(localPosition.y, parentScale.y),
                    DivideSafely(localPosition.z, parentScale.z));
            }

            if (preserveWorldScale)
            {
                Vector3 localScale = effectTransform.localScale;
                effectTransform.localScale = new Vector3(
                    DivideSafely(localScale.x, parentScale.x),
                    DivideSafely(localScale.y, parentScale.y),
                    DivideSafely(localScale.z, parentScale.z));
            }
        }

        private float DivideSafely(float value, float divisor)
        {
            return Mathf.Approximately(divisor, 0f) ? value : value / divisor;
        }

        private void ConfigureEffectSorting(GameObject effectObject, int sortingOrderOffset)
        {
            SpriteRenderer playerRenderer = GetComponent<SpriteRenderer>();
            if (playerRenderer == null)
            {
                return;
            }

            int sortingOrder = playerRenderer.sortingOrder + sortingOrderOffset;

            foreach (Renderer renderer in effectObject.GetComponentsInChildren<Renderer>(true))
            {
                renderer.sortingLayerID = playerRenderer.sortingLayerID;
                renderer.sortingOrder = sortingOrder;
            }
        }

        private IEnumerator HideEffectAfter(string key, GameObject effectObject, float duration)
        {
            yield return new WaitForSeconds(duration);
            HideEffect(key, effectObject);
        }

        private void DestroyActiveEffect(string key)
        {
            if (!activeEffectObjects.TryGetValue(key, out GameObject effectObject))
            {
                return;
            }

            if (effectObject != null)
            {
                Destroy(effectObject);
            }

            activeEffectObjects.Remove(key);
        }

        private void HideEffect(string key, GameObject effectObject)
        {
            if (activeEffectObjects.TryGetValue(key, out GameObject activeEffectObject) && activeEffectObject != effectObject)
            {
                return;
            }

            if (effectObject != null)
            {
                ShowBarrier barrier = effectObject.GetComponentInChildren<ShowBarrier>();
                if (barrier != null)
                {
                    barrier.DeleteBarrier();
                }
                else
                {
                    Destroy(effectObject);
                }
            }

            activeEffectObjects.Remove(key);
        }

        private IEnumerator RunAndClear(string key, IEnumerator routine)
        {
            yield return routine;
            runningEffects.Remove(key);
        }
    }
}
