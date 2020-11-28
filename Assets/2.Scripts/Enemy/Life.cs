using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class Life : MonoBehaviour, IDamage
    {
        [SerializeField]
        GameObject _slashedParticlePrefeb;

        

        public bool GetDamage()
        {
            Instantiate(_slashedParticlePrefeb, this.transform.position, this.transform.rotation);
            this.gameObject.SetActive(false);
            return true;
        }
    }
}