using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class Brass : Projectile
    {
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player") == true)
            {
                collision.gameObject.GetComponent<IDamage>().GetDamage();
            }
        }
    }
}