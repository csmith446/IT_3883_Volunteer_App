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
    public partial class MainVolunteerForm : Form
    {
        private List<int> SelectedEventIds;
        private List<User> UserList;
        private List<Event> EventList;

        private LoginForm LoginForm;
        private User CurrentUser;

        private enum OrderBy 
        { 
            Name, 
            NameDesc, 
            Location, 
            LocationDesc, 
            Date, 
            DateDesc 
        };

        public MainVolunteerForm(User user, LoginForm loginScreen)
        {
            InitializeComponent();
            LoginForm = loginScreen;

            SetupAllLists(user);
            SetLoggedInUserName();
            SetRegisteredCountLable();
            SetAdminStatus();

            SelectedEventIds = new List<int>();
            LoadCurrentEvents(HideRegisteredEventsCheckBox.Checked);
        }

        private void SetupAllLists(User user)
        {
            UserList = DatabaseManager.GetAllUsers();
            EventList = DatabaseManager.GetAllEvents();

            foreach (var usr in UserList)
            {
                foreach (var evt in EventList)
                {
                    if (evt.Creator.Id == usr.Id)
                    {
                        usr.CreatedEvents.Add(evt);
                        usr.RegisteredEvents.Add(evt);
                        evt.RegisteredUsers.Add(usr);
                    }
                    else if (DatabaseManager.IsUserRegisteredForEvent(evt.Id, usr.Id))
                    {
                        usr.RegisteredEvents.Add(evt);
                        evt.RegisteredUsers.Add(usr);
                    }
                }

                if (usr.Id == user.Id)
                    CurrentUser = usr;
            }
        }

        private void SetAdminStatus()
        {
            if (!CurrentUser.IsAdmin)
            {
                MainTabControl.TabPages.Remove(AdminTab);
            }
        }

        private void SetRegisteredCountLable()
        {
            int events = DatabaseManager.GetRegisteredCountForUser(CurrentUser.Id);
            RegisteredEventCountLabel.Text = string.Format("Registered for {0} upcoming events", events);
        }

        private void SetLoggedInUserName()
        {
            string loginStatus = LogInStatusLabel.Text.Replace("[username]",
                String.Format("{0}, {1}", CurrentUser.FullName.Item2,
                CurrentUser.FullName.Item1));

            LogInStatusLabel.Text = loginStatus;
        }

        private OrderBy CurrentOrder;
        private void LoadCurrentEvents(bool hideRegistered, OrderBy order = OrderBy.Date)
        {
            VolunteerEventsListView.Items.Clear();
            VolunteerEventsListView.CheckBoxes = true;
            var currentEvents = DatabaseManager.GetAllEvents();
            var RegisteredEventItems = new List<ListViewItem>();
            switch (order)
            {
                case OrderBy.Name:
                    currentEvents = currentEvents.OrderBy(evt => evt.Name).ToList();
                    break;
                case OrderBy.NameDesc:
                    currentEvents = currentEvents.OrderByDescending(evt => evt.Name).ToList();
                    break;
                case OrderBy.Location:
                    currentEvents = currentEvents.OrderBy(evt => evt.Location).ToList();
                    break;
                case OrderBy.LocationDesc:
                    currentEvents = currentEvents.OrderByDescending(evt => evt.Location).ToList();
                    break;
                case OrderBy.DateDesc:
                    currentEvents = currentEvents.OrderByDescending(evt => evt.Date).ToList();
                    break;
            }

            CurrentOrder = order;
            FixHeaderColors();

            foreach (var evt in currentEvents)
            {
                //visible subitems
                var item = new ListViewItem(evt.Name);
                item.SubItems.Add(evt.Location);
                item.SubItems.Add(evt.Date);

                //'invisible' subitems
                item.SubItems.Add(evt.StartTime.ToString("hh:mm tt") + 
                    " - " + evt.EndTime.ToString("hh:mm tt"));
                item.SubItems.Add(String.Format("{1}, {0}",
                    evt.Creator.FullName.Item1,
                    evt.Creator.FullName.Item2));
                item.SubItems.Add(evt.Creator.PhoneNumber);
                item.SubItems.Add(evt.Creator.Username);
                item.SubItems.Add(evt.RegisteredUsers.Count.ToString());
                item.SubItems.Add(evt.Id.ToString());
                item.SubItems.Add(evt.Duration.ToString());

                if (SelectedEventIds.Contains(evt.Id)) item.Checked = true;

                if (DatabaseManager.IsUserRegisteredForEvent(evt.Id, CurrentUser.Id))
                    RegisteredEventItems.Add(item);
                else
                    VolunteerEventsListView.Items.Add(item);
            }

            if (!hideRegistered)
            {
                foreach (var evt in RegisteredEventItems)
                {
                    evt.ForeColor = Color.Red;
                    VolunteerEventsListView.Items.Add(evt);
                }
            }

            if (VolunteerEventsListView.Items.Count == 0)
            {
                VolunteerEventsListView.Items.Add(new ListViewItem("There are no new events"));
                VolunteerEventsListView.CheckBoxes = false;
                VolunteerEventsListView.Enabled = false;
                DetailedInformationGroupBox.Visible = false;
            } 
            else
            {
                VolunteerEventsListView.Enabled = true;
                DetailedInformationGroupBox.Visible = true;
            }

            HideRegisteredEventsCheckBox.Visible = RegisteredEventItems.Count > 0;
            RedEventEntryLabel.Visible = RegisteredEventItems.Count > 0;
            VolunteerEventsListView.Items[0].Selected = true;
        }

        private void FixHeaderColors()
        {
            UpcomingEventNameHeaderButton.BackColor = (CurrentOrder == OrderBy.Name) ? Color.Green : Color.LightGreen;
            UpcomingEventDateHeaderButton.BackColor = (CurrentOrder == OrderBy.Date) ? Color.Green : Color.LightGreen;
            UpcomingEventLocationHeaderButton.BackColor = (CurrentOrder == OrderBy.Location) ? Color.Green : Color.LightGreen;
        }

        private void VolunteerEventsListView_ItemSelectionChanged(object sender,
            ListViewItemSelectionChangedEventArgs e)
        {
            var item = e.Item;
            if (item.Text != "There are no new events")
            {
                if (item.Selected)
                {
                    DetailedEventTimeLabel.Text = item.SubItems[3].Text;
                    DetailedContactNameLabel.Text = item.SubItems[4].Text;
                    DetailedContactNumberLabel.Text = item.SubItems[5].Text;
                    DetailedContactEmailLink.Text = item.SubItems[6].Text;
                    DetailedAttendeesLabel.Text = item.SubItems[7].Text;
                }
            }
            else
            {
                DetailedEventTimeLabel.Text = DetailedAttendeesLabel.Text = DetailedContactNameLabel.Text =
                    DetailedContactNumberLabel.Text = DetailedContactEmailLink.Text = "";
            }
        }

        private bool UserLoggedOut = false;
        private void ExitMenuItem_Click(object sender, EventArgs e)
        {
            UserLoggedOut = false;
            this.Close();
        }

        private void AboutMenuItem_Click(object sender, EventArgs e)
        {
            var about = new AboutBox();
            about.ShowDialog();            
        }

        private void LogOutMenuItem_Click(object sender, EventArgs e)
        {
            UserLoggedOut = true;
            this.Close();
        }

        private void MainWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (UserLoggedOut)
                LoginForm.Show();
            else
                Application.Exit();
        }

        private void HideRegisteredEventsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (HideRegisteredEventsCheckBox.Checked)
                LoadCurrentEvents(true);
            else
                LoadCurrentEvents(false);
        }

        //todo: duration check for added events to prevent overlap 
        private void VolunteerEventsListView_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (e.Item.ForeColor == Color.Red)
                e.Item.Checked = false;

            if (e.Item.Checked)
            {
                SelectedEventIds.Add(Int32.Parse(e.Item.SubItems[8].Text));
                SelectedEventsListBox.Items.Add(FormatSelctedItem(e.Item));
            }
            else
            {
                SelectedEventIds.Remove(Int32.Parse(e.Item.SubItems[8].Text));
                SelectedEventsListBox.Items.Remove(FormatSelctedItem(e.Item));
            }

            SetSelectedCount(SelectedEventsListBox.Items.Count);
            GenerateScheduleCheckBox.Enabled = ClearSelectedEventsButton.Enabled =
                RegisterForSelectedEventsButton.Enabled = (SelectedEventsListBox.Items.Count > 0);
        }

        private string FormatSelctedItem(ListViewItem item)
        {
            string date = item.SubItems[2].Text;
            string time = item.SubItems[3].Text;
            string name = item.SubItems[0].Text;

            return date + "  @  " + time + "\t" + name;
        }

        private void SetSelectedCount(int count)
        {
            var currentCount = string.Format("({0}) Selected ", count);
            currentCount += (count > 1 || count == 0) ? "Events" : "Event";
            SelectedEventsCountLabel.Text = currentCount;
        }

        private void ClearSelectedEvents()
        {
            foreach (ListViewItem item in VolunteerEventsListView.Items)
                item.Checked = false;

            SelectedEventIds.Clear();
        }

        private void ContactNameLabel_Hover(object sender, EventArgs e)
        {
            if (DetailedContactNameLabel.Text.Contains("..."))
            {
                var name = VolunteerEventsListView.SelectedItems[0].SubItems[4].Text;
                MainToolTip.Show(name, DetailedContactNameLabel);
            }
        }

        private void ContactEmailLabel_Hover(object sender, EventArgs e)
        {
            if (DetailedContactEmailLink.Text.Contains("..."))
            {
                var name = VolunteerEventsListView.SelectedItems[0].SubItems[6].Text;
                MainToolTip.Show(name, DetailedContactEmailLink);
            }
        }

        private void ClearSelectedEventsButton_Click(object sender, EventArgs e)
        {
            ClearSelectedEvents();
        }

        private void RegisterForSelectedEventsButton_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in VolunteerEventsListView.CheckedItems)
            {
                DatabaseManager.RegisterUserForEvent(CurrentUser.Id, Int32.Parse(item.SubItems[8].Text));
            }

            MessageBox.Show(string.Format("You registered for {0} events!", VolunteerEventsListView.CheckedItems.Count));
            ClearSelectedEvents();
            LoadCurrentEvents(HideRegisteredEventsCheckBox.Checked, CurrentOrder);
            SetRegisteredCountLable();
        }

        private void EventNameHeaderButton_Click(object sender, EventArgs e)
        {
            if (CurrentOrder == OrderBy.Name)
                LoadCurrentEvents(HideRegisteredEventsCheckBox.Checked, OrderBy.NameDesc);
            else
                LoadCurrentEvents(HideRegisteredEventsCheckBox.Checked, OrderBy.Name);

            VolunteerEventsListView.Focus();
        }

        private void EventLocationHeaderButton_Click(object sender, EventArgs e)
        {
            if (CurrentOrder == OrderBy.Location)
                LoadCurrentEvents(HideRegisteredEventsCheckBox.Checked, OrderBy.LocationDesc);
            else
                LoadCurrentEvents(HideRegisteredEventsCheckBox.Checked, OrderBy.Location);

            VolunteerEventsListView.Focus();
        }

        private void EventDateHeaderButton_Click(object sender, EventArgs e)
        {
            if (CurrentOrder == OrderBy.Date)
                LoadCurrentEvents(HideRegisteredEventsCheckBox.Checked, OrderBy.DateDesc);
            else
                LoadCurrentEvents(HideRegisteredEventsCheckBox.Checked);

            VolunteerEventsListView.Focus();
        }

        private void OpenEditUserForm()
        {
            var editUserForm = new EditUserForm(CurrentUser, false);
            editUserForm.ShowDialog(this);
        }

        private void UpdateInformationMenuItem_Click(object sender, EventArgs e)
        {
            OpenEditUserForm();
        }
        
        private void OpenCreateUserForm()
        {
            var createUserForm = new CreateUserForm();
            createUserForm.ShowDialog();
        }

        private void AdminCreateUserButton_Click(object sender, EventArgs e)
        {
            OpenCreateUserForm();
        }

        private void CreateNewEventButton_Click(object sender, EventArgs e)
        {
            var createEventForm = new CreateEventForm(CurrentUser);
            createEventForm.ShowDialog();
        }

        private void GetAllUsersToManage()
        {

        }

        private void GetAllEventsToManage()
        {

        }

        private void LoadUserCreatedEvents()
        {
            //todo: order items with header buttons
            var createdEvents = CurrentUser.CreatedEvents;
            foreach (var evt in createdEvents)
            {
                //visible subitems
                var item = new ListViewItem(evt.Name);
                item.SubItems.Add(evt.Date);

                //'invisible' subitems
                item.SubItems.Add(evt.StartTime.ToString("hh:mm tt") +
                    " - " + evt.EndTime.ToString("hh:mm tt"));
                item.SubItems.Add(evt.RegisteredUsers.Count.ToString());
                UserCreatedEventsListView.Items.Add(item);
            }

            if (UserCreatedEventsListView.Items.Count == 0)
            {
                UserCreatedEventsListView.Items.Add(new ListViewItem("You haven't created any events"));
                UserCreatedEventsListView.Enabled = false;
            }
            else
            {
                UserCreatedEventsListView.Enabled = true;
            }

            UserCreatedEventsListView.Items[0].Selected = true;
        }

        private void LoadUserRegisteredEvents()
        {
            //todo: order items with header buttons
            var registeredEvents = CurrentUser.RegisteredEvents;
            foreach (var evt in registeredEvents)
            {
                //visible subitems
                var item = new ListViewItem(evt.Name);
                item.SubItems.Add(evt.Date);

                //'invisible' subitems
                item.SubItems.Add(evt.Creator.Username);

                if(!CurrentUser.CreatedEvents.Contains(evt))
                    UserRegisteredEventsListView.Items.Add(item);
            }

            if (UserRegisteredEventsListView.Items.Count == 0)
            {
                UserRegisteredEventsListView.Items.Add(new ListViewItem("You aren't registered for any events"));
                UserRegisteredEventsListView.Enabled = false;
            }
            else
            {
                UserRegisteredEventsListView.Enabled = true;
            }

            UserRegisteredEventsListView.Items[0].Selected = true;
        }

        private void MainTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch(MainTabControl.SelectedIndex)
            {
                case 0:
                    LoadCurrentEvents(HideRegisteredEventsCheckBox.Checked, CurrentOrder);
                    break;
                case 1:
                    LoadUserCreatedEvents();
                    LoadUserRegisteredEvents();
                    break;
                case 2:
                    GetAllEventsToManage();
                    GetAllUsersToManage();
                    break;
            }
        }

        private void UserCreatedEventsListView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            EditSelectedEventButton.Enabled = UserCreatedEventsListView.SelectedItems.Count > 0;
            DeleteSelectedEventButton.Enabled = UserCreatedEventsListView.SelectedItems.Count > 0;
        }

        private void UserRegisteredEventsListView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            ViewSelectedEventButton.Enabled = UserRegisteredEventsListView.SelectedItems.Count > 0;
            UnregisterFromEventButton.Enabled = UserRegisteredEventsListView.SelectedItems.Count > 0;
        }
    }
}
