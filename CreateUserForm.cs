using System;
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
    public partial class CreateUserForm : Form
    {
        //todo: input validation, error providers, database query
        //todo: dialog result

        public CreateUserForm()
        {
            InitializeComponent();
        }

        private void CancelNewUserButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
