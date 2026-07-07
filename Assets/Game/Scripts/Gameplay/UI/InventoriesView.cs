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

        private Dictionary<string, InventoryStat> _viewsNamed = new();

        public void AddNewView(string itemName, string value)
        {
            if (_viewsNamed.ContainsKey(itemName))
            {
                Debug.LogError($"there is also inventory view with type {itemName}");
                return;
            }

            var view = Instantiate(_viewPrefab, transform);

            SetTitle(view, itemName);
            view.SetValue(value);

            _viewsNamed.Add(itemName, view);
        }

        public void SetValue(string itemName, string newValue)
        {
            if (!_viewsNamed.ContainsKey(itemName))
            {
                Debug.LogError($"no inventory view for {itemName}");
                return;
            }

            _viewsNamed[itemName].SetValue(newValue);
        }

        private void SetTitle(InventoryStat view, string itemName)
        {
            view.SetTitle($"{itemName}:");
        }
    }
}