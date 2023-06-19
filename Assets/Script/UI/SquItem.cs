public class SquItem
{
    public int Row;
    public int Column;
    public int Value;
    public bool Blank;
}


namespace Event
{
    public class EventUI
    {
        public struct EventClickClear
        {
            public int Number;
        }

        public struct EventInputNumCheck
        {
            public int Number;
        }

        public struct EventHint
        {
            public int Row;
            public int Column;
        }
    }
}