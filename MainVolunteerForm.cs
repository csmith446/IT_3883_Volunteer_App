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
        //todo: move list to user class
        private List<int> SelectedEventIds;

        private LoginForm LoginForm;
        private User CurrentUser;
        private enum OrderBy { Name, NameDesc, Location, LocationDesc, Date, DateDesc };

        public MainVolunteerForm(User user, LoginForm loginScreen)
        {
            InitializeComponent();
            CurrentUser = user;
            LoginForm = loginScreen;

            SetLoggedInUserName();
            SetRegisteredCountLable();
            SetAdminStatus();

            SelectedEventIds = new List<int>();
            LoadCurrentEvents(false);
        }

        private void SetAdminStatus()
        {
            //if (!DatabaseManager.IsUserAdmin(CurrentUser.Id))
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

        OrderBy CurrentOrder;
        private void LoadCurrentEvents(bool hideRegistered, OrderBy order = OrderBy.Date)
        {
            VolunteerEventsListView.Items.Clear();
            VolunteerEventsListView.CheckBoxes = true;
            var currentEvents = DatabaseManager.GetCurrentEvents();
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
                    evt.ContactInformation.FullName.Item1,
                    evt.ContactInformation.FullName.Item2));
                item.SubItems.Add(evt.ContactInformation.PhoneNumber);
                item.SubItems.Add(evt.ContactInformation.Username);
                item.SubItems.Add(evt.Attendees.ToString());
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
            RedEntryLabel.Visible = RegisteredEventItems.Count > 0;
            VolunteerEventsListView.Items[0].Selected = true;
        }

        private void FixHeaderColors()
        {
            EventNameHeaderButton.BackColor = (CurrentOrder == OrderBy.Name) ? Color.Green : Color.LightGreen;
            EventDateHeaderButton.BackColor = (CurrentOrder == OrderBy.Date) ? Color.Green : Color.LightGreen;
            EventLocationHeaderButton.BackColor = (CurrentOrder == OrderBy.Location) ? Color.Green : Color.LightGreen;
        }

        private void VolunteerEventsListView_ItemSelectionChanged(object sender,
            ListViewItemSelectionChangedEventArgs e)
        {
            var item = e.Item;
            if (item.Text != "There are no new events")
            {
                if (item.Selected)
                {
                    EventTimeLabel.Text = item.SubItems[3].Text;
                    ContactNameLabel.Text = item.SubItems[4].Text;
                    ContactNumberLabel.Text = item.SubItems[5].Text;
                    ContactEmailLink.Text = item.SubItems[6].Text;
                    AttendeesLabel.Text = item.SubItems[7].Text;
                }
            }
            else
            {
                EventTimeLabel.Text = AttendeesLabel.Text = ContactNameLabel.Text =
                    ContactNumberLabel.Text = ContactEmailLink.Text = "";
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
            if (ContactNameLabel.Text.Contains("..."))
            {
                var name = VolunteerEventsListView.SelectedItems[0].SubItems[4].Text;
                MainToolTip.Show(name, ContactNameLabel);
            }
        }

        private void ContactEmailLabel_Hover(object sender, EventArgs e)
        {
            if (ContactEmailLink.Text.Contains("..."))
            {
                var name = VolunteerEventsListView.SelectedItems[0].SubItems[6].Text;
                MainToolTip.Show(name, ContactEmailLink);
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
    }
}
