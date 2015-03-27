using System;
using System.Reflection;
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
    public partial class AboutBox : Form
    {
        public AboutBox()
        {
            InitializeComponent();
        }

        private void About_Load(object sender, EventArgs e)
        {
            string version = lblVersionNumber.Text.Replace("[version]",
                typeof(LoginForm).Assembly.GetName().Version.ToString());

            lblVersionNumber.Text = version;
        }
    }
}
