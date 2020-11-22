using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class SlashRange : MonoBehaviour
    {
        private PlayerController _playerController;
        public PlayerController PlayerController
        {
            set { _playerController = value; }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Enemy"))
            {
                if (collision.GetComponent<IDamage>().GetDamage())
                {
                    _playerController.SuccessSlashAction?.Invoke();
                }
                else
                {
                    _playerController.FailedSlashAction?.Invoke();
                }
            }
        }
    }
}

