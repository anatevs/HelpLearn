using Gameplay;
using TMPro;
using UnityEngine;

namespace UI
{
    public sealed class CollectItemView : MonoBehaviour,
        ICollectItemView
    {
        [SerializeField]
        private TMP_Text _name;

        [SerializeField]
        private TMP_Text _amount;

        public void Init(ItemConfig config, int amount)
        {
            _name.text = config.Name;
            SetAmount(amount);
        }

        public void SetAmount(int amount)
        {
            _amount.text = amount.ToString();
        }
    }
}