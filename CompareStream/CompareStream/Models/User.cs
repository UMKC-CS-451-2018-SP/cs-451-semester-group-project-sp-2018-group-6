using System;
namespace CompareStream.Models
{
    public class User
    {
        private string email;
        private int _id;

        public Show(int id, string email)
        {
            ID = id;
            Email = email;
        }

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }
    }
}
