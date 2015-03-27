using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT_3883_Volunteer_App
{
    public class User
    {
        private int _Id { set; get; }
        private string _Username { set; get; }
        private string _PhoneNumber { set; get; }
        private bool _IsAdmin { get; set; }
        private Tuple<string,string> _FullName { set; get; }

        public User(int id, string username, string firstName, string lastName, 
            string phoneNumber, bool isAdmin)
        {
            this.Id = id;
            this._Username = username;
            this._PhoneNumber = phoneNumber;
            this._IsAdmin = isAdmin;
            this._FullName = new Tuple<string, string>(firstName, lastName);
        }

        public int Id
        {
            get { return this._Id; }
            protected set
            {
                this._Id = value;
            }
        }

        public bool IsAdmin
        {
            get { return this._IsAdmin; }
        }

        public string Username
        {
            get { return this._Username; }
        }

        public string PhoneNumber
        {
            get { return this._PhoneNumber; }
        }

        public Tuple<string,string> FullName
        {
            get { return this._FullName; }
        }
    }
}
