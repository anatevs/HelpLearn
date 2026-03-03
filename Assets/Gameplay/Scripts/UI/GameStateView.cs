using EventBusNamespace;
using TMPro;
using UnityEngine;

namespace UI
{
    public class GameStateView : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _text;

        private void OnEnable()
        {
            EventBus.Subscribe<ChangeGameStateEvent>(ShowState);
        }

        private void OnDisable()
        {
            EventBus.Unsubscribe<ChangeGameStateEvent>(ShowState);
        }

        private void ShowState(ChangeGameStateEvent e)
        {
            SetText(e.Value.ToString());
        }

        private void SetText(string text)
        {
            _text.text = text;
        }
    }
}