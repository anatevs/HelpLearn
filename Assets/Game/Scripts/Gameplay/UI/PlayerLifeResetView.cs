using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PlayerLifeResetView : MonoBehaviour
    {
        [SerializeField]
        private Slider _lifeResetSlider;

        public void SetProgress(float value)
        {
            _lifeResetSlider.value = value;
        }
    }
}