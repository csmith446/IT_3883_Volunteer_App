using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT_3883_Volunteer_App
{
    public class Event
    {
        private int _Id { set; get; }
        private double _Duration { get; set; }
        private string _Name { set; get; }
        private string _Date { set; get; }
        private DateTime _StartTime { set; get; }
        private DateTime _EndTime { set; get; }
        private string _Location { set; get; }
        private User _Creator { set; get; }
        private List<User> _RegisteredUsers { get; set; }

        public Event(int id, string name, string date,
            string time, string location, double duration, User contact)
        {
            this._Id = id;
            this._Name = name;
            this._Date = date;
            this._StartTime = DateTime.ParseExact(time, "hh:mm tt", CultureInfo.InvariantCulture);
            this._Duration = duration;
            this._EndTime = this._StartTime.AddHours(duration);
            this._Location = location;
            this._Creator = contact;
            this._RegisteredUsers = new List<User>();
        }

        public List<User> RegisteredUsers
        {
            get { return this._RegisteredUsers; }
            set { this._RegisteredUsers = value; }
        }

        public int Id
        {
            get { return this._Id; }
        }

        public string Name
        {
            get { return this._Name; }
        }

        public string Date
        {
            get { return this._Date; }
        }

        public DateTime StartTime
        {
            get { return this._StartTime; }
        }

        public DateTime EndTime
        {
            get { return this._EndTime; }
        }

        public double Duration
        {
            get { return this._Duration; }
        }

        public string Location
        {
            get { return this._Location; }
        }

        public User Creator
        {
            get { return this._Creator; }
        }
    }
}
