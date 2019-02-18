namespace WinG
{
    public static class Message
    {
        public static void Beep(MessageType type)
        {
            Core.Core.MessageBeep(type);
        }
    }
}
