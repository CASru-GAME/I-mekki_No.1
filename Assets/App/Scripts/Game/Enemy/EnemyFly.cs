using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.Scripts.Game.Enemy
{
    public class EnemyFly : EnemyCrushed
    {
        void FixedUpdate()
        {
            UpdatePosition();
        }
        private void UpdatePosition()
        {
            //
        }
        
        // private void OnCollisionEnter2D(Collision2D collision)
        // {
            
        // }
    }
}
