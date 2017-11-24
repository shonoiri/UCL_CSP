using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Microsoft.VisualBasic.FileIO;

namespace EX_01
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static ObservableCollection<MeetingCenter> centres = new ObservableCollection<MeetingCenter>();
        private const string InputFileName = "ImportData.csv";

        public MainWindow()
        {
            InitializeComponent();
            LoadEmployeeData();
            DataGridCenters.DataContext = centres;
        }

        private void DataGridCenters_CurrentCellChanged(object sender, EventArgs e)
        {
            if (DataGridCenters.CurrentItem != null)
            {
                MeetingCenter center = DataGridCenters.CurrentItem as MeetingCenter;
                CurrentCentre.DataContext = center;
                DataGridRooms.DataContext = center.Rooms;
                CurrentRoom.DataContext = null;
            }
        }

        private void DataGridRooms_CurrentCellChanged(object sender, EventArgs e)
        {
            if (DataGridRooms.CurrentItem != null)
            {
                CurrentRoom.DataContext = DataGridRooms.CurrentItem as MeetingRoom;
            }
        }

        private void BtnCreateRoom_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentCentre.DataContext != null)
            {
                MeetingRoom room = new MeetingRoom();
                room.MeetingCenterCode = (CurrentCentre.DataContext as MeetingCenter).Code;

                EditRoomWindow editWindow = new EditRoomWindow(room);
                editWindow.ShowDialog();
                if (editWindow.DialogResult == true)
                {
                    room.Name = editWindow.TBoxRoomName.Text;
                    room.Code = editWindow.TBoxRoomCode.Text;
                    room.Code = editWindow.TBoxRoomDescription.Text;
                    room.Capacity = Int32.Parse(editWindow.TBoxRoomCapacity.Text);
                    room.VideoConference = editWindow.CBoxVideo.IsChecked.Value;
                    (CurrentCentre.DataContext as MeetingCenter).Rooms.Add(room);
                }
                DataGridRooms.Items.Refresh();
            }
            else
            {
                MessageBox.Show("Please select Meeting Centre", "Meeting Centre Not Selected", MessageBoxButton.OK);
            }
        }

        private void BtnCreateCenter_Click(object sender, RoutedEventArgs e)
        {
            MeetingCenter center = new MeetingCenter();
            EditCenterWindow editWindow = new EditCenterWindow(center);
            editWindow.ShowDialog();
            if (editWindow.DialogResult == true)
            {
                center.Name = editWindow.TBoxCenterName.Text;
                center.Code = editWindow.TBoxCenterCode.Text;
                center.Code = editWindow.TBoxCenterDescription.Text;
                centres.Add(center);
                DataGridCenters.Items.Refresh();
            }
        }

        private void BtnDeleteCenter_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridCenters.SelectedItem != null)
            {
                MeetingCenter center = DataGridCenters.SelectedItem as MeetingCenter;
                centres.Remove(center);
                DataGridRooms.DataContext = null;
                CurrentCentre.DataContext = null;
            }
            this.UpdateLayout();
        }

        private void BtnEditCenter_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentCentre.DataContext != null)
            {
                MeetingCenter center = CurrentCentre.DataContext as MeetingCenter;
                EditCenterWindow editWindow = new EditCenterWindow(center);
                editWindow.Show();
                CurrentCentre.DataContext = center;
                DataGridRooms.Items.Refresh();
                DataGridCenters.Items.Refresh();
                CurrentCentre.DataContext = null;
            }
            else
            {
                MessageBox.Show("Please select Meeting Centre", "Meeting Centre Not Selected", MessageBoxButton.OK);
            }
        }

        private void BtnDeleteRoom_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridRooms.SelectedItem != null)
            {
                MeetingRoom room = DataGridRooms.SelectedItem as MeetingRoom;
                GetMeetingCenterByName(room.MeetingCenterCode).Rooms.Remove(room);
                CurrentRoom.DataContext = null;
            }
            DataGridRooms.Items.Refresh();
        }

        private void BtnEditRoom_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentRoom.DataContext != null)
            {
                MeetingRoom room = CurrentRoom.DataContext as MeetingRoom;
                EditRoomWindow editWindow = new EditRoomWindow(room);
                editWindow.ShowDialog();
                if (editWindow.DialogResult == true)
                {
                    room.Name = editWindow.TBoxRoomName.Text;
                    room.Code = editWindow.TBoxRoomCode.Text;
                    room.Description = editWindow.TBoxRoomDescription.Text;
                    room.Capacity = Int32.Parse(editWindow.TBoxRoomCapacity.Text);
                    room.VideoConference = editWindow.CBoxVideo.IsChecked.Value;
                    DataGridRooms.Items.Refresh();
                    CurrentRoom.DataContext = room;
                }
            }
            else
            {
                if (CurrentCentre.DataContext == null)
                    MessageBox.Show("Please select Meeting Centre", "Meeting Centre Not Selected", MessageBoxButton.OK);
                else
                    MessageBox.Show("Please select Meeting Room", "Meeting Room Not Selected", MessageBoxButton.OK);
            }
        }

        private static MeetingCenter GetMeetingCenterByName(string Name)
        {
            return centres.Where(x => x.Code.Equals(Name)).First();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            //save data - save data warning ?
        }

        private void LoadEmployeeData()
        {
            using (TextFieldParser parser = new TextFieldParser(InputFileName))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                string[] fields = parser.ReadFields();

                //MEETING_CENTRES
                fields = parser.ReadFields();
                while (!fields[0].Equals("MEETING_ROOMS", StringComparison.InvariantCultureIgnoreCase))
                {
                    MeetingCenter center = new MeetingCenter();
                    center.Name = fields[0];
                    center.Code = fields[1];
                    center.Description = fields[2];
                    centres.Add(center);
                    fields = parser.ReadFields();
                }

                //MEETING_ROOMS
                while (!parser.EndOfData)
                {
                    fields = parser.ReadFields();
                    MeetingRoom room = new MeetingRoom();
                    room.Name = fields[0];
                    room.Code = fields[1];
                    room.Description = fields[2];
                    room.Capacity = Int32.Parse(fields[3]);
                    room.VideoConference = fields[4].Equals("yes", StringComparison.InvariantCultureIgnoreCase);
                    room.MeetingCenterCode = fields[5];
                    GetMeetingCenterByName(fields[5]).Rooms.Add(room);
                }

                parser.Close();
            }
        }
    }
}
