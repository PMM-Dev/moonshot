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

        private ParticleSystem[] _trails;

        [SerializeField]
        private ParticleSystem _jumpParticle;
        [SerializeField]
        private ParticleSystem _grapParticle;

        private Coroutine _ghostCoroutine;
        private Coroutine _jumpCoroutine;
        private Coroutine _grapCoroutine;

        private Transform _jumpParent;

        public void InitializeEvent(PlayerController playerController)
        {
            _playerController = playerController;

            _slashTrail.SetActive(false);

            _playerController.SlashAction += OnTrail;
            _playerController.EndSlashAction += OffTrail;
            _playerController.NormalJumpAction += JumpFX;
            _playerController.WallJumpAction += JumpFX;
            _playerController.StickAction += OnGrapFX;
            _playerController.StopStickAction += OffGrapFX;

            _trails = _slashTrail.GetComponentsInChildren<ParticleSystem>();

            _slashGhost = GetComponentInChildren<DashGhostFxPool>();
            for (int i = 0; i < _trails.Length; i++)
            {
                _trails[i].Stop();
            }
            _jumpParticle.Stop();
            _grapParticle.Stop();

            _jumpParent = _jumpParticle.transform.parent.transform;
        }

        private void OnTrail(LookDirection lookDirection, Vector3 slashDirection)
        {
            _slashTrail.SetActive(true);
            _slashTrail.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, slashDirection.z + 90f));

            for (int i = 0; i < _trails.Length; i++)
            {
                _trails[i].Play();
            }

            if (_ghostCoroutine != null)
                StopCoroutine(_ghostCoroutine);
            _ghostCoroutine = StartCoroutine(_slashGhost.PlayGhostFx(lookDirection == LookDirection.Left));
        }

        private void OffTrail()
        {
            for (int i = 0; i < _trails.Length; i++)
            {
                _trails[i].Stop();
            }
            StopCoroutine(_ghostCoroutine);
        }

        private void JumpFX()
        {
            if (_jumpCoroutine != null)
            {
                _jumpParent.transform.parent = _jumpParent;
                _jumpParticle.Stop();
                _jumpCoroutine = null;
            }

            _jumpCoroutine = StartCoroutine(OnJumpParticle());
        }
        
        private void OnGrapFX(LookDirection lookDirection)
        {
            if (_grapParticle.isStopped)
            {
                _grapParticle.transform.localPosition = new Vector3(4f * (int)lookDirection, 0f, 0f);
                _grapParticle.Play();
            }
        }

        private void OffGrapFX()
        {
            if (!_grapParticle.isStopped)
            {
                _grapParticle.Stop();
            }
        }

        private IEnumerator OnJumpParticle()
        {
            _jumpParticle.transform.parent = null;
            _jumpParticle.transform.position = _playerController.transform.position;
            _jumpParticle.transform.localScale = new Vector3(1.5f, 1.5f, 1f);
            _jumpParticle.Play();
            float time = 0f;
            while (time < 0.25f)
            {
                time += Time.deltaTime;
                yield return null;
            }
            _jumpParent.transform.parent = _jumpParent;
            _jumpParticle.Stop();
        }
    }
}
