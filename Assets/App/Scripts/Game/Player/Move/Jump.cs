using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using DG.Tweening;
using UnityEditor.Callbacks;
using System;

namespace App.Game.Player.Move
{
    public class Jump
    {
        private GameObject player;
        private Tween jumpTween;
        private float jumpCooldown = 0.3f; // ジャンプ後のクールタイム（秒）
        private float lastJumpTime = -1f;
        private bool isJumping = false;
        //private float jumpForce = 30f;
        //private float FirstjumpForce = 5f;
        private float FisrtJumpVelocity;
        private float MaxJumpForce;
        private float jumprange;
        private int maxJumpCount = 25;
        private int count = 0;
        private float gravityScale;
        private float mass;
        private float maxjumptime;
        private float jumpstarttime;
        private float maxjump;
        private float gravity;
        private float jumprangetime;
        Rigidbody2D rb;
        public Jump(GameObject player, float maxjump, float minjump, float jumprange, float maxjumptime = 0.1f, float jumprangetime = 0.05f)
        {
            this.player = player;
            rb = player.GetComponent<Rigidbody2D>();
            gravityScale = rb.gravityScale;
            gravity = Math.Abs(Physics2D.gravity.y * gravityScale);
            mass = rb.mass;
            this.FisrtJumpVelocity = Mathf.Sqrt(2 * gravity * minjump);
            this.maxjump = maxjump;
            this.jumprange = jumprange;
            this.maxjumptime = maxjumptime;
            this.jumprangetime = jumprangetime;
        }

        public void PerformJump(InputAction.CallbackContext context)
        {
            if (Time.time - lastJumpTime < jumpCooldown){
                isJumping = false;
                return;
            }
            
            if (IsGrounded())
            {
                if (context.phase == InputActionPhase.Started){
                    rb.linearVelocity = new Vector2(rb.linearVelocity.x, FisrtJumpVelocity);
                    count = 0;
                    isJumping = true;
                    lastJumpTime = Time.time;
                    jumpstarttime = Time.time;
                }
            }
            // ジャンプボタンを離したら上昇終了
                if (context.phase == InputActionPhase.Canceled)
                {
                    //Debug.Log(Time.time - lastJumpTime);
                    isJumping = false;
                    count = 0;
                    /*float holdtime = Time.time - lastJumpTime;
                    if(holdtime < maxjumptime-jumprangetime){
                        float jumpVelocity = Mathf.Sqrt(2 * gravity * (maxjump - jumprange));
                        rb.AddForce(new Vector2(0, rb.mass * (jumpVelocity - rb.linearVelocity.y)), ForceMode2D.Impulse);
                    }else if(holdtime < maxjumptime){
                        float jumpVelocity = Mathf.Sqrt(2 * gravity * maxjump);
                        rb.AddForce(new Vector2(0, rb.mass * (jumpVelocity - rb.linearVelocity.y)), ForceMode2D.Impulse);
                    }else{
                        float jumpVelocity = Mathf.Sqrt(2 * gravity * (maxjump + jumprange));
                        rb.AddForce(new Vector2(0, rb.mass * (jumpVelocity - rb.linearVelocity.y)), ForceMode2D.Impulse);
                    }*/
                }
        }

        public void CancelJump()
        {
            if (jumpTween != null && jumpTween.IsActive())
            {
                jumpTween.Kill();
            }
        }

        public bool IsGrounded()
        {
            // プレイヤーのコライダーのサイズを取得
            Collider2D collider = player.GetComponent<Collider2D>();
            if (collider == null)
            {
                Debug.LogError("Player does not have a Collider2D component.");
                return false;
            }

            // プレイヤーの足元にレイキャストを飛ばして地面に接触しているか確認
            Vector2 origin = new Vector2(player.transform.position.x, player.transform.position.y - collider.bounds.extents.y - 0.1f);
            RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.down, 0.2f);
            Debug.DrawRay(origin, Vector2.down * 0.2f, Color.red);

            if (hit.collider != null && !hit.collider.gameObject.CompareTag("Player"))
            {
                return true;
            }
            return false;
        }
        public bool IsTouchingCeiling(){
            // プレイヤーのコライダーのサイズを取得
            Collider2D collider = player.GetComponent<Collider2D>();
            if (collider == null)
            {
                Debug.LogError("Player does not have a Collider2D component.");
                return false;
            }

            // プレイヤーの頭上にレイキャストを飛ばして天井に接触しているか確認
            Vector2 origin = new Vector2(player.transform.position.x, player.transform.position.y + collider.bounds.extents.y + 0.1f);
            RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.up, 0.2f);
            Debug.DrawRay(origin, Vector2.up * 0.2f, Color.red);

            if (hit.collider != null)
            {
                return true;
            }
            return false;
        }
        public void FixedUpdate()
        {

            if(isJumping)
            {
                count ++;
                Debug.Log(Time.time - jumpstarttime);
                if(Time.time - jumpstarttime > maxjumptime){
                    float jumpVelocity = Mathf.Sqrt(2 * gravity * maxjump);
                    rb.AddForce(new Vector2(0, rb.mass * (jumpVelocity - rb.linearVelocity.y)), ForceMode2D.Impulse);
                    isJumping = false;
                    jumpstarttime = 0f;
                }
                
                /*if(Time.time - jumpstarttime > maxjumptime - jumprangetime){
                    float jumpVelocity = Mathf.Sqrt(2 * gravity * (maxjump - jumprange));
                    rb.AddForce(new Vector2(0, rb.mass * (jumpVelocity - rb.linearVelocity.y)), ForceMode2D.Impulse);
                    //isJumping = false;
                    //jumpstarttime = 0f;
                }else if(Time.time - jumpstarttime > maxjumptime){
                    float jumpVelocity = Mathf.Sqrt(2 * gravity * maxjump);
                    rb.AddForce(new Vector2(0, rb.mass * (jumpVelocity - rb.linearVelocity.y)), ForceMode2D.Impulse);
                    //isJumping = false;
                    //jumpstarttime = 0f;
                }else if(Time.time - jumpstarttime > maxjumptime + jumprangetime){
                    float jumpVelocity = Mathf.Sqrt(2 * gravity * (maxjump + jumprange));
                    rb.AddForce(new Vector2(0, rb.mass * (jumpVelocity - rb.linearVelocity.y)), ForceMode2D.Impulse);
                    isJumping = false;
                    jumpstarttime = 0f;
                }*/
            }

            // プレイヤーのY座標を固定する処理をここに追加
            if (IsTouchingCeiling())
            {
                CancelJump();
                isJumping = false;
                count = 0;
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, Mathf.Min(rb.linearVelocity.y, -1f)); // 落下を促すためにy軸の速度を調整
            }
        }
    }
}