using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

namespace Game.Player.Move
{
    public class Jump
    {
        public bool IsGrounded(GameObject player)
        {
            // プレイヤーのコライダーのサイズを取得
            Collider2D collider = player.GetComponent<Collider2D>();
            if (collider == null)
            {
                Debug.LogError("Player does not have a Collider2D component.");
                return false;
            }

            // プレイヤーの足元にレイキャストを飛ばして地面に接触しているか確認
            Vector2 origin = new Vector2(player.transform.position.x, player.transform.position.y - collider.bounds.extents.y);
            RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.down, 0.1f);
            Debug.DrawRay(origin, Vector2.down * 0.1f, Color.red);

            if (hit.collider != null)
            {
                //Debug.Log("Grounded");
                return true;
            }
            return false;
        }
        public Jump()
        {
        }
        public Jump(GameObject player, float jumpForce, float maxJumpForce, InputAction.CallbackContext context)
        {
            // プレイヤーが地面についているか確認
            if (IsGrounded(player))
            {
                if (context.phase == InputActionPhase.Started)
                {
                    if (context.interaction is PressInteraction || context.interaction is HoldInteraction)
                    {
                        //Debug.Log("Jump Started");
                        player.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(player.GetComponent<Rigidbody2D>().linearVelocity.x, jumpForce);
                    }
                }
                if (context.phase == InputActionPhase.Performed && context.interaction is HoldInteraction hold)
                {
                    Debug.Log("Jump Hold");
                    player.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(player.GetComponent<Rigidbody2D>().linearVelocity.x, maxJumpForce);
                }
            }
        }
    }
}