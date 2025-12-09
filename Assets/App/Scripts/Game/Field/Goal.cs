using UnityEngine;
using UnityEngine.Tilemaps;

public class Goal : MonoBehaviour
{
    [SerializeField] private string ResultSceneName = "ResultScene";
    [SerializeField] private GameObject Player;
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private TileBase goalTile; // ゴールタイルの種類を指定
    
    private Vector3 posPlayer;
    private Vector3 goalPosition;

    void Start()
    {
        // 指定されたタイルの種類を検索してワールド座標を取得
        if (tilemap != null && goalTile != null)
        {
            BoundsInt bounds = tilemap.cellBounds;
            
            for (int x = bounds.xMin; x < bounds.xMax; x++)
            {
                for (int y = bounds.yMin; y < bounds.yMax; y++)
                {
                    Vector3Int cellPosition = new Vector3Int(x, y, 0);
                    
                    // 指定したタイルの種類と一致するかチェック
                    if (tilemap.GetTile(cellPosition) == goalTile)
                    {
                        // セルの中心のワールド座標を取得
                        goalPosition = tilemap.GetCellCenterWorld(cellPosition);
                        
                        Debug.Log($"Goal tile found at: {goalPosition}");
                        break;
                    }
                }
            }
        }
    }

    void FixedUpdate()
    {
        posPlayer = Player.transform.position;
        if (goalPosition.x < posPlayer.x)
        {
            Debug.Log("Goal Triggered");
            UnityEngine.SceneManagement.SceneManager.LoadScene(ResultSceneName);
        }
    }
}