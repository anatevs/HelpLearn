using System;

namespace Gameplay
{
    public interface IPool<T>
    {
        public event Action<T> OnNewInstantiated;

        public T Get();

        public void Release(T item);
    }
}