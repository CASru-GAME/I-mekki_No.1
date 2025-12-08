using UnityEngine;

public class Goal : MonoBehaviour
{
    [SerializeField] private string ResultSceneName = "ResultScene";
    [SerializeField] private GameObject Player;
    private Vector3 posPlayer;

    void FixedUpdate()
    {
        posPlayer = Player.transform.position;
        if (this.transform.position.x < posPlayer.x)
        {
            Debug.Log("Goal Triggered");
            UnityEngine.SceneManagement.SceneManager.LoadScene(ResultSceneName);
        }
    }

}
