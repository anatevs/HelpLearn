using Gameplay;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class InventoriesView : MonoBehaviour
    {
        [SerializeField]
        private InventoryStat _viewPrefab;

        private Dictionary<ItemType, InventoryStat> _views = new();

        public void AddNewView(ItemType type, string value)
        {
            if (_views.ContainsKey(type))
            {
                Debug.LogError($"there is also inventory view with type {type}");
                return;
            }

            var view = Instantiate(_viewPrefab, transform);

            SetTitle(view, type);
            view.SetValue(value);

            _views.Add(type, view);
        }

        public void SetValue(ItemType type, string newValue)
        {
            if (!_views.ContainsKey(type))
            {
                Debug.LogError($"no inventory view for {type}");
                return;
            }

            _views[type].SetValue(newValue);
        }

        private void SetTitle(InventoryStat view, ItemType type)
        {
            view.SetTitle($"{type}:");
        }
    }
}