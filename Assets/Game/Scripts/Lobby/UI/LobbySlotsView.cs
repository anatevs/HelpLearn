using System;
using UnityEngine;

namespace Network.UI
{
    public class LobbySlotsView : MonoBehaviour
    {
        public event Action<int> OnRemoveClicked;

        [SerializeField]
        private LobbySlotView _viewPrefab;

        private LobbySlotView[] _views;

        private void OnDisable()
        {
            for (int i = 0; i < _views.Length; i++)
            {
                SetSlotEmpty(i);
                _views[i].UnsubscribeRemoveEvent();
            }
        }

        public void Init(int slotsNumber)
        {
            _views = new LobbySlotView[slotsNumber];

            for (int i = 0; i < slotsNumber; i++)
            {
                var viewGO = Instantiate(_viewPrefab.gameObject, transform);
                var view = viewGO.GetComponent<LobbySlotView>();
                _views[i] = view;

                int index = i;
                view.OnRemoveClicked += () => OnRemoveClicked?.Invoke(index);
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

        public void ShowRemoveButton(int index, bool isShow)
        {
            if (index > -1 && index < _views.Length)
            {
                _views[index].ShowRemoveButton(isShow);
            }
        }
    }
}