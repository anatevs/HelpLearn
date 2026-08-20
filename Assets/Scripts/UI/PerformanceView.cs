using TMPro;
using UnityEngine;

namespace UI
{
    public class PerformanceView : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _fpsText;

        [SerializeField]
        private TMP_Text _frameDurationText;

        [SerializeField]
        private TMP_Text _pickObjectsCountText;

        public void SetFPS(string fps)
        {
            _fpsText.text = fps;
        }

        public void SetFrameDuration(string frameDuration)
        {
            _frameDurationText.text = frameDuration;
        }

        public void SetPeakCount(string pickCount)
        {
            _pickObjectsCountText.text = pickCount;
        }
    }
}