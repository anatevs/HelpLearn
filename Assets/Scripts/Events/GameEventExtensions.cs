namespace Events
{
    public static class GameEventExtensions
    {
        public static string FormatInfoString(this GameEvent gameEvent, string hexColor, string prefix = "")
        {
            return $"<color={hexColor}>{prefix}{gameEvent.Type} at {gameEvent.Time.ToString("HH:mm:ss")}: {gameEvent.Description}";
        }
    }
}