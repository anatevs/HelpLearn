using TMPro;
using UnityEngine;

namespace UI
{
    public class HealthView : MonoBehaviour,
        IHealthView
    {
        [SerializeField]
        private TMP_Text _hpValue;

        public void SetHP(string hp)
        {
            _hpValue.text = hp;
        }
    }
}