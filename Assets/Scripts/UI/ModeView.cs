using TMPro;
using UnityEngine;

namespace UI
{
    public class ModeView : MonoBehaviour
    {
        public PrewarmView PrewarmView => _prewarmView;

        [SerializeField]
        private TMP_Text _modeNameText;

        [SerializeField]
        private PrewarmView _prewarmView;

        public void SetModeName(string modeName)
        {
            _modeNameText.text = modeName;
        }
    }
}