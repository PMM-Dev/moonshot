using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerFX : MonoBehaviour
    {
        private PlayerController _playerController;
        [SerializeField]
        private GameObject _slashTrail;

        public void InitializeEvent(PlayerController playerController)
        {
            _playerController = playerController;

            _slashTrail.SetActive(false);

            _playerController.SlashAction += OnTrail;
            _playerController.EndSlashAction += OffTrail;
        }

        private void OnTrail(LookDirection lookDirection, Vector3 slashDirection)
        {
            _slashTrail.SetActive(true);
            _slashTrail.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, slashDirection.z + 90f));
        }

        private void OffTrail()
        {
            _slashTrail.SetActive(false);
        }
    }
}

