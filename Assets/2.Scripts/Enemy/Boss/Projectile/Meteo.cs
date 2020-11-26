using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class Meteo : Projectile
    {
        private void Update()
        {
            ChiledRotate();
            Run();
        }

        override protected void Pattern()
        {
            this.gameObject.SetActive(false);
        }
    }
}