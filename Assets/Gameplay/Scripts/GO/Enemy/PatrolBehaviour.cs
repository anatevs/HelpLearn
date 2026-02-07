using System.Collections;
using UnityEngine;

namespace Gameplay
{
    public class PatrolBehaviour : EnemyBehaviour
    {
        public PatrolBehaviour(Enemy enemy) : base(enemy)
        {
        }

        public override void ActUpdate()
        {
            Debug.Log("patrol");
        }
    }
}