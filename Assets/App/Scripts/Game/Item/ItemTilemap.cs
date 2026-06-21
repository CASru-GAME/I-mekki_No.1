using UnityEngine;
using UnityEngine.Tilemaps;

namespace App.Game.Item
{
    public class ItemTilemap : MonoBehaviour
    {
        [SerializeField] private Tilemap tilemap;
        [SerializeField] private string itemId;
        [SerializeField] private ItemEffectDatabase effectDatabase;

        private void Reset()
        {
            tilemap = GetComponent<Tilemap>();
        }

        private void Awake()
        {
            if (tilemap == null)
            {
                tilemap = GetComponent<Tilemap>();
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            TryUseItem(collision);
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            TryUseItem(collision);
        }

        private void TryUseItem(Collider2D collision)
        {
            global::App.Game.Player.Player player = collision.GetComponent<global::App.Game.Player.Player>();
            if (player == null || tilemap == null || effectDatabase == null || string.IsNullOrEmpty(itemId))
            {
                return;
            }

            if (!TryGetTouchedCell(collision, out Vector3Int cell))
            {
                return;
            }

            if (!effectDatabase.TryGetEffect(itemId, out ItemEffectBase effect))
            {
                Debug.LogWarning($"Item effect not found. itemId: {itemId}", this);
                return;
            }

            ItemEffectRunner runner = GetOrAddRunner(player.gameObject);
            player.ConfigureActiveItemIconDisplay(runner);
            ItemEffectContext context = new ItemEffectContext(player.gameObject, tilemap, cell, runner);
            effect.Apply(context);
            player.PlayItemSound(effect);

            if (effect.ShowsActiveIcon)
            {
                runner.ShowActiveIcon(effect.ItemId, effect.Icon, effect.EffectDuration);
            }

            RemoveTile(cell);
        }

        private ItemEffectRunner GetOrAddRunner(GameObject player)
        {
            if (!player.TryGetComponent(out ItemEffectRunner runner))
            {
                runner = player.AddComponent<ItemEffectRunner>();
            }

            return runner;
        }

        private bool TryGetTouchedCell(Collider2D collision, out Vector3Int cell)
        {
            Vector2 hitPoint = collision.ClosestPoint(transform.position);
            cell = tilemap.WorldToCell(hitPoint);

            if (tilemap.HasTile(cell))
            {
                return true;
            }

            for (int y = -1; y <= 1; y++)
            {
                for (int x = -1; x <= 1; x++)
                {
                    Vector3Int nearCell = cell + new Vector3Int(x, y, 0);
                    if (!tilemap.HasTile(nearCell))
                    {
                        continue;
                    }

                    cell = nearCell;
                    return true;
                }
            }

            return false;
        }

        private void RemoveTile(Vector3Int cell)
        {
            tilemap.SetTile(cell, null);
            tilemap.CompressBounds();
            tilemap.RefreshAllTiles();
        }
    }
}
