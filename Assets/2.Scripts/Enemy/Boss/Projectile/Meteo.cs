using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class Meteo : Projectile
    {
        private void Update()
        {
            Rotate();
            Run();
        }

        override protected void Pattern()
        {
            Destroy(this.gameObject);
        }
    }
}