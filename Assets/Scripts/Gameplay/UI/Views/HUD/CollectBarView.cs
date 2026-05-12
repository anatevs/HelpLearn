using Gameplay;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public sealed class CollectBarView : MonoBehaviour,
        ICollectBarView
    {
        [SerializeField]
        private CollectItemView _viewPrefab;

        private readonly Dictionary<string, CollectItemView> _items = new();

        public void AddView(ItemConfig config, int amount)
        {
            var view = Instantiate(_viewPrefab, transform);

            view.Init(config, amount);

            _items.Add(config.Name, view);
        }

        public void SetAmount(string name, int amount)
        {
            _items[name].SetAmount(amount);
        }

        public void RemoveView(string name)
        {
            Destroy(_items[name].gameObject);

            _items.Remove(name);
        }
    }
}