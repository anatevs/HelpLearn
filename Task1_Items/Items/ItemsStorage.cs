namespace Task1_Items.Items
{
    public class ItemsStorage<T> where T : IItem
    {
        public event Action<int, T>? OnItemAdded;
        public event Action? OnItemRemoved;

        public List<T> Items => _items;

        public T? CurrentActive
        {
            get => _currentActive;
            set => _currentActive = value;
        }

        private readonly List<T> _items = new();

        private T? _currentActive;

        public ItemsStorage(List<T> items)
        {
            _items = items;
        }

        public void AddItem(T item)
        {
            _items.Add(item);

            OnItemAdded?.Invoke(_items.Count, item);
        }

        public void RemoveItem(T item)
        {
            if (!_items.Remove(item))
            {
                Console.WriteLine($"Try to remove missing {item.Name} from options");
                return;
            }

            OnItemRemoved?.Invoke();
        }
    }
}