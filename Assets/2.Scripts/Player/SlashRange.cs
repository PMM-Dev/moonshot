using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class SlashRange : MonoBehaviour
    {
        private static SlashRange _instance;
        public static SlashRange Instance
        {
            get
            {
                if (_instance == null)
                {
                    var obj = FindObjectOfType<SlashRange>();
                    if (obj != null)
                    {
                        _instance = obj;
                    }
                    else
                    {
                        var newSingletoneObject = Instantiate(Resources.Load("Player/SlashRange")) as GameObject;
                        var newSingleton = newSingletoneObject.transform.GetChild(0).GetComponent<SlashRange>();
                        _instance = newSingleton;
                    }
                }
                return _instance;
            }
            private set
            {
                _instance = value;
            }
        }

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

