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

        private IEnumerator RunAndClear(string key, IEnumerator routine)
        {
            yield return routine;
            runningEffects.Remove(key);
        }
    }
}
