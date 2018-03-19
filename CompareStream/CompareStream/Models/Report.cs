using System;
namespace CompareStream.Models
{
    public class Show
    {
        private string _description;
        private int _id;

        public Show(int id, string description)
        {
            ID = id;
            Description = description;
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }
    }
}
