using EventBusNamespace;
using UI;

namespace Gameplay
{
    public sealed class ScoreController : CounterController<ScoreChangedEvent>
    {
        public ScoreController(ScoreStorage storage, CounterView counterView, EventBus eventBus) :
            base(storage, counterView, eventBus)
        {
        }

        protected override void HandleEvent(ScoreChangedEvent e)
        {
            _storage.ChangeValue(e.Value);

            SetToView();
        }
    }
}