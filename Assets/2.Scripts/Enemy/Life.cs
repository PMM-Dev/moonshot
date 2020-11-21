using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class Life : MonoBehaviour, IDamage
    {
        public void GetDamage()
        {
            Destroy(this.gameObject);
        }
    }
}