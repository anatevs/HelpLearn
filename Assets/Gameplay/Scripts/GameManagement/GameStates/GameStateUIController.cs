using EventBusNamespace;
using System;
using UI;

namespace GameManagement
{
    public class GameStateUIController :
        IDisposable
    {
        private readonly EventBus _eventBus;
        private readonly CanvasView _canvasView;

        public GameStateUIController(EventBus eventBus, CanvasView canvasView)
        {
            _eventBus = eventBus;
            _canvasView = canvasView;

            _eventBus.Subscribe<GamePlayingEvent>(ShowPlaying);
            _eventBus.Subscribe<GameWinEvent>(ShowWin);
            _eventBus.Subscribe<GameLoseEvent>(ShowLose);
        }

        void IDisposable.Dispose()
        {
            _eventBus.Unsubscribe<GamePlayingEvent>(ShowPlaying);
            _eventBus.Unsubscribe<GameWinEvent>(ShowWin);
            _eventBus.Unsubscribe<GameLoseEvent>(ShowLose);
        }

        public void ShowPlaying(GamePlayingEvent e)
        {
            _canvasView.ShowPlaying();
        }

        public void ShowWin(GameWinEvent e)
        {
            _canvasView.ShowWin();
        }

        public void ShowLose(GameLoseEvent e)
        {
            _canvasView.ShowLose();
        }
    }
}