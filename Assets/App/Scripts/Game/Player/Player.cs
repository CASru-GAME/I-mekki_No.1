using UnityEngine;
using UnityEngine.InputSystem;
using App.Game.Item;
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
        [SerializeField] private float outofscreenaddspeed = 0.5f;
        [SerializeField] private Collider2D playerCollider;
        [SerializeField] private Collider2D PlayerStompCollider;
        [SerializeField] private float invincibleTime = 2f;
        [SerializeField] private float flashDuration = 0.1f;
        [SerializeField] private float offScreenTimeout = 2f;
        [SerializeField] private GameObject camera;
        [SerializeField] private bool inwater = false;
        [SerializeField] private float Itemmaxjumpheight = 6f;
        [SerializeField] private float Itemminjumpheight = 3f;
        [SerializeField] private float ItemactiveTime = 4f;
        [Header("Active Item Icon")]
        [SerializeField] private Vector2 activeItemIconOffset = new Vector2(0.35f, 0.35f);
        [SerializeField] private float activeItemIconSize = 0.32f;
        [SerializeField] private float activeItemIconSpacing = 0.08f;
        [SerializeField] private int activeItemIconSortingOrderOffset = 10;
        [SerializeField] private Vector2 activeItemTimerOffset = new Vector2(0f, -0.34f);
        [SerializeField] private int activeItemTimerFontSize = 32;
        [SerializeField] private float activeItemTimerCharacterSize = 0.06f;
        [SerializeField] private Color activeItemTimerColor = Color.white;
        [SerializeField] private Font activeItemTimerFont;
        private Dash dash;
        private Jump jump;
        private MoveRight moveright;
        private _PlayerDamage playerDamage;
        private _StompEnemy stompEnemy;
        int playerLayer;
        int enemyLayer;
        private Animator animator;
        private PlayerSE playerSE;

        void Start()
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            if (rb == null)
            {
                Debug.LogError("Rigidbody2D component not found on the player.");
                return;
            }
            playerSE = GetComponent<PlayerSE>();
            animator = GetComponent<Animator>();
            jump = new Jump(animator, gameobject, maxjumpheight, minjumpheight, maxJumpCount, inwater, Itemmaxjumpheight, Itemminjumpheight, ItemactiveTime);
            dash = new Dash(rb, jump, airTime, speed, inwater, animator);
            moveright = new MoveRight(gameobject, speed, offScreenTimeout, camera, outofscreenaddspeed);
            playerLayer = LayerMask.NameToLayer("Player");
            enemyLayer  = LayerMask.NameToLayer("Enemy");
            playerDamage = new _PlayerDamage(playerLayer, enemyLayer, invincibleTime, flashDuration, GetComponent<SpriteRenderer>(), playerSE, this);
            stompEnemy = new _StompEnemy(playerCollider.bounds.size.y, playerDamage);
            playerSE.Bind(jump, dash);
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

        public void OnTriggerStay2D(Collider2D collisionInfo)
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

            /*if(animator.GetCurrentAnimatorStateInfo(0).IsName("Jump")){
            }
            if(animator.GetCurrentAnimatorStateInfo(0).IsName("Dash")){
            }*/
        }
        
        public void ActiveInvincibility()
        {
            playerDamage.StartInvincibility();
        }

        public void ActivateJumpEffect(float duration = 0f, float maxJumpHeightBonus = 0f, float minJumpHeightBonus = 0f)
        {
            jump.ItemActive(duration, maxJumpHeightBonus, minJumpHeightBonus);
        }

        public void ConfigureActiveItemIconDisplay(ItemEffectRunner runner)
        {
            if (runner == null)
            {
                return;
            }

            runner.ConfigureActiveIconDisplay(
                activeItemIconOffset,
                activeItemIconSize,
                activeItemIconSpacing,
                activeItemIconSortingOrderOffset,
                activeItemTimerOffset,
                activeItemTimerFontSize,
                activeItemTimerCharacterSize,
                activeItemTimerColor,
                activeItemTimerFont);
        }

        public void PlayItemSound(ItemEffectBase effect)
        {
            if (effect == null || !effect.HasSoundEffect || playerSE == null)
            {
                return;
            }

            playerSE.PlayItem(effect.SoundEffect, effect.SoundEffectVolume);
        }
    }
}
