using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class EventsInfoView : MonoBehaviour
    {
        [SerializeField]
        private Button _filterButton;

        [SerializeField]
        private Button _backButton;

        [SerializeField]
        private FilterOprtionsView _filterMenu;

        [SerializeField]
        private EventsLogView _logView;

        private void OnEnable()
        {
            _filterButton.onClick.AddListener(SetFilterMode);
            _backButton.onClick.AddListener(SetLogsMode);
        }

        private void OnDisable()
        {
            _filterButton.onClick.RemoveListener(SetFilterMode);
            _backButton.onClick.RemoveListener(SetLogsMode);
        }

        private void SetFilterMode()
        {
            ChangeMode(true);
        }

        private void SetLogsMode()
        {
            ChangeMode(false);

            _logView.ShowLastEvent();
        }

        private void ChangeMode(bool isFilter)
        {
            _filterMenu.Show(isFilter);
            _logView.IsLogsShow = !isFilter;

            _filterButton.gameObject.SetActive(!isFilter);
            _backButton.gameObject.SetActive(isFilter);
        }

    }
}