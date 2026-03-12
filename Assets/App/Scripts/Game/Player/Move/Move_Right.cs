using UnityEngine;
using App.Common._Data;

namespace App.Game.Player.Move
{
    public class MoveRight
    {
        private float speed;
        private GameObject camera;
        Rigidbody2D rb;
        private int count=0;
        private float offScreenTimeout;
        private float offScreenspeed;
        public MoveRight(GameObject player, float speed, float offScreentimeout, GameObject camera, float offScreenspeed)
        {
            this.speed = speed;
            rb = player.GetComponent<Rigidbody2D>();
            this.camera = camera;
            this.offScreenspeed = offScreenspeed;
            this.offScreenTimeout = offScreentimeout;
        }

        public void Move()
        {
            rb.linearVelocity = new Vector2(speed, rb.linearVelocity.y);
            if(camera.transform.position.x -2.1> rb.transform.position.x){
                rb.linearVelocity = new Vector2(speed+offScreenspeed, rb.linearVelocity.y);
                count++;
                if(count>offScreenTimeout/Time.fixedDeltaTime){
                    _PlayerStatus.SubHp();
                    Debug.Log("Player fell off the screen! HP: " + _PlayerStatus.GetHp());
                    count=0;
                }
            }else{
                count=0;
            }
        }
    }
}