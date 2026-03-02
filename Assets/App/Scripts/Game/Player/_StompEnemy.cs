using UnityEngine;
using App.Game.Player;
public class _StompEnemy
{
    float pleyerY;
    float playerheight;
    float enemyY;
    float enemyHeight;
    GameObject enemy;
    _PlayerDamage playerDamage;
    public _StompEnemy(float playerheight, _PlayerDamage playerDamage)
    {
        this.playerheight = playerheight;
        this.playerDamage = playerDamage;
    }
    public GameObject OnCollisionEnemy(float playerY, Collider2D collisionInfo)
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
            }else{
                playerDamage.TakeDamage();
            }
        }
        return null;
    } 
}
