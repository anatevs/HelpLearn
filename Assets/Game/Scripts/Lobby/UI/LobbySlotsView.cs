using UnityEngine;

namespace Network.UI
{
    public class LobbySlotsView : MonoBehaviour
    {
        [SerializeField]
        private LobbySlotView _viewPrefab;

        private LobbySlotView[] _views;

        public void Init(int slotsNumber)
        {
            _views = new LobbySlotView[slotsNumber];

            for (int i = 0; i < slotsNumber; i++)
            {
                var view = Instantiate(_viewPrefab.gameObject, transform);
                _views[i] = view.GetComponent<LobbySlotView>();
            }
        }

        public void SetSlotEmpty(int index)
        {
            if (index > -1 && index < _views.Length)
            {
                _views[index].SetEmpty();
            }
        }

        public void SetColor(int index, Color color)
        {
            if (index > -1 && index < _views.Length)
            {
                _views[index].SetColor(color);
            }
        }

        public void SetName(int index, string name)
        {
            if (index > -1 && index < _views.Length)
            {
                _views[index].SetName(name);
            }
        }

        public void SetStatus(int index, string status)
        {
            if (index > -1 && index < _views.Length)
            {
                _views[index].SetStaus(status);
            }
        }
    }
}