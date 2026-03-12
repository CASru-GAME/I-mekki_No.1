using App.Common._Data;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace App.Game.Field
{
    public class Coin : MonoBehaviour
    {
        [SerializeField] private Tilemap tilemap;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent<App.Game.Player.Player>() == null) return;

            // 接触点ベースでセル取得（ズレ対策）
            Vector2 hitPoint = collision.ClosestPoint(transform.position);
            Vector3Int cell = tilemap.WorldToCell(hitPoint);

            // そのセルにタイルが無ければ、周囲も探索（境界ヒット対策）
            if (!tilemap.HasTile(cell))
            {
                bool found = false;
                for (int y = -1; y <= 1 && !found; y++)
                for (int x = -1; x <= 1 && !found; x++)
                {
                    var c = cell + new Vector3Int(x, y, 0);
                    if (!tilemap.HasTile(c)) continue;
                    cell = c;
                    found = true;
                }
                if (!found) return;
            }

            CollectCoin(cell);
        }

        private void CollectCoin(Vector3Int tilePosition)
        {
            _PlayerStatistics.AddCoins(1);
            tilemap.SetTile(tilePosition, null);
            tilemap.CompressBounds();
            tilemap.RefreshAllTiles();
        }
    }
}
