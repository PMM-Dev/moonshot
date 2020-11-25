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
        [SerializeField]
        private DashGhostFxPool _slashGhost;

        private ParticleSystem[] trails;

        private Coroutine _ghostCoroutine;

        public void InitializeEvent(PlayerController playerController)
        {
            _playerController = playerController;

            _slashTrail.SetActive(false);

            _playerController.SlashAction += OnTrail;
            _playerController.EndSlashAction += OffTrail;

            trails = _slashTrail.GetComponentsInChildren<ParticleSystem>();
            OffTrail();
        }

        private void OnTrail(LookDirection lookDirection, Vector3 slashDirection)
        {
            _slashTrail.SetActive(true);
            _slashTrail.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, slashDirection.z + 90f));
            // _ghostCoroutine = StartCoroutine(_slashGhost.PlayGhostFx(lookDirection == LookDirection.Left));
            for (int i = 0; i < trails.Length; i++)
            {
                trails[i].Play();
            }
        }

        private void OffTrail()
        {
            for (int i = 0; i < trails.Length; i++)
            {
                trails[i].Stop();
            }

            //StartCoroutine(SetTrail(false));
            // StopCoroutine(_ghostCoroutine);
        }

        private IEnumerator SetTrail(bool isOn)
        {
            yield return new WaitForSeconds(0.2f);
            _slashTrail.SetActive(false);
            yield return null;
        }
    }
}

