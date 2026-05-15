using UnityEngine;

namespace Gameplay
{
    public class GameModifierConfig : ScriptableObject
    {
        public virtual string Name => _name;

        [SerializeField]
        protected string _name;
    }
}