using UnityEngine;
using UnityEngine.Tilemaps;

namespace App.Game.Item
{
    public readonly struct ItemEffectContext
    {
        public ItemEffectContext(GameObject player, Tilemap tilemap, Vector3Int cellPosition, ItemEffectRunner runner)
        {
            Player = player;
            Tilemap = tilemap;
            CellPosition = cellPosition;
            Runner = runner;
        }

        public GameObject Player { get; }
        public Tilemap Tilemap { get; }
        public Vector3Int CellPosition { get; }
        public ItemEffectRunner Runner { get; }
    }
}
