namespace Gameplay
{
    public class CounterStorage
    {
        public int Value => _countValue;

        protected int _countValue;

        private readonly int _startCount;

        public CounterStorage(int startCount)
        {
            _startCount = startCount;
            _countValue = _startCount;
        }

        public virtual void ChangeValue(int deltaValue)
        {
            _countValue += deltaValue;
        }

        public void ResetValue()
        {
            _countValue = _startCount;
        }
    }
}