namespace Events
{
    public static class GameEventExtentions
    {
        public static string FormatInfoString(this GameEvent gameEvent, string hexColor)
        {
            return $"<color={hexColor}>{gameEvent.Type} at {gameEvent.Time.ToString("HH:mm:ss")}: {gameEvent.Description}";
        }
    }
}