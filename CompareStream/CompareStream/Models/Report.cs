using System;
namespace CompareStream.Models
{
    public class Show
    {
        private string description;
        private int _id;

        public Show(int id, string description)
        {
            ID = id;
            Description = description;
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }
    }
}
