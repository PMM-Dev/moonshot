using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class Meteo : Projectile
    {
        private SoundHelper _soundHelper;

        private void Awake()
        {
            _soundHelper = this.gameObject.AddComponent<SoundHelper>();
        }


        private void Update()
        {
            ChiledRotate();
            Run();
        }

        private void OnEnable()
        {
            _soundHelper.PlaySound(false, "Boss_Meteor");
        }

        override protected void Pattern()
        {
            this.gameObject.SetActive(false);
        }
    }
}