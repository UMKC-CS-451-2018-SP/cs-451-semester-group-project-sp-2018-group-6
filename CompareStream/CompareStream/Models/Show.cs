using System;
namespace CompareStream.Models
{
    public class Show
    {
        private string _name;

        public Show(string name)
        {
            Name = name;
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
    }
}
