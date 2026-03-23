using System;

namespace Gameplay
{
    public class Storage
    {
        public event Action<int> OnValueChanged;

        private int _value;

        protected void ChangeValue(int deltaValue)
        {
            if (deltaValue < 0)
            {
                return;
            }

            _value += deltaValue;

            OnValueChanged?.Invoke(_value);
        }

        public void Init(int value)
        {
            _value = value;

            OnValueChanged?.Invoke(value);
        }
    }
}