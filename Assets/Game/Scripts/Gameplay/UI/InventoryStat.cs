using TMPro;
using UnityEngine;

namespace UI
{
    public class InventoryStat : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _title;

        [SerializeField]
        private TMP_Text _value;

        public void SetTitle(string title)
        {
            _title.text = title;
        }

        public void SetValue(string value)
        {
            _value.text = value;
        }
    }
}