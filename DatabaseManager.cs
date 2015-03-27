using System;
using System.Data;
using System.Data.SQLite;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT_3883_Volunteer_App
{
    static public class DatabaseManager
    {
        private const string TRUE = "1", FALSE = "0";
        private const string CONNECTION_STRING = @"Data Source=..\..\Resources\volunteer_db.sqlite;Version=3;";

        /// <summary>
        /// Gets the SQLite Connection for the database manager class methods.
        /// </summary>
        static private SQLiteConnection GetConnection()
        {
            var connection = new SQLiteConnection(CONNECTION_STRING);
            connection.Open();

            return connection;
        }

        /// <summary>
        /// Checks the user's entered data against the user's 
        /// username and password in the database.
        /// </summary>
        /// <param name="username">The users username/email address.</param>
        /// <param name="password">The MD5 hashed value of the users password.</param>
        static public bool IsValidCredentials(string username, string password)
        {
            using (var connection = GetConnection())
            {
                string query = "SELECT COUNT(Users.ID) FROM Users " +
                        "WHERE Users.Username = @username " +
                        "AND Users.Password =  @password";

                var cmd = connection.CreateCommand();
                cmd.CommandText = query;
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);

                return (cmd.ExecuteScalar().ToString() == TRUE);
            }

        }

        /// <summary>
        /// Creates a new ContactInfo entry in the database witht the provided 
        /// phone number and email address, and returns the ID for the row created.
        /// <usage>Call from within RegisterNewUser()</usage>
        /// </summary>
        /// <param name="emailAddress">The user's email address.</param>
        /// <param name="phoneNumber">The user's phone number. Format: (###) ###-####</param>
        static private string GetNewContactInfo(string emailAddress, string phoneNumber)
        {
            using (var connection = GetConnection())
            {
                string query = "INSERT INTO ContactInfo (EmailAddress, PhoneNumber) " +
                    "VALUES (@email, @phone)";
                var cmd = connection.CreateCommand();
                cmd.CommandText = query;
                cmd.Parameters.AddWithValue("@email", emailAddress);
                cmd.Parameters.AddWithValue("@phone", phoneNumber);

                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();

                query = "SELECT ContactInfo.ID FROM ContactInfo WHERE " +
                    "ContactInfo.EmailAddress = @email";
                cmd.CommandText = query;
                cmd.Parameters.AddWithValue("@email", emailAddress);

                return cmd.ExecuteScalar().ToString();
            }
        }

        /// <summary>
        /// Creates a new Users entry in the database and associates
        /// it with the corresponding contact information.
        /// </summary>
        /// <param name="username">The user's email address.</param>
        /// <param name="password">The user's MD5 hashed password.</param>
        /// <param name="firstName">The user's first name.</param>
        /// <param name="lastName">The user's last name.</param>
        /// <param name="phoneNumber">The user's phone number.</param>
        static public void RegisterNewUser(string username, string password, string firstName,
            string lastName, string phoneNumber)
        {
            using (var connection = GetConnection())
            {
                string contactID = GetNewContactInfo(username, phoneNumber);
                string query = "INSERT INTO Users (Username, Password, FirstName, LastName, " +
                    "ContactInfoID, IsAdmin) VALUES (@username, @password, @firstName, @lastName, " +
                    "@contact, @admin)";
                var cmd = connection.CreateCommand();
                cmd.CommandText = query;
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);
                cmd.Parameters.AddWithValue("@firstName", firstName);
                cmd.Parameters.AddWithValue("@lastName", lastName);
                cmd.Parameters.AddWithValue("@contact", contactID);
                cmd.Parameters.AddWithValue("@admin", FALSE);

                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Checks to see if user entered email address doesn't already
        /// exist in the database, i.e. being used by somebody else.
        /// </summary>
        /// <param name="emailAddress">The user's email address.</param>
        static public bool IsEmailInUse(string emailAddress)
        {
            using (var connection = GetConnection())
            {
                string query = "SELECT COUNT(Users.Username) FROM Users " +
                    "WHERE Users.Username = @email";
                var cmd = connection.CreateCommand();
                cmd.CommandText = query;
                cmd.Parameters.AddWithValue("@email", emailAddress);

                return (cmd.ExecuteScalar().ToString() == TRUE);
            }
        }

        /// <summary>
        /// Registers a user for an event.
        /// </summary>
        /// <param name="user_id"></param>
        /// <param name="event_id"></param>
        static public void RegisterUserForEvent(int user_id, int event_id)
        {
            using (var connection = GetConnection())
            {
                string query = "INSERT INTO Registered_Events(EventID,UserID) VALUES(@eId,@uId)";
                var cmd = connection.CreateCommand();
                cmd.CommandText = query;
                cmd.Parameters.AddWithValue("@uId", user_id);
                cmd.Parameters.AddWithValue("@eId", event_id);

                cmd.ExecuteNonQuery();
            }
        }

        static public User GetUserInformation(string username)
        {
            var table = new DataTable();
            using (var connection = GetConnection())
            {
                string query = "SELECT Users.ID, Users.Username, Users.FirstName, " +
                    "Users.LastName, ContactInfo.PhoneNumber, Users.IsAdmin FROM Users JOIN " +
                    "ContactInfo ON Users.ContactInfoID = ContactInfo.ID WHERE " +
                    "Users.Username = @username";
                var cmd = connection.CreateCommand();
                cmd.CommandText = query;
                cmd.Parameters.AddWithValue("@username", username);

                var da = new SQLiteDataAdapter(cmd);
                da.Fill(table);
            }

            var userInfo = new User(
                Int32.Parse(table.Rows[0][0].ToString()),
                table.Rows[0][1].ToString(),
                table.Rows[0][2].ToString(),
                table.Rows[0][3].ToString(),
                table.Rows[0][4].ToString(),
                (table.Rows[0][5].ToString() == TRUE) ? true : false);

            return userInfo;
        }

        static public User GetUserInformation(int contactId)
        {
            var table = new DataTable();
            using (var connection = GetConnection())
            {
                string query = "SELECT Users.ID, Users.Username, Users.FirstName, " +
                    "Users.LastName, ContactInfo.PhoneNumber, Users.IsAdmin FROM Users JOIN " +
                    "ContactInfo ON Users.ContactInfoID = ContactInfo.ID WHERE " +
                    "ContactInfo.ID = @contactId";
                var cmd = connection.CreateCommand();
                cmd.CommandText = query;
                cmd.Parameters.AddWithValue("@contactId", contactId);

                var da = new SQLiteDataAdapter(cmd);
                da.Fill(table);
            }

            var userInfo = new User(
                Int32.Parse(table.Rows[0][0].ToString()),
                table.Rows[0][1].ToString(),
                table.Rows[0][2].ToString(),
                table.Rows[0][3].ToString(),
                table.Rows[0][4].ToString(),
                (table.Rows[0][5].ToString() == TRUE) ? true : false);


            return userInfo;
        }

        static public List<Event> GetAllEvents()
        {
            var table = new DataTable();
            var listOfEvents = new List<Event>();
            using (var connection = GetConnection())
            {
                string query = "SELECT Events.ID, Events.Name, Events.Date, " +
                    "Events.Location, Events.Time, ContactInfo.ID as 'ContactID', " +
                    "Events.Duration FROM Events JOIN ContactInfo ON Events.ContactInfoID = " +
                    "ContactInfo.ID JOIN Users ON ContactInfo.ID = Users.ContactInfoID " +
                    "ORDER BY Events.Date";
                var cmd = connection.CreateCommand();
                cmd.CommandText = query;
                var da = new SQLiteDataAdapter(cmd);
                da.Fill(table);
            }

            foreach (DataRow row in table.Rows)
            {
                listOfEvents.Add(new Event(
                    Int32.Parse(row[0].ToString()),
                    row[1].ToString(),
                    row[2].ToString(),
                    row[4].ToString(),
                    row[3].ToString(),
                    double.Parse(row[6].ToString()),
                    GetUserInformation(Int32.Parse(row[5].ToString()))
                    ));
            }

            return listOfEvents;
        }

        static public List<User> GetAllUsers()
        {
            var table = new DataTable();
            var listOfUsers = new List<User>();
            using (var connection = GetConnection())
            {
                string query = "SELECT Users.ID, Users.Username, Users.FirstName, " +
                    "Users.LastName, ContactInfo.PhoneNumber, Users.Isadmin FROM " +
                    "Users JOIN ContactInfo ON Users.ContactInfoID = ContactInfo.ID";
                var cmd = connection.CreateCommand();
                cmd.CommandText = query;
                var da = new SQLiteDataAdapter(cmd);
                da.Fill(table);
            }

            foreach (DataRow row in table.Rows)
            {
                listOfUsers.Add(new User(
                    Int32.Parse(row[0].ToString()),
                    row[1].ToString(),
                    row[2].ToString(),
                    row[3].ToString(),
                    row[4].ToString(),
                    (row[5].ToString() == TRUE ? true : false)
                    ));
            }

            return listOfUsers;
        }

        static public bool IsUserRegisteredForEvent(int eventId, int userId)
        {
            bool result;
            using (var connection = GetConnection())
            {
                string query = "SELECT COUNT(Events.ID) FROM Events JOIN Registered_Events " +
                    "ON Events.ID = Registered_Events.EventID JOIN Users ON " +
                    "Registered_Events.UserID = Users.ID WHERE Events.ID = " +
                    "@eventId AND Users.ID = @userId";
                var cmd = connection.CreateCommand();
                cmd.CommandText = query;
                cmd.Parameters.AddWithValue("@eventId", eventId);
                cmd.Parameters.AddWithValue("@userId", userId);

                result = (cmd.ExecuteScalar().ToString() == TRUE);
            }

            return result;
        }

        static public bool IsUserAdmin(int id)
        {
            bool result;
            using (var connection = GetConnection())
            {
                string query = "SELECT Users.IsAdmin FROM Users WHERE " +
                    "Users.ID = @id";
                var cmd = connection.CreateCommand();
                cmd.CommandText = query;
                cmd.Parameters.AddWithValue("@id", id);

                result = (cmd.ExecuteScalar().ToString() == TRUE);
            }

            return result;
        }

        static public int GetContactIDForUser(User user)
        {
            int result = -1;
            using (var connection = GetConnection())
            {
                string query = "SELECT Users.ContactInfoID FROM Users WHERE Users.ID = @userId";
                var cmd = connection.CreateCommand();
                cmd.CommandText = query;
                cmd.Parameters.AddWithValue("@userId", user.Id);

                result = Int32.Parse(cmd.ExecuteScalar().ToString());
            }

            return result;
        }

        static public int GetRegisteredCountForUser(int userId)
        {
            int result = -1;
            using(var connection = GetConnection())
            {
                string query = "SELECT COUNT(Registered_Events.EventID) FROM " +
                    "Registered_Events JOIN Users ON Registered_Events.UserID = " +
                    "Users.ID WHERE Users.ID = @userId";
                var cmd = connection.CreateCommand();
                cmd.CommandText = query;
                cmd.Parameters.AddWithValue("@userId", userId);

                result = Int32.Parse(cmd.ExecuteScalar().ToString());
            }

            return result;
        }
    }
}
