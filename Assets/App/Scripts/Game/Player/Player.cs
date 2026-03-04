using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.InputSystem;
using App.Game.Player.Move;

namespace App.Game.Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private GameObject gameobject;
        [SerializeField] private float maxjumpheight = 30f;
        [SerializeField] private float minjumpheight = 5f;
        [SerializeField] private int maxJumpCount = 25;
        [SerializeField] private float airTime = 0.4f;
        [SerializeField] private float speed = 1f;
        [SerializeField] private Collider2D playerCollider;
        [SerializeField] private Collider2D PlayerStompCollider;
        [SerializeField] private float invincibleTime = 2f;
        [SerializeField] private float flashDuration = 0.1f;
        private Dash dash;
        private Jump jump;
        private MoveRight moveright;
        private _PlayerDamage playerDamage;
        private _StompEnemy stompEnemy;
        int playerLayer;
        int enemyLayer;
        private Animator animator;

        void Start()
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            if (rb == null)
            {
                Debug.LogError("Rigidbody2D component not found on the player.");
                return;
            }
            animator = GetComponent<Animator>();
            jump = new Jump(animator, gameobject, maxjumpheight, minjumpheight, maxJumpCount);
            dash = new Dash(rb, jump, airTime, speed, animator);
            moveright = new MoveRight(gameobject, speed);
            playerLayer = LayerMask.NameToLayer("Player");
            enemyLayer  = LayerMask.NameToLayer("Enemy");
            playerDamage = new _PlayerDamage(playerLayer, enemyLayer, invincibleTime, flashDuration, GetComponent<SpriteRenderer>());
            stompEnemy = new _StompEnemy(playerCollider.bounds.size.y, playerDamage);
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            // Jump
            jump.PerformJump(context);
        }

        public async void OnDash(InputAction.CallbackContext context)
        {
            await dash.PerformDash(context);
        }

        public void OnTriggerEnter2D(Collider2D collisionInfo)
        {
            GameObject stompedEnemy = stompEnemy.OnCollisionEnemy(transform.position.y, collisionInfo);
            if (stompedEnemy != null)
            {
                Destroy(stompedEnemy);
                jump.StompEnemyJump(); // ジャンプさせる
            }
        }

        void FixedUpdate()
        {
            dash.FixedUpdate();
            moveright.Move();
            jump.FixedUpdate();
            //playerDamage.FixedUpdate();

            if(animator.GetCurrentAnimatorStateInfo(0).IsName("Jump")){
                Debug.Log("Jumping");
            }
            if(animator.GetCurrentAnimatorStateInfo(0).IsName("Dash")){
                Debug.Log("Dashing");
            }
        }
    }
}