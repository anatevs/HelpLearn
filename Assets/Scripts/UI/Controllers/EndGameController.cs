namespace UI
{
    public sealed class EndGameController
    {
        private readonly EndGameView _view;

        public EndGameController(EndGameView view)
        {
            _view = view;
        }

        public void ShowLose()
        {
            _view.Show(true);
        }

        public void ShowWin()
        {
            _view.Show(false);
        }

        public void Hide()
        {
            _view.Hide();
        }
    }
}