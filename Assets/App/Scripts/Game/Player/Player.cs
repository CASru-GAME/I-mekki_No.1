using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.InputSystem;
using App.Scripts.Game.Player.Move;

namespace App.Scripts.Game.Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private GameObject gameobject;
        [SerializeField] private float jumpForce = 5f;
        [SerializeField] private float jumping = 1f;
        [SerializeField] private float airTime = 1f;
        private Dash dash;
        private Jump jump;

        void Start()
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            if (rb == null)
            {
                Debug.LogError("Rigidbody2D component not found on the player.");
                return;
            }

            jump = new Jump(gameobject);
            dash = new Dash(rb, jump, airTime);
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
        }
    }
}