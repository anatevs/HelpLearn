using System.Collections;
using UnityEngine;
using TMPro;
using System;

namespace UI
{
    public class LastAmountMenu : MonoBehaviour
    {
        public event Action<int> OnAmountSet;

        [SerializeField]
        private TMP_InputField _inputField;

        private void OnEnable()
        {
            _inputField.onEndEdit.AddListener(EndEnter);
        }

        private void OnDisable()
        {
            _inputField.onEndEdit.RemoveListener(EndEnter);
        }

        private void EndEnter(string text)
        {
            if (!int.TryParse(text, out var amount))
            {
                return;
            }

            OnAmountSet?.Invoke(amount);

            gameObject.SetActive(false);
        }
    }
}