using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using EventType = Events.EventType;

namespace UI
{
    public class SelectEventTypeView : MonoBehaviour
    {
        public event Action<EventType> OnEndClicked;

        [SerializeField]
        private Toggle _togglePrefab;

        [SerializeField]
        private ToggleGroup _group;

        [SerializeField]
        private Button _endButton;

        private EventType _selectedType;

        private Toggle[] _toggles;

        private UnityAction<bool>[] _handlers;

        private readonly string _toggleNameGO = "Label";

        private void Awake()
        {
            EventType[] eventTypes = (EventType[])Enum.GetValues(typeof(EventType));

            _toggles = new Toggle[eventTypes.Length];
            _handlers = new UnityAction<bool>[eventTypes.Length];

            for (int i = 0; i < _toggles.Length; i++)
            {
                Toggle toggle = Instantiate(_togglePrefab, _group.transform);

                if (i == 0) toggle.isOn = true;

                _toggles[i] = toggle;

                toggle.transform
                    .Find(_toggleNameGO)
                    .GetComponent<Text>()
                    .text = eventTypes[i].ToString();

                toggle.group = _group;

                var number = i;
                Action<bool> action = (bool isToggled) => SelectType(number, isToggled);

                var uAction = new UnityAction<bool>(action);

                _handlers[i] = uAction;
            }
        }

        private void OnEnable()
        {
            for (int i = 0; i < _toggles.Length; i++)
            {
                _toggles[i].onValueChanged.AddListener(_handlers[i]);
            }

            _endButton.onClick.AddListener(EndSelection);
        }

        private void OnDisable()
        {
            for (int i = 0; i < _toggles.Length; i++)
            {
                _toggles[i].onValueChanged.RemoveListener(_handlers[i]);
            }

            _endButton.onClick.RemoveListener(EndSelection);
        }

        private void SelectType(int index, bool isToggled)
        {
            if (isToggled)
            {
                _selectedType = (EventType)index;
            }
        }

        private void EndSelection()
        {
            OnEndClicked?.Invoke(_selectedType);

            gameObject.SetActive(false);
        }
    }
}