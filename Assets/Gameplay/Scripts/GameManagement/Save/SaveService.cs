namespace GameManagement
{
    public class SaveService : DDOLClass<SaveService>
    {
        private readonly HistorySaver _historySaver = new();

        private void Awake()
        {
            _historySaver.Init();
        }

        private void OnEnable()
        {
            _historySaver.OnEnable();
        }

        private void OnDisable()
        {
            _historySaver.OnDisable();
        }
    }
}