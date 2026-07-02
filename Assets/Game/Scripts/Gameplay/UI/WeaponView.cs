using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class WeaponView : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _chargeValue;

        [SerializeField]
        private GameObject _chargeInfo;

        [SerializeField]
        private Slider _reloadSlider;

        public void ShowCharge(bool isShow)
        {
            _chargeInfo.SetActive(isShow);
            _reloadSlider.gameObject.SetActive(!isShow);
        }

        public void SetCharge(string value)
        {
            _chargeValue.text = value;
        }

        public void SetReloadProgress(float value)
        {
            _reloadSlider.value = value;
        }
    }
}