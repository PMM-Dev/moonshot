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
            
        }

        public bool GetDamage()
        {
            Instantiate(_slashedParticle, this.transform.position, this.transform.rotation).gameObject.SetActive(true);
            this.gameObject.SetActive(false);
            return true;
        }
    }
}