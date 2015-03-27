namespace IT_3883_Volunteer_App
{
    partial class RegistrationForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lblFirstName = new System.Windows.Forms.Label();
            this.ContactInformationGroupBox = new System.Windows.Forms.GroupBox();
            this.PhoneNumberTextBox = new System.Windows.Forms.TextBox();
            this.LastNameTextBox = new System.Windows.Forms.TextBox();
            this.FirstNameTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.AccountInformationGroupBox = new System.Windows.Forms.GroupBox();
            this.EmailAddressTextBox = new System.Windows.Forms.TextBox();
            this.PasswordTextBox = new System.Windows.Forms.TextBox();
            this.ConfirmPasswordTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.SubmitInformationButton = new System.Windows.Forms.Button();
            this.CancelRegistrationButton = new System.Windows.Forms.Button();
            this.MainErrorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.ContactInformationGroupBox.SuspendLayout();
            this.AccountInformationGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MainErrorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // lblFirstName
            // 
            this.lblFirstName.AutoSize = true;
            this.lblFirstName.Location = new System.Drawing.Point(12, 30);
            this.lblFirstName.Name = "lblFirstName";
            this.lblFirstName.Size = new System.Drawing.Size(63, 13);
            this.lblFirstName.TabIndex = 0;
            this.lblFirstName.Text = "First Name: ";
            // 
            // ContactInformationGroupBox
            // 
            this.ContactInformationGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ContactInformationGroupBox.Controls.Add(this.PhoneNumberTextBox);
            this.ContactInformationGroupBox.Controls.Add(this.LastNameTextBox);
            this.ContactInformationGroupBox.Controls.Add(this.FirstNameTextBox);
            this.ContactInformationGroupBox.Controls.Add(this.label3);
            this.ContactInformationGroupBox.Controls.Add(this.label1);
            this.ContactInformationGroupBox.Controls.Add(this.lblFirstName);
            this.ContactInformationGroupBox.Location = new System.Drawing.Point(12, 12);
            this.ContactInformationGroupBox.Name = "ContactInformationGroupBox";
            this.ContactInformationGroupBox.Size = new System.Drawing.Size(344, 125);
            this.ContactInformationGroupBox.TabIndex = 1;
            this.ContactInformationGroupBox.TabStop = false;
            this.ContactInformationGroupBox.Text = "Contact Information";
            // 
            // PhoneNumberTextBox
            // 
            this.PhoneNumberTextBox.Location = new System.Drawing.Point(130, 87);
            this.PhoneNumberTextBox.MaxLength = 10;
            this.PhoneNumberTextBox.Name = "PhoneNumberTextBox";
            this.PhoneNumberTextBox.Size = new System.Drawing.Size(100, 20);
            this.PhoneNumberTextBox.TabIndex = 2;
            this.PhoneNumberTextBox.Enter += new System.EventHandler(this.ClearPhoneNumber_EnterFocus);
            this.PhoneNumberTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.LimitInputForNumber_KeyPress);
            this.PhoneNumberTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.ValidatePhoneNumber_Validating);
            // 
            // LastNameTextBox
            // 
            this.LastNameTextBox.Location = new System.Drawing.Point(130, 57);
            this.LastNameTextBox.Name = "LastNameTextBox";
            this.LastNameTextBox.Size = new System.Drawing.Size(175, 20);
            this.LastNameTextBox.TabIndex = 1;
            this.LastNameTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.LimitInputForName_KeyPress);
            this.LastNameTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.ValidateLastName_Validating);
            // 
            // FirstNameTextBox
            // 
            this.FirstNameTextBox.Location = new System.Drawing.Point(130, 27);
            this.FirstNameTextBox.Name = "FirstNameTextBox";
            this.FirstNameTextBox.Size = new System.Drawing.Size(175, 20);
            this.FirstNameTextBox.TabIndex = 0;
            this.FirstNameTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.LimitInputForName_KeyPress);
            this.FirstNameTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.ValidateFirstName_Validating);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Phone Number:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Last Name: ";
            // 
            // AccountInformationGroupBox
            // 
            this.AccountInformationGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AccountInformationGroupBox.Controls.Add(this.EmailAddressTextBox);
            this.AccountInformationGroupBox.Controls.Add(this.PasswordTextBox);
            this.AccountInformationGroupBox.Controls.Add(this.ConfirmPasswordTextBox);
            this.AccountInformationGroupBox.Controls.Add(this.label4);
            this.AccountInformationGroupBox.Controls.Add(this.label6);
            this.AccountInformationGroupBox.Controls.Add(this.label7);
            this.AccountInformationGroupBox.Location = new System.Drawing.Point(12, 158);
            this.AccountInformationGroupBox.Name = "AccountInformationGroupBox";
            this.AccountInformationGroupBox.Size = new System.Drawing.Size(344, 127);
            this.AccountInformationGroupBox.TabIndex = 4;
            this.AccountInformationGroupBox.TabStop = false;
            this.AccountInformationGroupBox.Text = "Account Information";
            // 
            // EmailAddressTextBox
            // 
            this.EmailAddressTextBox.Location = new System.Drawing.Point(130, 27);
            this.EmailAddressTextBox.Name = "EmailAddressTextBox";
            this.EmailAddressTextBox.Size = new System.Drawing.Size(175, 20);
            this.EmailAddressTextBox.TabIndex = 3;
            this.EmailAddressTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.ValidateEmailAddress_Validating);
            // 
            // PasswordTextBox
            // 
            this.PasswordTextBox.Location = new System.Drawing.Point(130, 58);
            this.PasswordTextBox.Name = "PasswordTextBox";
            this.PasswordTextBox.PasswordChar = '•';
            this.PasswordTextBox.Size = new System.Drawing.Size(175, 20);
            this.PasswordTextBox.TabIndex = 4;
            this.PasswordTextBox.Enter += new System.EventHandler(this.ClearInput_EnterFocus);
            this.PasswordTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.ValidatePassword_Validating);
            // 
            // ConfirmPasswordTextBox
            // 
            this.ConfirmPasswordTextBox.Location = new System.Drawing.Point(130, 90);
            this.ConfirmPasswordTextBox.Name = "ConfirmPasswordTextBox";
            this.ConfirmPasswordTextBox.PasswordChar = '•';
            this.ConfirmPasswordTextBox.Size = new System.Drawing.Size(175, 20);
            this.ConfirmPasswordTextBox.TabIndex = 5;
            this.ConfirmPasswordTextBox.Enter += new System.EventHandler(this.ClearInput_EnterFocus);
            this.ConfirmPasswordTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.ValidateConfirmedPassword_Validating);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 93);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(94, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Confirm Password:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(11, 61);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 13);
            this.label6.TabIndex = 1;
            this.label6.Text = "Password:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 30);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(76, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "Email Address:";
            // 
            // SubmitInformationButton
            // 
            this.SubmitInformationButton.BackColor = System.Drawing.Color.LightGreen;
            this.SubmitInformationButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SubmitInformationButton.Location = new System.Drawing.Point(210, 307);
            this.SubmitInformationButton.Name = "SubmitInformationButton";
            this.SubmitInformationButton.Size = new System.Drawing.Size(100, 40);
            this.SubmitInformationButton.TabIndex = 6;
            this.SubmitInformationButton.Text = "Submit Information";
            this.SubmitInformationButton.UseVisualStyleBackColor = false;
            this.SubmitInformationButton.Click += new System.EventHandler(this.SubmitInformationButton_Click);
            // 
            // CancelRegistrationButton
            // 
            this.CancelRegistrationButton.BackColor = System.Drawing.Color.LightCoral;
            this.CancelRegistrationButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelRegistrationButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CancelRegistrationButton.Location = new System.Drawing.Point(60, 307);
            this.CancelRegistrationButton.Name = "CancelRegistrationButton";
            this.CancelRegistrationButton.Size = new System.Drawing.Size(100, 40);
            this.CancelRegistrationButton.TabIndex = 7;
            this.CancelRegistrationButton.Text = "Cancel Registration";
            this.CancelRegistrationButton.UseVisualStyleBackColor = false;
            this.CancelRegistrationButton.Click += new System.EventHandler(this.CancelRegistrationButton_Click);
            // 
            // MainErrorProvider
            // 
            this.MainErrorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.MainErrorProvider.ContainerControl = this;
            // 
            // RegistrationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(368, 368);
            this.Controls.Add(this.CancelRegistrationButton);
            this.Controls.Add(this.SubmitInformationButton);
            this.Controls.Add(this.AccountInformationGroupBox);
            this.Controls.Add(this.ContactInformationGroupBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "RegistrationForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Volunteer Registration";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.RegistrationForm_KeyDown);
            this.ContactInformationGroupBox.ResumeLayout(false);
            this.ContactInformationGroupBox.PerformLayout();
            this.AccountInformationGroupBox.ResumeLayout(false);
            this.AccountInformationGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MainErrorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblFirstName;
        private System.Windows.Forms.GroupBox ContactInformationGroupBox;
        private System.Windows.Forms.TextBox PhoneNumberTextBox;
        private System.Windows.Forms.TextBox LastNameTextBox;
        private System.Windows.Forms.TextBox FirstNameTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox AccountInformationGroupBox;
        private System.Windows.Forms.TextBox EmailAddressTextBox;
        private System.Windows.Forms.TextBox PasswordTextBox;
        private System.Windows.Forms.TextBox ConfirmPasswordTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button SubmitInformationButton;
        private System.Windows.Forms.Button CancelRegistrationButton;
        private System.Windows.Forms.ErrorProvider MainErrorProvider;
    }
}