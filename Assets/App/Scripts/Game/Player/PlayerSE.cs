using UnityEngine;
using App.Game.Player.Move;

namespace App.Game.Player
{
    public class PlayerSE : MonoBehaviour
    {
        [SerializeField] private AudioSource seSource;
        [SerializeField] private AudioSource ActionseSource;
        [SerializeField] private AudioClip jumpClip;
        [SerializeField] private AudioClip dashClip;
        [SerializeField] private AudioClip stompClip;
        [SerializeField] private AudioClip damageClip;
        [SerializeField] private AudioClip coinClip;
        [SerializeField] private AudioClip dictionaryClip;

        public void Bind(Jump jump, Dash dash)
        {
            jump.OnJumpStarted += PlayJump;
            jump.OnStompJump += PlayStomp;
            dash.OnDashStarted += PlayDash;
        }

        private void PlayAction(AudioClip clip)
        {
            if(ActionseSource == null) return;
            ActionseSource.Stop();
            ActionseSource.clip = clip;
            ActionseSource.Play();
        }

        private void PlayJump()  => PlayAction(jumpClip);
        private void PlayDash()  => PlayAction(dashClip);
        private void PlayStomp() => PlayAction(stompClip);
        public void PlayDamage() => seSource.PlayOneShot(damageClip);
        public void PlayCoin()   => seSource.PlayOneShot(coinClip);
        public void PlayDictionary() => seSource.PlayOneShot(dictionaryClip);
    }
}
