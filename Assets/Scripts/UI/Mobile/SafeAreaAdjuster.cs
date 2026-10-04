using UnityEngine;

namespace UI.Mobile
{
    public class SafeAreaAdjuster
    {
        private readonly RectTransform _safeAreaPanel;

        public SafeAreaAdjuster(RectTransform safeAreaPanel)
        {
            _safeAreaPanel = safeAreaPanel;
        }

        public void AdjustSafeArea()
        {
            Rect safeArea = Screen.safeArea;

            Vector2 anchorMin = safeArea.position;
            Vector2 anchorMax = safeArea.position + safeArea.size;
            anchorMin.x /= Screen.width;
            anchorMin.y /= Screen.height;
            anchorMax.x /= Screen.width;
            anchorMax.y /= Screen.height;

            _safeAreaPanel.anchorMin = anchorMin;
            _safeAreaPanel.anchorMax = anchorMax;
        }
    }
}