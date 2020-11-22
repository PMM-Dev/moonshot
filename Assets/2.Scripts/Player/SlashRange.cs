using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class SlashRange : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Enemy"))
            {
                collision.GetComponent<IDamage>().GetDamage();
            }
        }
    }
}

