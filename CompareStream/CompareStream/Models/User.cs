using System;
namespace CompareStream.Models
{
    public class User
    {
        private string _email;
        private int _id;

        public User(int id, string email)
        {
            ID = id;
            Email = email;
        }

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }
    }
}
