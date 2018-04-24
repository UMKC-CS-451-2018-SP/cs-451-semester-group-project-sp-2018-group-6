using System;
namespace CompareStream.Models
{
    public class Report
    {
        private string _description;
        private int _id, _uid;
        bool _fixed;

        public Report(int id, int userID, string description, bool isFixed)
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

        public int UID
        {
            get { return _uid; }
            set { _uid = value; }
        }

        public bool Fixed
        {
            get { return _fixed; }
            set { _fixed = value; }
        }
    }
}
