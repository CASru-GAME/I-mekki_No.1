using UnityEngine;
using Game.Player.Move;
using UnityEngine.InputSystem;
using NUnit.Framework;

namespace Game.Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private GameObject gameobject;
        [SerializeField] private float jumpForce = 5f;
        [SerializeField] private float jumping = 1f;
        private Dash dash;

        void Start()
        {
            dash = GetComponent<Dash>();
            if (dash == null)
            {
                Debug.LogError("Dash component not found on the player.");
            }
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            // Jump
            new Move.Jump(gameobject, jumpForce, jumping, context);
        }

        public void OnDash(InputAction.CallbackContext context)
        {
                Debug.Log("Dash");
                dash.PerformDash(gameObject,context);
        }
    }
}