namespace Gameplay
{
    public interface IPool<T>
    {
        public T Get();

        public void Release(T item);
    }
}