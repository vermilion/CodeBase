using System;

namespace Model
{
    public class Entry
    {
        public string Category;
        public string Code;
        public string DateChanged;
        public string Description;
        public Int64 ID;
        public string Name;
        public string Root;

        public Entry()
        {
            ID = -1;
            Root = "C#";
        }
    }
}