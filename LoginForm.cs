#define DEBUG   

using System;
using System.Data.SQLite;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IT_3883_Volunteer_App
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
#if DEBUG
            EmailAddressTextBox.Text = "csmith34@spsu.edu";
            PasswordTextBox.Text = "Chaeson1";
#endif
        }

        private bool UserLoggedIn(string username, string password)
        {
            if (DatabaseManager.IsValidCredentials(username, MD5Hasher.GetHashedValue(password)))
                return true;

            return false;
        }

        private bool ValidateForm(string username, string password)
        {
            if (!String.IsNullOrWhiteSpace(username) && !String.IsNullOrWhiteSpace(password))
                return true;

            return false;
        }

        private void BeginUserLogin()
        {
            string username = EmailAddressTextBox.Text.ToLower();
            string password = PasswordTextBox.Text;

            try
            {
                if (ValidateForm(username, password))
                {
                    if (UserLoggedIn(username, password))
                    {
                        var user = DatabaseManager.GetUserInformation(username);
                        var mainForm = new MainVolunteerForm(user, this);
                        ErrorMessageLabel.Visible = false;

                        this.Hide();
                        mainForm.ShowDialog();
                    }
                    else
                    {
                        throw new Exception("The email and password does not match.");
                    }
                }
                else
                {
                    throw new Exception("The email and password cannot be blank.");
                }
            }
            catch (Exception ex)
            {
                ErrorMessageLabel.Text = ex.Message;
                ErrorMessageLabel.Visible = true;
            }

            EmailAddressTextBox.Clear();
            PasswordTextBox.Clear();
            EmailAddressTextBox.Focus();
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            BeginUserLogin();
        }

        private void ShowNewUserForm()
        {
            var createUser = new RegistrationForm();
            createUser.ShowDialog();
        }

        private void CreateUserButton_Click(object sender, EventArgs e)
        {
            ShowNewUserForm();
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            CloseApplication();
        }

        private void CloseApplication()
        {
            this.Close();
        }

        private void ExitMenuItem_Click(object sender, EventArgs e)
        {
            CloseApplication();
        }

        private void SignUpMenuItem_Click(object sender, EventArgs e)
        {
            ShowNewUserForm();
        }

        private void AboutMenuItem_Click(object sender, EventArgs e)
        {
            var about = new AboutBox();
            about.ShowDialog();
        }

        private void LoginForm_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyData == Keys.Enter | e.KeyData == Keys.Return)
            {
                BeginUserLogin();
                e.Handled = true;
            }
        }
    }
}
