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
        private Tuple<string, string> _FullName { set; get; }
        private List<Event> _CreatedEvents { get; set; }
        private List<Event> _RegisteredEvents { get; set; }

        public User(int id, string username, string firstName, string lastName,
            string phoneNumber, bool isAdmin)
        {
            this._Id = id;
            this._Username = username;
            this._PhoneNumber = phoneNumber;
            this._IsAdmin = isAdmin;
            this._FullName = new Tuple<string, string>(firstName, lastName);
            this._CreatedEvents = new List<Event>();
            this._RegisteredEvents = new List<Event>();
        }

        public List<Event> RegisteredEvents
        {
            get { return this._RegisteredEvents; }
            set { this._RegisteredEvents = value; }
        }

        public List<Event> CreatedEvents
        {
            get { return this._CreatedEvents; }
            set { this._CreatedEvents = value; }
        }

        public int Id
        {
            get { return this._Id; }
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

        public Tuple<string, string> FullName
        {
            get { return this._FullName; }
        }
    }
}
