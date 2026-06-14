using UnityEngine;
using UnityEngine.Tilemaps;

namespace App.Game.Item
{
    public readonly struct ItemEffectContext
    {
        public ItemEffectContext(GameObject player, Tilemap tilemap, Vector3Int cellPosition)
        {
            Player = player;
            Tilemap = tilemap;
            CellPosition = cellPosition;
        }

        public GameObject Player { get; }
        public Tilemap Tilemap { get; }
        public Vector3Int CellPosition { get; }
    }
}
