using TMPro;
using UnityEngine;

namespace UI
{
    public class SpawnInfoView : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _objectsName;

        [SerializeField]
        private TMP_Text _totalValueText;

        [SerializeField]
        private TMP_Text _currentValueText;

        public void SetObjectsName(string objectsName)
        {
            _objectsName.text = objectsName;
        }

        public void SetTotalAndCurrent(string totalValue, string currentValue)
        {
            _totalValueText.text = totalValue;
            _currentValueText.text = currentValue;
        }

        public void SetTotal(string value)
        {
            _totalValueText.text = value;
        }

        public void SetCurrent(string value)
        {
            _currentValueText.text = value;
        }
    }
}