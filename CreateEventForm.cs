﻿using System;
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
    public partial class CreateEventForm : Form
    {
        private User Creator;

        public CreateEventForm(User currentUser)
        {
            InitializeComponent();
            Creator = currentUser;
        }
    }
}