using Task1_Items.Items;

namespace Task1_Items.Commands
{
    public abstract class SelectItemCommand<T> : GameCommand,
        IDisposable
        where T : IItem
    {
        protected readonly Player _player;

        protected readonly CommandsController _commandsController = new();

        protected readonly ItemsStorage<T> _storage;

        public SelectItemCommand(Player player, ItemsStorage<T> storage)
        {
            _player = player;

            _storage = storage;

            FillOptionsList();

            _storage.OnItemAdded += AddItem;
            _storage.OnItemRemoved += RemoveItem;
        }

        void IDisposable.Dispose()
        {
            _storage.OnItemAdded -= AddItem;
            _storage.OnItemRemoved -= RemoveItem;
        }

        public override void Execute()
        {
            _commandsController.ShowOptions();

            var pressKey = Console.ReadLine();

            if (pressKey == null || !_commandsController.InvokeOption(pressKey))
            {
                Console.WriteLine("Wrong option number entered");
            }
        }

        public void AddItem(int number, T item)
        {
            AddToOption(number, item);
        }

        public void RemoveItem()
        {
            FillOptionsList();
        }

        protected void AddToOption(int number, T item)
        {
            var takeCommand = CreateCommand(item);
            _commandsController.AddOption(number.ToString(), takeCommand);
        }

        protected abstract GameCommand CreateCommand(T item);

        private void FillOptionsList()
        {
            for (int i = 0; i < _storage.Items.Count; i++)
            {
                AddToOption(i + 1, _storage.Items[i]);
            }
        }
    }
}