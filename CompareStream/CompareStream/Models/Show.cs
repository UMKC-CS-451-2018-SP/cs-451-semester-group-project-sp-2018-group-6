using System;
namespace CompareStream.Models
{
    public class Show
    {
        private string _name;
        private int _id;

        public Show(int id, string name)
        {
            ID = id;
            Name = name;
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }
    }
}
