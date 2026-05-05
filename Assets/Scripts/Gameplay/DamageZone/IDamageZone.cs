using UnityEngine;

namespace Gameplay
{
    public interface IDamageZone
    {
        public int Damage {  get; }
        public void CheckAndMakeDamage(GameObject checkGO);
    }
}