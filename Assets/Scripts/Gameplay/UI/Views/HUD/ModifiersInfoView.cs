using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace UI
{
    public class ModifiersInfoView : MonoBehaviour,
        IModifiersInfoView
    {
        [SerializeField]
        private TMP_Text _textViewPrefab;

        private readonly List<TMP_Text> _currentModifiers = new();

        public void AddModifierName(string modifierName)
        {
            var modifier = Instantiate(_textViewPrefab, transform);
            modifier.text = modifierName;
        }

        public void RemoveModifierName(string modifierName)
        {
            for (int i = _currentModifiers.Count; i >= 0; i--)
            {
                if (_currentModifiers[i].text == modifierName)
                {
                    Destroy(_currentModifiers[i]);
                    _currentModifiers.RemoveAt(i);
                    break;
                }
            }
        }
    }
}