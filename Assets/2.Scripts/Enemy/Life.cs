using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class Life : MonoBehaviour, IDamage
    {
        [SerializeField]
        ParticleSystem _slashedParticle;

        private void Start()
        {
           // _slashedParticle.Stop();
        }

        public bool GetDamage()
        {
            //_slashedParticle.Play();
            this.gameObject.SetActive(false);
            return true;
        }
    }
}