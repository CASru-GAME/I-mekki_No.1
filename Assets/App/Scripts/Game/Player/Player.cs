using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.InputSystem;
using App.Game.Player.Move;

namespace App.Game.Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private GameObject gameobject;
        [SerializeField] private float jumpForce = 5f;
        [SerializeField] private float jumping = 1f;
        [SerializeField] private float airTime = 0.4f;
        [SerializeField] private float speed = 1f;
        [SerializeField] private Collider2D playerCollider;
        [SerializeField] private Collider2D PlayerStompCollider;
        [SerializeField] private float invincibleTime = 2f;
        private Dash dash;
        private Jump jump;
        private MoveRight moveright;
        private _PlayerDamage playerDamage;


        void Start()
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            playerDamage = new _PlayerDamage(playerCollider, PlayerStompCollider, rb, invincibleTime);
            if (rb == null)
            {
                Debug.LogError("Rigidbody2D component not found on the player.");
                return;
            }

            jump = new Jump(gameobject);
            dash = new Dash(rb, jump, airTime, speed);
            moveright = new MoveRight(gameobject, speed);

        }

        public void OnJump(InputAction.CallbackContext context)
        {
            // Jump
            jump.PerformJump(jumpForce, jumping, context);
        }

        public async void OnDash(InputAction.CallbackContext context)
        {
            await dash.PerformDash(context);
        }

        void FixedUpdate()
        {
            dash.FixedUpdate();
            moveright.Move();
            jump.FixedUpdate();
        }
    }
}