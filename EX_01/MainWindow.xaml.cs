using System;
using System.Collections.ObjectModel;
using System.Windows;
using Microsoft.Win32;


namespace EX_01
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static ObservableCollection<MeetingCenter> centres;
        private string FileName = "";

        public MainWindow()
        {
            InitializeComponent();
            centres = new ObservableCollection<MeetingCenter>();
            DataGridCenters.DataContext = centres;
        }

        private void DataGridCenters_CurrentCellChanged(object sender, EventArgs e)
        {
            if (DataGridCenters.CurrentItem != null)
            {
                var center = DataGridCenters.CurrentItem as MeetingCenter;
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
                var room = new MeetingRoom();
                room.MeetingCenterCode = (CurrentCentre.DataContext as MeetingCenter).Code;
                var editWindow = new EditRoomWindow(room);
                editWindow.ShowDialog();
                (CurrentCentre.DataContext as MeetingCenter).Rooms.Add(room);
                DataGridRooms.Items.Refresh();
            }
            else
            {
                MessageBox.Show("Please select Meeting Centre", "Meeting Centre Not Selected", MessageBoxButton.OK);
            }
        }

        private void BtnCreateCenter_Click(object sender, RoutedEventArgs e)
        {
            var center = new MeetingCenter();
            var editWindow = new EditCenterWindow(center);
            editWindow.ShowDialog();
            centres.Add(center);
            DataGridCenters.Items.Refresh();
            DataGridRooms.Items.Refresh();
        }

        private void BtnDeleteCenter_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridCenters.SelectedItem != null)
            {
                var center = DataGridCenters.SelectedItem as MeetingCenter;
                centres.Remove(center);
                DataGridRooms.DataContext = null;
                CurrentCentre.DataContext = null;
            }
            DataGridCenters.Items.Refresh();
        }

        private void BtnEditCenter_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentCentre.DataContext != null)
            {
                var center = CurrentCentre.DataContext as MeetingCenter;
                var editWindow = new EditCenterWindow(center);
                editWindow.ShowDialog();
                DataGridRooms.Items.Refresh();
                CurrentCentre.DataContext = null;
                DataGridCenters.Items.Refresh();
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
                var room = DataGridRooms.SelectedItem as MeetingRoom;
                GetMeetingCenterByCode(room.MeetingCenterCode).Rooms.Remove(room);
                CurrentRoom.DataContext = null;
            }
            DataGridRooms.Items.Refresh();
        }

        private void BtnEditRoom_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentRoom.DataContext != null)
            {
                var room = CurrentRoom.DataContext as MeetingRoom;
                var editWindow = new EditRoomWindow(room);
                editWindow.ShowDialog();
                CurrentRoom.DataContext = null;
                DataGridRooms.Items.Refresh();
            }
            else
            {
                if (CurrentCentre.DataContext == null)
                    MessageBox.Show("Please select Meeting Centre", "Meeting Centre Not Selected", MessageBoxButton.OK);
                else
                    MessageBox.Show("Please select Meeting Room", "Meeting Room Not Selected", MessageBoxButton.OK);
            }
        }

        public static MeetingCenter GetMeetingCenterByCode(string Name)
        {
            foreach (var center in centres)
            {
                if (center.Code.Equals(Name))
                    return center;
            }
            return null;
        }

        public static MeetingRoom GetMeetingRoomByCode(string Code)
        {
            foreach (var center in centres)
            {
                foreach (var room in center.Rooms)
                    if (room.Code.Equals(Code))
                        return room;
            }
            return null;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if (centres.Count != 0)
            {
                MessageBoxResult result = MessageBox.Show("Save changes ?", "Exit", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                    if (string.IsNullOrEmpty(FileName))
                    {
                        var dlg = new SaveFileDialog();
                        dlg.FileName = "ManagerData";
                        dlg.DefaultExt = ".csv";
                        dlg.Filter = "CSV documents (.csv)|*.csv";

                        Nullable<bool> resultDlg = dlg.ShowDialog();

                        if (resultDlg == true)
                            try
                            {
                                DataLoader.SaveData(dlg.FileName, centres);
                            }
                            catch (Exception exc)
                            {
                                MessageBox.Show("Cannot save the data.\n " + exc.Message, "Error", MessageBoxButton.OK);
                                this.Activate();
                            }
                    }
                    else
                        try
                        {
                            DataLoader.SaveData(this.FileName, centres);
                        }
                        catch (Exception exc)
                        {
                            MessageBox.Show("Cannot save the data.\n " + exc.Message, "Error", MessageBoxButton.OK);
                        }
            }
        }

        private void BtnUploadFile_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "CSV documents (.csv)|*.csv";
            if (openFileDialog.ShowDialog() == true)
                try
                {
                    DataLoader.LoadData(openFileDialog.FileName, centres);
                    this.FileName = openFileDialog.FileName;
                    DataGridCenters.Items.Refresh();
                }
                catch (Exception exc)
                {
                    if (exc is NullReferenceException)
                        MessageBox.Show("Cannot load data from file.\n Check the correctness of file", "Error", MessageBoxButton.OK);
                    else
                        MessageBox.Show("Cannot load data from file.\n" + exc.Message, "Error", MessageBoxButton.OK);
                }
            else
                MessageBox.Show("Cannot open file.\n", "Error", MessageBoxButton.OK);
        }

        private void BtnSaveFile_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataLoader.SaveData(this.FileName, centres);
            }
            catch (Exception exc)
            {
                MessageBox.Show("Cannot save the data.\n " + exc.Message, "Error", MessageBoxButton.OK);
            }
        }

        private void BtnExportFile_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new SaveFileDialog();
            dlg.FileName = "ManagerData";
            dlg.DefaultExt = ".csv";
            dlg.Filter = "CSV documents (.csv)|*.csv";

            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
                try
                {
                    DataLoader.SaveData(dlg.FileName, centres);
                }
                catch (Exception exc)
                {
                    MessageBox.Show("Cannot save the data.\n " + exc.Message, "Error", MessageBoxButton.OK);
                }

        }
    }
}
