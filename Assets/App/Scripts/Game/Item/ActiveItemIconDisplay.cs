using System.Collections.Generic;
using UnityEngine;

namespace App.Game.Item
{
    public class ActiveItemIconDisplay : MonoBehaviour
    {
        [SerializeField] private Vector2 offset = new Vector2(0.35f, 0.35f);
        [SerializeField] private float iconSize = 0.32f;
        [SerializeField] private float spacing = 0.08f;
        [SerializeField] private int sortingOrderOffset = 10;
        [SerializeField] private Vector2 timerOffset = new Vector2(0f, -0.34f);
        [SerializeField] private int timerFontSize = 32;
        [SerializeField] private float timerCharacterSize = 0.06f;
        [SerializeField] private Color timerColor = Color.white;
        [SerializeField] private Font timerFont;

        private readonly Dictionary<string, ActiveIcon> activeIcons = new Dictionary<string, ActiveIcon>();
        private SpriteRenderer playerRenderer;

        private void Awake()
        {
            playerRenderer = GetComponent<SpriteRenderer>();
        }

        public void Configure(
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
            this.offset = offset;
            this.iconSize = iconSize;
            this.spacing = spacing;
            this.sortingOrderOffset = sortingOrderOffset;
            this.timerOffset = timerOffset;
            this.timerFontSize = timerFontSize;
            this.timerCharacterSize = timerCharacterSize;
            this.timerColor = timerColor;
            this.timerFont = timerFont;

            foreach (ActiveIcon activeIcon in activeIcons.Values)
            {
                ConfigureSorting(activeIcon.Renderer);
                ConfigureTimer(activeIcon.TimerText);
                UpdateIconScale(activeIcon.Renderer);
            }

            UpdateIconPositions();
            UpdateTimerTexts(Time.time);
        }

        private void LateUpdate()
        {
            float now = Time.time;
            List<string> expiredKeys = null;

            foreach (KeyValuePair<string, ActiveIcon> pair in activeIcons)
            {
                if (now < pair.Value.EndTime)
                {
                    continue;
                }

                if (expiredKeys == null)
                {
                    expiredKeys = new List<string>();
                }

                expiredKeys.Add(pair.Key);
            }

            if (expiredKeys != null)
            {
                foreach (string key in expiredKeys)
                {
                    Remove(key);
                }
            }

            UpdateIconPositions();
            UpdateTimerTexts(now);
        }

        public void Show(string itemId, Sprite icon, float duration)
        {
            if (string.IsNullOrEmpty(itemId) || icon == null || duration <= 0f)
            {
                return;
            }

            if (!activeIcons.TryGetValue(itemId, out ActiveIcon activeIcon))
            {
                activeIcon = CreateIcon(itemId);
                activeIcons[itemId] = activeIcon;
            }

            activeIcon.Renderer.sprite = icon;
            activeIcon.EndTime = Time.time + duration;
            ConfigureSorting(activeIcon.Renderer);
            ConfigureTimer(activeIcon.TimerText);
            UpdateIconScale(activeIcon.Renderer);
            UpdateIconPositions();
            UpdateTimerTexts(Time.time);
        }

        public void Hide(string itemId)
        {
            Remove(itemId);
            UpdateIconPositions();
        }

        private ActiveIcon CreateIcon(string itemId)
        {
            GameObject iconObject = new GameObject($"ActiveItemIcon_{itemId}");
            iconObject.transform.SetParent(null);

            SpriteRenderer iconRenderer = iconObject.AddComponent<SpriteRenderer>();
            ConfigureSorting(iconRenderer);

            GameObject timerObject = new GameObject($"ActiveItemTimer_{itemId}");
            timerObject.transform.SetParent(null);

            TextMesh timerText = timerObject.AddComponent<TextMesh>();
            ConfigureTimer(timerText);

            return new ActiveIcon(iconObject.transform, iconRenderer, timerObject.transform, timerText);
        }

        private void UpdateIconPositions()
        {
            if (activeIcons.Count == 0)
            {
                return;
            }

            Vector3 basePosition = GetBasePosition();
            int index = 0;

            foreach (ActiveIcon activeIcon in activeIcons.Values)
            {
                Vector3 iconPosition = basePosition + new Vector3(index * (iconSize + spacing), 0f, 0f);
                activeIcon.Transform.position = iconPosition;
                activeIcon.TimerTransform.position = iconPosition + new Vector3(timerOffset.x, timerOffset.y, 0f);
                index++;
            }
        }

        private void UpdateTimerTexts(float now)
        {
            foreach (ActiveIcon activeIcon in activeIcons.Values)
            {
                float remaining = Mathf.Max(0f, activeIcon.EndTime - now);
                activeIcon.TimerText.text = Mathf.CeilToInt(remaining).ToString();
            }
        }

        private Vector3 GetBasePosition()
        {
            if (playerRenderer == null)
            {
                playerRenderer = GetComponent<SpriteRenderer>();
            }

            if (playerRenderer != null)
            {
                Bounds bounds = playerRenderer.bounds;
                return new Vector3(bounds.max.x + offset.x, bounds.max.y + offset.y, transform.position.z);
            }

            return transform.position + new Vector3(offset.x, offset.y, 0f);
        }

        private void ConfigureSorting(SpriteRenderer iconRenderer)
        {
            if (playerRenderer == null)
            {
                playerRenderer = GetComponent<SpriteRenderer>();
            }

            if (playerRenderer == null)
            {
                iconRenderer.sortingOrder = sortingOrderOffset;
                return;
            }

            iconRenderer.sortingLayerID = playerRenderer.sortingLayerID;
            iconRenderer.sortingOrder = playerRenderer.sortingOrder + sortingOrderOffset;
        }

        private void ConfigureTimer(TextMesh timerText)
        {
            if (timerText == null)
            {
                return;
            }

            timerText.anchor = TextAnchor.MiddleCenter;
            timerText.alignment = TextAlignment.Center;
            timerText.fontSize = timerFontSize;
            timerText.characterSize = timerCharacterSize;
            timerText.color = timerColor;

            if (timerFont != null)
            {
                timerText.font = timerFont;
            }

            Renderer timerRenderer = timerText.GetComponent<Renderer>();
            if (timerRenderer == null)
            {
                return;
            }

            if (timerFont != null)
            {
                timerRenderer.sharedMaterial = timerFont.material;
            }

            if (playerRenderer == null)
            {
                playerRenderer = GetComponent<SpriteRenderer>();
            }

            if (playerRenderer != null)
            {
                timerRenderer.sortingLayerID = playerRenderer.sortingLayerID;
                timerRenderer.sortingOrder = playerRenderer.sortingOrder + sortingOrderOffset + 1;
            }
            else
            {
                timerRenderer.sortingOrder = sortingOrderOffset + 1;
            }
        }

        private void UpdateIconScale(SpriteRenderer iconRenderer)
        {
            Sprite sprite = iconRenderer.sprite;
            if (sprite == null)
            {
                return;
            }

            Vector2 spriteSize = sprite.bounds.size;
            float largestSide = Mathf.Max(spriteSize.x, spriteSize.y);
            if (largestSide <= 0f)
            {
                return;
            }

            float scale = iconSize / largestSide;
            iconRenderer.transform.localScale = new Vector3(scale, scale, 1f);
        }

        private void Remove(string itemId)
        {
            if (!activeIcons.TryGetValue(itemId, out ActiveIcon activeIcon))
            {
                return;
            }

            if (activeIcon.Transform != null)
            {
                Destroy(activeIcon.Transform.gameObject);
            }

            if (activeIcon.TimerTransform != null)
            {
                Destroy(activeIcon.TimerTransform.gameObject);
            }

            activeIcons.Remove(itemId);
        }

        private void OnDisable()
        {
            ClearIcons();
        }

        private void OnDestroy()
        {
            ClearIcons();
        }

        private void ClearIcons()
        {
            foreach (ActiveIcon activeIcon in activeIcons.Values)
            {
                if (activeIcon.Transform != null)
                {
                    Destroy(activeIcon.Transform.gameObject);
                }

                if (activeIcon.TimerTransform != null)
                {
                    Destroy(activeIcon.TimerTransform.gameObject);
                }
            }

            activeIcons.Clear();
        }

        private sealed class ActiveIcon
        {
            public ActiveIcon(Transform transform, SpriteRenderer renderer, Transform timerTransform, TextMesh timerText)
            {
                Transform = transform;
                Renderer = renderer;
                TimerTransform = timerTransform;
                TimerText = timerText;
            }

            public Transform Transform { get; }
            public SpriteRenderer Renderer { get; }
            public Transform TimerTransform { get; }
            public TextMesh TimerText { get; }
            public float EndTime { get; set; }
        }
    }
}
