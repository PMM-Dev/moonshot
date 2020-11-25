using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class Life : MonoBehaviour, IDamage
    {
        public bool GetDamage()
        {
            this.gameObject.SetActive(false);
            return true;
        }
    }
}