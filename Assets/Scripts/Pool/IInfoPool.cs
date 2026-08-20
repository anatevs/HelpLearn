using System;

namespace Gameplay
{
    public interface IInfoPool
    {
        public event Action<int> OnPoolSizeChanged;
        public event Action<int> OnCurrentFreeChanged;
        public event Action<int> OnRepeatUsingChanged;

        //public void RaisePoolInfoEvents();

        public (int Size, int Free, int Repeat) GetInfo();
    }
}