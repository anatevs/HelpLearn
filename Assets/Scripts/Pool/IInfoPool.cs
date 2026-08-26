using System;

namespace Gameplay
{
    public interface IInfoPool
    {
        public event Action<int> OnPoolSizeChanged;
        public event Action<int> OnCurrentFreeChanged;
        public event Action<int> OnRepeatUsingChanged;

        public (int Size, int Free, int Repeat) GetInfo();
    }
}