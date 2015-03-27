using System;
using System.Net.Mail;
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
    public partial class RegistrationForm : Form
    {
        private const string FIRST_NAME_ERROR = "Your first name cannot be left blank.";
        private const string LAST_NAME_ERROR = "Your last name cannot be left blank.";
        private const string PHONE_ERROR = "Your phone number cannot be blank and must be 10 digits long";
        private const string INVALID_EMAIL_ERROR = "The email address you provided is not in the correct format.";
        private const string EMAIL_INUSE_ERROR = "The email address you provided is already in use.";
        private const string PASSWORD_ERROR = "Your password must be at least 6 characters long.";
        private const string CONFIRM_ERROR = "Your confirmed password and password do not match.";

        public RegistrationForm()
        {
            InitializeComponent();
        }

        private void CreateNewUser()
        {
            string firstName = FirstNameTextBox.Text, lastName = LastNameTextBox.Text,
                phoneNumber = PhoneNumberTextBox.Text, emailAddress = EmailAddressTextBox.Text,
                password = ConfirmPasswordTextBox.Text;

            string hashedPassword = MD5Hasher.GetHashedValue(password);
            DatabaseManager.RegisterNewUser(emailAddress, hashedPassword, firstName, lastName, phoneNumber);
        }

        //TODO: show correct error message for email address 
        private void ShowAllErrors()
        {
            if (!FirstNameIsValid) SetErrorForControl(FirstNameTextBox, FIRST_NAME_ERROR);
            if (!LastNameIsValid) SetErrorForControl(LastNameTextBox, LAST_NAME_ERROR);
            if (!PhoneNumberIsValid) SetErrorForControl(PhoneNumberTextBox, PHONE_ERROR);
            if (!EmailIsValid) SetErrorForControl(EmailAddressTextBox, INVALID_EMAIL_ERROR);
            if (!PasswordIsValid) SetErrorForControl(PasswordTextBox, PASSWORD_ERROR);
            if (!ConfirmedPasswordIsValid) SetErrorForControl(ConfirmPasswordTextBox, CONFIRM_ERROR);
        }

        private bool ValidateForm()
        {
            if (FirstNameIsValid && LastNameIsValid && PhoneNumberIsValid
                && EmailIsValid && PasswordIsValid && ConfirmedPasswordIsValid)
            {
                return true;
            }

            ShowAllErrors();
            MessageBox.Show("Registration was not submitted. Errors exist on the page.",
                "Oops! There were some errors!");
            return false;
        }

        private bool EmailIsValid = false;
        private bool ValidateEmail(ref bool isInUse)
        {
            string email = EmailAddressTextBox.Text.ToLower();
            if (email.Contains("..") || email.Contains(".@"))
                return false;

            try
            {
                var address = new MailAddress(email);
                isInUse = (DatabaseManager.IsEmailInUse(email)) ? true : false;
                EmailIsValid = !isInUse;
            }
            catch
            {
                EmailIsValid = false;
            }

            return EmailIsValid;
        }

        private bool PasswordIsValid = false;
        private bool ValidatePassword()
        {
            PasswordIsValid = false;
            string password = PasswordTextBox.Text;
            if (password.Length >= 6)
            {
                PasswordIsValid = true;
            }

            return PasswordIsValid;
        }

        private bool ConfirmedPasswordIsValid = false;
        private bool ValidateConfirmedPassword()
        {
            ConfirmedPasswordIsValid = false;
            string password = PasswordTextBox.Text, confirmed = ConfirmPasswordTextBox.Text;
            if (confirmed == password)
            {
                ConfirmedPasswordIsValid = true;
            }

            return ConfirmedPasswordIsValid;
        }

        private bool FirstNameIsValid = false;
        private bool ValidateFirstName()
        {
            FirstNameIsValid = false;
            string firstName = FirstNameTextBox.Text;
            if (!string.IsNullOrWhiteSpace(firstName))
            {
                FirstNameIsValid = true;
            }

            return FirstNameIsValid;
        }

        private bool LastNameIsValid = false;
        private bool ValidateLastName()
        {
            LastNameIsValid = false;
            string lastName = LastNameTextBox.Text;
            if (!string.IsNullOrWhiteSpace(lastName))
            {
                LastNameIsValid = true;
            }

            return LastNameIsValid;
        }

        private bool PhoneNumberIsValid = false;
        private bool ValidatePhoneNumber()
        {
            PhoneNumberIsValid = false;
            string phoneNumber = PhoneNumberTextBox.Text;
            if (!string.IsNullOrWhiteSpace(phoneNumber) &&
                phoneNumber.Length == 10)
            {
                PhoneNumberIsValid = true;
            }

            return PhoneNumberIsValid;
        }

        private string FormatPhoneNumber(Int64 number)
        {
            string formattedNumber = String.Format("{0:(###) ###-####}", number);

            return formattedNumber;
        }

        private string FormatName(string name)
        {
            name = name.Replace(name[0], char.ToUpper(name[0]));
            return name;
        }

        private void ProcessRegistration()
        {
            if (ValidateForm())
            {
                CreateNewUser();
                MessageBox.Show("Volunteer account has been created." +
                    "\nClick OK to go back to the login screen.", "Registration Complete!");
                this.Close();
            }
        }

        private void SubmitInformationButton_Click(object sender, EventArgs e)
        {
            ProcessRegistration();
        }

        private void CloseRegistrationForm()
        {
            this.Close();
        }

        private void CancelRegistrationButton_Click(object sender, EventArgs e)
        {
            CloseRegistrationForm();
        }

        private void LimitInputForName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar))
                e.Handled = true;
        }

        private void LimitInputForNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }

        private void ClearInput_EnterFocus(object sender, EventArgs e)
        {
            var textbox = sender as TextBox;
            if (textbox == PasswordTextBox)
            {
                ConfirmPasswordTextBox.Clear();
                SetErrorForControl(ConfirmPasswordTextBox);
            }

            textbox.Clear();
            SetErrorForControl(textbox);
        }

        private void SetErrorForControl(Control control, string error = "")
        {
            MainErrorProvider.SetError(control, error);
            MainErrorProvider.SetIconPadding(control, 10);
        }

        private void ValidateFirstName_Validating(object sender, CancelEventArgs e)
        {
            if (!ValidateFirstName())
                SetErrorForControl(FirstNameTextBox, FIRST_NAME_ERROR);
            else
            {
                SetErrorForControl(FirstNameTextBox);
                FirstNameTextBox.Text = FormatName(FirstNameTextBox.Text);
            }
        }

        private void ValidateLastName_Validating(object sender, CancelEventArgs e)
        {
            if (!ValidateLastName())
                SetErrorForControl(LastNameTextBox, LAST_NAME_ERROR);
            else
            {
                SetErrorForControl(LastNameTextBox);
                LastNameTextBox.Text = FormatName(LastNameTextBox.Text);
            }
        }

        private void ValidatePhoneNumber_Validating(object sender, CancelEventArgs e)
        {
            if (!ValidatePhoneNumber())
                SetErrorForControl(PhoneNumberTextBox, PHONE_ERROR);
            else
            {
                MainErrorProvider.SetError(PhoneNumberTextBox, "");
                PhoneNumberTextBox.Text = FormatPhoneNumber(Int64.Parse(PhoneNumberTextBox.Text));
            }
        }

        private void ValidateEmailAddress_Validating(object sender, CancelEventArgs e)
        {
            bool isInUse = false;
            if (!ValidateEmail(ref isInUse))
            {
                if (isInUse)
                    SetErrorForControl(EmailAddressTextBox, EMAIL_INUSE_ERROR);
                else
                    SetErrorForControl(EmailAddressTextBox, INVALID_EMAIL_ERROR);
            }
            else
            {
                SetErrorForControl(EmailAddressTextBox);
            }
        }

        private void ValidatePassword_Validating(object sender, CancelEventArgs e)
        {
            if (!ValidatePassword())
                SetErrorForControl(PasswordTextBox, PASSWORD_ERROR);
            else
                SetErrorForControl(PasswordTextBox);
        }

        private void ClearPhoneNumber_EnterFocus(object sender, EventArgs e)
        {
            if (PhoneNumberTextBox.Text.Length > 10)
                PhoneNumberTextBox.Clear();
        }

        private void RegistrationForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
                CloseRegistrationForm();
            else if (e.KeyData == Keys.Enter || e.KeyData == Keys.Return)
                ProcessRegistration();
        }

        private void ConfirmPasswordTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (!ValidateConfirmedPassword() && PasswordIsValid)
                SetErrorForControl(ConfirmPasswordTextBox, CONFIRM_ERROR);
            else
                SetErrorForControl(ConfirmPasswordTextBox);
        }

        private void RegistrationForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to cancel your registration?",
                "Cancel Registration", MessageBoxButtons.YesNo) == DialogResult.No)
                e.Cancel = true;
        }
    }
}
