using System;

namespace Gameplay
{
    public sealed class CollectItemsBinder : 
        IDisposable
    {
        private readonly IItemsSceneService _itemsService;

        private readonly ICollectService _collectService;

        public CollectItemsBinder(IItemsSceneService itemsService,
            ICollectService collectService)
        {
            _itemsService = itemsService;
            _collectService = collectService;

            _itemsService.OnCollected += _collectService.AddItem;
        }

        void IDisposable.Dispose()
        {
            _itemsService.OnCollected -= _collectService.AddItem;
        }
    }
}