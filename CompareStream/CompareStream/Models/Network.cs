using System;
namespace CompareStream.Models
{
    public class Network
    {
        private string _name;
        private int _id;
        private bool _containsShow;

        public Network(int id, string name, bool containsShow)
        {
            ID = id;
            Name = name;
            ContainsShow = containsShow;
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

        public bool ContainsShow
        {
            get { return _containsShow; }
            set { _containsShow = value; }
        }
    }
}
