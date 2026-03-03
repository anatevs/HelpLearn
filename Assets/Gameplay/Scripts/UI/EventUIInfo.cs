using EventBusNamespace;

namespace UI
{
    public static class EventUIInfo
    {
        public static string GetEventString(IGameEvent e)
        {
            var result = $"{e.Name}";

            if (!string.IsNullOrEmpty(e.Description))
            {
                result = $"{result}. {e.Description}";
            }

            return result;
        }
    }
}