using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public sealed class WeaponsMenu : MonoBehaviour
    {
        [SerializeField]
        private WeaponView _viewPrefab;

        [SerializeField]
        private ToggleGroup _toggleGroup;

        public WeaponView AddView()
        {
            var view = Instantiate(_viewPrefab);

            view.transform.SetParent(transform);

            view.SetToggleGroup(_toggleGroup);

            return view;
        }
    }
}