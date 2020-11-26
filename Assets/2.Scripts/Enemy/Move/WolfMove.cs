using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class WolfMove : EnemyMove
    {
        override protected IEnumerator Translate()
        {
                CalculationDistance(_player.transform.position);
                FlipSize();
            yield return StartCoroutine(Translate());
        }
    }
}