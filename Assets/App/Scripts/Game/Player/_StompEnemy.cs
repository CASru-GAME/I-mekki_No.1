using UnityEngine;

public class _StompEnemy
{
    float pleyerY;
    float playerheight;
    float enemyY;
    float enemyHeight;
    GameObject enemy;
    public _StompEnemy(float playerheight)
    {
        this.playerheight = playerheight;
    }
    public GameObject OnCollisionEnemy(float playerY, Collision2D collisionInfo)
    {
        if (collisionInfo.gameObject.tag == "Enemy")
        {
            this.pleyerY = playerY;
            enemy = collisionInfo.gameObject;
            enemyY = enemy.transform.position.y;
            enemyHeight = enemy.GetComponent<SpriteRenderer>().bounds.size.y;
            if(enemyY - enemyHeight / 2 < pleyerY - playerheight / 2)
            {
                Debug.Log("Enemy Stomped");
                return enemy;
            }
        }
        return null;
    } 
}
