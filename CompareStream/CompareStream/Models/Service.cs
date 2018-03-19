using System;
namespace CompareStream.Models
{
    public class Service
    {
        private string _name;
        private int _id;
        private float _price;

        public Service(int id, string name, float price)
        {
            ID = id;
            Name = name;
            Price = price;
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
        
        public float Price
        {
          get { return _price; }
          set { _price = value;}
        }
    }
}
