namespace IT_3883_Volunteer_App
{
    partial class CreateUserForm
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
            this.UserIsAdminCheckBox = new System.Windows.Forms.CheckBox();
            this.ConfirmPasswordTextBox = new System.Windows.Forms.TextBox();
            this.PasswordTextBox = new System.Windows.Forms.TextBox();
            this.ConfirmPasswordLabel = new System.Windows.Forms.Label();
            this.PasswordLabel = new System.Windows.Forms.Label();
            this.PhoneNumberTextBox = new System.Windows.Forms.TextBox();
            this.EmailAddressTextBox = new System.Windows.Forms.TextBox();
            this.LastNameTextBox = new System.Windows.Forms.TextBox();
            this.FirstNameTextBox = new System.Windows.Forms.TextBox();
            this.CreateUserButton = new System.Windows.Forms.Button();
            this.CancelNewUserButton = new System.Windows.Forms.Button();
            this.PhoneNumberLabel = new System.Windows.Forms.Label();
            this.EmailAddressLabel = new System.Windows.Forms.Label();
            this.LastNameLabel = new System.Windows.Forms.Label();
            this.FirstNameLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // UserIsAdminCheckBox
            // 
            this.UserIsAdminCheckBox.AutoSize = true;
            this.UserIsAdminCheckBox.Location = new System.Drawing.Point(204, 79);
            this.UserIsAdminCheckBox.Name = "UserIsAdminCheckBox";
            this.UserIsAdminCheckBox.Size = new System.Drawing.Size(136, 17);
            this.UserIsAdminCheckBox.TabIndex = 31;
            this.UserIsAdminCheckBox.Text = "User is an Administrator";
            this.UserIsAdminCheckBox.UseVisualStyleBackColor = true;
            // 
            // ConfirmPasswordTextBox
            // 
            this.ConfirmPasswordTextBox.Location = new System.Drawing.Point(101, 147);
            this.ConfirmPasswordTextBox.Name = "ConfirmPasswordTextBox";
            this.ConfirmPasswordTextBox.Size = new System.Drawing.Size(279, 20);
            this.ConfirmPasswordTextBox.TabIndex = 30;
            // 
            // PasswordTextBox
            // 
            this.PasswordTextBox.Location = new System.Drawing.Point(101, 117);
            this.PasswordTextBox.Name = "PasswordTextBox";
            this.PasswordTextBox.Size = new System.Drawing.Size(279, 20);
            this.PasswordTextBox.TabIndex = 29;
            // 
            // ConfirmPasswordLabel
            // 
            this.ConfirmPasswordLabel.AutoSize = true;
            this.ConfirmPasswordLabel.Location = new System.Drawing.Point(47, 150);
            this.ConfirmPasswordLabel.Name = "ConfirmPasswordLabel";
            this.ConfirmPasswordLabel.Size = new System.Drawing.Size(45, 13);
            this.ConfirmPasswordLabel.TabIndex = 28;
            this.ConfirmPasswordLabel.Text = "Confirm:";
            // 
            // PasswordLabel
            // 
            this.PasswordLabel.AutoSize = true;
            this.PasswordLabel.Location = new System.Drawing.Point(36, 120);
            this.PasswordLabel.Name = "PasswordLabel";
            this.PasswordLabel.Size = new System.Drawing.Size(56, 13);
            this.PasswordLabel.TabIndex = 27;
            this.PasswordLabel.Text = "Password:";
            // 
            // PhoneNumberTextBox
            // 
            this.PhoneNumberTextBox.Location = new System.Drawing.Point(101, 77);
            this.PhoneNumberTextBox.MaxLength = 13;
            this.PhoneNumberTextBox.Name = "PhoneNumberTextBox";
            this.PhoneNumberTextBox.Size = new System.Drawing.Size(80, 20);
            this.PhoneNumberTextBox.TabIndex = 25;
            // 
            // EmailAddressTextBox
            // 
            this.EmailAddressTextBox.Location = new System.Drawing.Point(101, 47);
            this.EmailAddressTextBox.Name = "EmailAddressTextBox";
            this.EmailAddressTextBox.Size = new System.Drawing.Size(279, 20);
            this.EmailAddressTextBox.TabIndex = 24;
            // 
            // LastNameTextBox
            // 
            this.LastNameTextBox.Location = new System.Drawing.Point(280, 17);
            this.LastNameTextBox.Name = "LastNameTextBox";
            this.LastNameTextBox.Size = new System.Drawing.Size(100, 20);
            this.LastNameTextBox.TabIndex = 23;
            // 
            // FirstNameTextBox
            // 
            this.FirstNameTextBox.Location = new System.Drawing.Point(101, 17);
            this.FirstNameTextBox.Name = "FirstNameTextBox";
            this.FirstNameTextBox.Size = new System.Drawing.Size(100, 20);
            this.FirstNameTextBox.TabIndex = 22;
            // 
            // CreateUserButton
            // 
            this.CreateUserButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CreateUserButton.BackColor = System.Drawing.Color.PaleGreen;
            this.CreateUserButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CreateUserButton.Location = new System.Drawing.Point(209, 187);
            this.CreateUserButton.Name = "CreateUserButton";
            this.CreateUserButton.Size = new System.Drawing.Size(145, 40);
            this.CreateUserButton.TabIndex = 21;
            this.CreateUserButton.Text = "Create User Account";
            this.CreateUserButton.UseVisualStyleBackColor = false;
            // 
            // CancelNewUserButton
            // 
            this.CancelNewUserButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CancelNewUserButton.BackColor = System.Drawing.Color.Salmon;
            this.CancelNewUserButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CancelNewUserButton.Location = new System.Drawing.Point(65, 187);
            this.CancelNewUserButton.Name = "CancelNewUserButton";
            this.CancelNewUserButton.Size = new System.Drawing.Size(145, 40);
            this.CancelNewUserButton.TabIndex = 20;
            this.CancelNewUserButton.Text = "Cancel";
            this.CancelNewUserButton.UseVisualStyleBackColor = false;
            this.CancelNewUserButton.Click += new System.EventHandler(this.CancelNewUserButton_Click);
            // 
            // PhoneNumberLabel
            // 
            this.PhoneNumberLabel.AutoSize = true;
            this.PhoneNumberLabel.Location = new System.Drawing.Point(14, 80);
            this.PhoneNumberLabel.Name = "PhoneNumberLabel";
            this.PhoneNumberLabel.Size = new System.Drawing.Size(81, 13);
            this.PhoneNumberLabel.TabIndex = 19;
            this.PhoneNumberLabel.Text = "Phone Number:";
            // 
            // EmailAddressLabel
            // 
            this.EmailAddressLabel.AutoSize = true;
            this.EmailAddressLabel.Location = new System.Drawing.Point(19, 50);
            this.EmailAddressLabel.Name = "EmailAddressLabel";
            this.EmailAddressLabel.Size = new System.Drawing.Size(76, 13);
            this.EmailAddressLabel.TabIndex = 18;
            this.EmailAddressLabel.Text = "Email Address:";
            // 
            // LastNameLabel
            // 
            this.LastNameLabel.AutoSize = true;
            this.LastNameLabel.Location = new System.Drawing.Point(213, 20);
            this.LastNameLabel.Name = "LastNameLabel";
            this.LastNameLabel.Size = new System.Drawing.Size(61, 13);
            this.LastNameLabel.TabIndex = 17;
            this.LastNameLabel.Text = "Last Name:";
            // 
            // FirstNameLabel
            // 
            this.FirstNameLabel.AutoSize = true;
            this.FirstNameLabel.Location = new System.Drawing.Point(35, 20);
            this.FirstNameLabel.Name = "FirstNameLabel";
            this.FirstNameLabel.Size = new System.Drawing.Size(60, 13);
            this.FirstNameLabel.TabIndex = 16;
            this.FirstNameLabel.Text = "First Name:";
            // 
            // CreateUserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(414, 244);
            this.Controls.Add(this.UserIsAdminCheckBox);
            this.Controls.Add(this.ConfirmPasswordTextBox);
            this.Controls.Add(this.PasswordTextBox);
            this.Controls.Add(this.ConfirmPasswordLabel);
            this.Controls.Add(this.PasswordLabel);
            this.Controls.Add(this.PhoneNumberTextBox);
            this.Controls.Add(this.EmailAddressTextBox);
            this.Controls.Add(this.LastNameTextBox);
            this.Controls.Add(this.FirstNameTextBox);
            this.Controls.Add(this.CreateUserButton);
            this.Controls.Add(this.CancelNewUserButton);
            this.Controls.Add(this.PhoneNumberLabel);
            this.Controls.Add(this.EmailAddressLabel);
            this.Controls.Add(this.LastNameLabel);
            this.Controls.Add(this.FirstNameLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "CreateUserForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Create a New User Account";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox UserIsAdminCheckBox;
        private System.Windows.Forms.TextBox ConfirmPasswordTextBox;
        private System.Windows.Forms.TextBox PasswordTextBox;
        private System.Windows.Forms.Label ConfirmPasswordLabel;
        private System.Windows.Forms.Label PasswordLabel;
        private System.Windows.Forms.TextBox PhoneNumberTextBox;
        private System.Windows.Forms.TextBox EmailAddressTextBox;
        private System.Windows.Forms.TextBox LastNameTextBox;
        private System.Windows.Forms.TextBox FirstNameTextBox;
        private System.Windows.Forms.Button CreateUserButton;
        private System.Windows.Forms.Button CancelNewUserButton;
        private System.Windows.Forms.Label PhoneNumberLabel;
        private System.Windows.Forms.Label EmailAddressLabel;
        private System.Windows.Forms.Label LastNameLabel;
        private System.Windows.Forms.Label FirstNameLabel;
    }
}