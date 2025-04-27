using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

namespace App.Game.Player.Move
{
    public class MoveRight
    {
        private float speed;
        private GameObject camera;
        Rigidbody2D rb;
        public MoveRight(GameObject player, float speed)
        {
            this.speed = speed;
            rb = player.GetComponent<Rigidbody2D>();
            camera = GameObject.Find("Main Camera");//後で変える
        }

        public void Move()
        {
            rb.linearVelocity = new Vector2(speed, rb.linearVelocity.y);
            if(camera.transform.position.x -2.1> rb.transform.position.x){
                rb.linearVelocity = new Vector2(speed+0.5f, rb.linearVelocity.y);
            }
        }
    }
}