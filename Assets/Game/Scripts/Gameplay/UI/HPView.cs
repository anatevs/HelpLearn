using TMPro;
using UnityEngine;

namespace UI
{
    public class HPView : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _hpText;

        public void SetHP(string hpText)
        {
            _hpText.text = hpText;
        }
    }
}