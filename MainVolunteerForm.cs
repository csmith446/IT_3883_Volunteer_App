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
        private List<int> SelectedEventIds;     //list for 'saving' selected events when sorting
        private List<User> UserList;            //main User list - pulled form database
        private List<Event> EventList;          //main event list - pulled from database

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

            UpdateLists(user);
            SetLoggedInUserName();
            SetAdminStatus();

            SelectedEventIds = new List<int>();
            LoadCurrentEvents(HideRegisteredEventsCheckBox.Checked);
        }

        //Ties all of the events' users and users' events together
        private void UpdateLists(User user)
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
                        evt.UpdateCreator(usr);
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

            SetRegisteredCountLable();
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
            if (CurrentUser.IsAdmin) loginStatus += " (Administrator)";

            LogInStatusLabel.Text = loginStatus;
        }

        private OrderBy CurrentOrder;
        private void LoadCurrentEvents(bool hideRegistered, OrderBy order = OrderBy.Date)
        {
            VolunteerEventsListView.Items.Clear();
            VolunteerEventsListView.CheckBoxes = true;
            var currentEvents = EventList;
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
            UpcomingEventNameHeaderButton.BackColor = (CurrentOrder == OrderBy.Name) ? Color.Green : Color.PaleGreen; ;
            UpcomingEventDateHeaderButton.BackColor = (CurrentOrder == OrderBy.Date) ? Color.Green : Color.PaleGreen;
            UpcomingEventLocationHeaderButton.BackColor = (CurrentOrder == OrderBy.Location) ? Color.Green : Color.PaleGreen;
        }

        private void VolunteerEventsListView_ItemSelectionChanged(object sender,
            ListViewItemSelectionChangedEventArgs e)
        {
            //todo: possibly make the group box look nicer?
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
            //todo: generate schedule/print preview and printing
            //bool generateSchedule = GenerateScheduleCheckBox.Checked;

            foreach (ListViewItem item in VolunteerEventsListView.CheckedItems)
            {
                DatabaseManager.RegisterUserForEvent(CurrentUser.Id, Int32.Parse(item.SubItems[8].Text));
            }

            MessageBox.Show(string.Format("You registered for {0} events!", VolunteerEventsListView.CheckedItems.Count));
            ClearSelectedEvents();
            LoadCurrentEvents(HideRegisteredEventsCheckBox.Checked, CurrentOrder);
            UpdateLists(CurrentUser);
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

        //create a user
        private DialogResult ShowCreateUserForm()
        {
            var createUserForm = new CreateUserForm();
            createUserForm.ShowDialog();
            return createUserForm.DialogResult;
        }

        //edit a user
        private DialogResult ShowUserForm(User usr, bool adminEdit = false)
        {
            var viewUserForm = new ViewUserForm(usr, adminEdit);
            viewUserForm.ShowDialog(this);
            return viewUserForm.DialogResult;
        }

        //create an event
        private DialogResult ShowCreateEventForm()
        {
            var createEventForm = new CreateEventForm(CurrentUser);
            createEventForm.ShowDialog();
            return createEventForm.DialogResult;
        }

        //edit event; users: readOnly = true, admin: readOnly = false
        private DialogResult ShowEventForm(bool readOnly = false)
        {
            var selectedId = (!readOnly) ?
                Int32.Parse(UserCreatedEventsListView.SelectedItems[0].SubItems[4].Text) :
                Int32.Parse(UserRegisteredEventsListView.SelectedItems[0].SubItems[3].Text);
            var events = (readOnly) ? CurrentUser.RegisteredEvents : CurrentUser.CreatedEvents;
            Event selectedEvent = null;

            foreach (var evt in events)
            {
                if (evt.Id == selectedId)
                {
                    selectedEvent = evt;
                    break;
                }
            }

            var eventForm = new ViewEventForm(selectedEvent, readOnly);
            eventForm.ShowDialog();
            return eventForm.DialogResult;
        }

        private void UpdateInformationMenuItem_Click(object sender, EventArgs e)
        {
            ShowUserForm(CurrentUser);
            //todo: dialog result: if OK then
            //UpdateLists(CurrentUser);
        }

        private void AdminCreateUserButton_Click(object sender, EventArgs e)
        {
            ShowCreateUserForm();
        }

        private void CreateNewEventButton_Click(object sender, EventArgs e)
        {
            ShowCreateEventForm();
            //todo: dialog result: if ok then
            //UpdateLists(CurrentUser);
        }

        private void GetAllUsersToManage()
        {
            AdminUserListView.Items.Clear();
            foreach (var user in UserList)
            {
                var item = new ListViewItem(user.Username);
                item.SubItems.Add(string.Format("{1}, {0}",
                    user.FullName.Item1, user.FullName.Item2));
                item.SubItems.Add(user.CreatedEvents.Count.ToString());
                item.SubItems.Add(user.RegisteredEvents.Count.ToString());
                item.SubItems.Add(user.Id.ToString());
                AdminUserListView.Items.Add(item);
            }

            AdminUserListView.Items[0].Selected = true;
        }

        private void GetAllEventsToManage()
        {
            AdminEventListView.Items.Clear();
            foreach (var evt in EventList)
            {
                var item = new ListViewItem(evt.Name);
                item.SubItems.Add(evt.Date);
                item.SubItems.Add(evt.Creator.Username);
                item.SubItems.Add(evt.Id.ToString());
                AdminEventListView.Items.Add(item);
            }

            AdminEventListView.Items[0].Selected = true;
        }

        private void LoadUserCreatedEvents()
        {
            UserCreatedEventsListView.Items.Clear();
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
                item.SubItems.Add(evt.Id.ToString());
                UserCreatedEventsListView.Items.Add(item);
            }

            if (UserCreatedEventsListView.Items.Count == 0)
            {
                UserCreatedEventsListView.Enabled = false;
                UserCreatedEventsListView.Items.Add(new ListViewItem("You haven't created any events"));
            }
            else
            {
                UserCreatedEventsListView.Enabled = true;
            }

            UserCreatedEventsListView.Items[0].Selected = true;
        }

        private void LoadUserRegisteredEvents()
        {
            UserRegisteredEventsListView.Items.Clear();
            //todo: order items with header buttons
            var registeredEvents = CurrentUser.RegisteredEvents;
            foreach (var evt in registeredEvents)
            {
                //visible subitems
                var item = new ListViewItem(evt.Name);
                item.SubItems.Add(evt.Date);

                //'invisible' subitems
                item.SubItems.Add(evt.Creator.Username);
                item.SubItems.Add(evt.Id.ToString());

                if (!CurrentUser.CreatedEvents.Contains(evt))
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
            switch (MainTabControl.SelectedIndex)
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
            EditSelectedEventButton.Enabled = false;
            DeleteSelectedEventButton.Enabled = false;

            if (e.Item.Text == "You haven't created any events")
            {
                CreatedEventTime.Text = "";
                CreatedEventAttendees.Text = "";
                e.Item.Selected = false;
                return;
            }

            if (e.IsSelected)
            {
                CreatedEventTime.Text = e.Item.SubItems[2].Text;
                CreatedEventAttendees.Text = e.Item.SubItems[3].Text;
                EditSelectedEventButton.Enabled = true;
                DeleteSelectedEventButton.Enabled = true;
            }
        }

        private void UserRegisteredEventsListView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            ViewSelectedEventButton.Enabled = false;
            UnregisterFromEventButton.Enabled = false;

            if (e.Item.Text == "You aren't registered for any events")
            {
                RegisteredEventContactEmail.Text = "";
                e.Item.Selected = false;
                return;
            }

            if (e.IsSelected)
            {
                RegisteredEventContactEmail.Text = e.Item.SubItems[2].Text;
                ViewSelectedEventButton.Enabled = true;
                UnregisterFromEventButton.Enabled = true;
            }
        }

        private void EditSelectedEventButton_Click(object sender, EventArgs e)
        {
            ShowEventForm();
            //todo: dialog result: if OK then
            //UpdateLists(CurrentUser);
        }

        private void ViewSelectedEventButton_Click(object sender, EventArgs e)
        {
            ShowEventForm(true);
            //todo: dialog result: if OK then
            //UpdateLists(CurrentUser);
        }

        private void AdminUserListView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            AdminEditUserButton.Enabled = AdminUserListView.SelectedItems.Count > 0;
            AdminDeleteUserButton.Enabled = AdminUserListView.SelectedItems.Count > 0;
            if (e.IsSelected)
            {
                AdminUserCreatedEvents.Text = e.Item.SubItems[2].Text;
                AdminUserRegisteredEvents.Text = e.Item.SubItems[3].Text;
            }
        }

        private void AdminEventListView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            AdminEditEventButton.Enabled = AdminEventListView.SelectedItems.Count > 0;
            AdminDeleteEventButton.Enabled = AdminEventListView.SelectedItems.Count > 0;
            if (e.IsSelected)
            {
                AdminEventContactEmail.Text = e.Item.SubItems[2].Text;
            }
        }

        private void AdminEditUserButton_Click(object sender, EventArgs e)
        {
            var selectedId =
                Int32.Parse(AdminUserListView.SelectedItems[0].SubItems[4].Text);
            User selectedUser = null;

            foreach (var user in UserList)
            {
                if (user.Id == selectedId)
                {
                    selectedUser = user;
                    break;
                }
            }

            ShowUserForm(selectedUser, true);
            //todo: diagog result: if OK then
            //UpdateLists(CurrentUser);
        }

        private void AdminEditEventButton_Click(object sender, EventArgs e)
        {
            var selectedId =
                Int32.Parse(AdminEventListView.SelectedItems[0].SubItems[3].Text);
            Event selectedEvent = null;

            foreach (var evt in EventList)
            {
                if (evt.Id == selectedId)
                {
                    selectedEvent = evt;
                    break;
                }
            }

            var eventForm = new ViewEventForm(selectedEvent, false);
            eventForm.ShowDialog();
            UpdateLists(CurrentUser);
        }

        private void DeleteSelectedEventButton_Click(object sender, EventArgs e)
        {
            //todo: functionality for deleting a created event
            //UpdateLists(CurrentUser);
        }

        private void UnregisterFromEventButton_Click(object sender, EventArgs e)
        {
            //todo: functionality for unregistering for an event
            //UpdateLists(CurrentUser);
        }

        private void GenerateEventScheduleButton_Click(object sender, EventArgs e)
        {
            //todo: functionality for generating/printing event schedule 
            //bool includeCreated = IncludeCreatedEventsCheckBox.Checked;
        }

        private void AdminDeleteUserButton_Click(object sender, EventArgs e)
        {
            //odo: admin functionality for deleting a user
            //UpdateLists(CurrentUser);
        }

        private void AdminDeleteEventButton_Click(object sender, EventArgs e)
        {
            //todo: admin functionality for deleting entire event
            //UpdateLists(CurrentUser);
        }

        private void AdminRegisterUserButton_Click(object sender, EventArgs e)
        {
            //todo: admin functionality for manually registering a user for an event
            //UpdateLists(CurrentUser);
        }

        private void AdminGnerateReportButton_Click(object sender, EventArgs e)
        {
            //todo: admin generate event report
        }

        private void ScrollDownButton_Click(object sender, EventArgs e)
        {
            VolunteerEventsListView.Items[10].EnsureVisible();
            VolunteerEventsListView.Scrollable = true;
            VolunteerEventsListView.Focus();
        }

        private void VolunteerEventsListView_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {

        }

        private void VolunteerEventsListView_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            e.DrawDefault = true;
        }
    }
}
