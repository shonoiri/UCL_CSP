using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Microsoft.VisualBasic.FileIO;
using Microsoft.Win32;
using System.IO;

namespace EX_01
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static ObservableCollection<MeetingCenter> centres = new ObservableCollection<MeetingCenter>();
        private string FileName = "";

        public MainWindow()
        {
            InitializeComponent();
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
                MeetingRoom room = DataGridRooms.SelectedItem as MeetingRoom;
                GetMeetingCenterByCode(room.MeetingCenterCode).Rooms.Remove(room);
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
            foreach (MeetingCenter center in centres)
            {
                if (center.Code.Equals(Name))
                    return center;
            }
            return null;
        }

        public static MeetingRoom GetMeetingRoomByCode(string Code)
        {
            foreach (MeetingCenter center in centres)
            {
                foreach (MeetingRoom room in center.Rooms)
                    if (room.Code.Equals(Code))
                        return room;
            }
            return null;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            //save data - save data warning ?
        }

        private void LoadData(string InputFileName)
        {
            using (TextFieldParser parser = new TextFieldParser(InputFileName))
            {
                centres = new ObservableCollection<MeetingCenter>();
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
                    GetMeetingCenterByCode(fields[5]).Rooms.Add(room);
                }

                parser.Close();
                DataGridCenters.DataContext = centres;
            }
        }

        private void SaveData(string fileName)
        {
            using (StreamWriter outputFile = new StreamWriter(fileName, false))
            {
                outputFile.WriteLine("MEETING_CENTRES\n");
                foreach (MeetingCenter center in centres)
                    outputFile.WriteLine(center.ToString());
                outputFile.WriteLine("MEETING_ROOMS\n");
                foreach (MeetingCenter center in centres)
                    foreach (MeetingRoom room in center.Rooms)
                        outputFile.WriteLine(room.ToString());
                outputFile.Close();
            }
        }

        private void BtnUploadFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text documents (.csv)|*.csv";
            if (openFileDialog.ShowDialog() == true)
                LoadData(openFileDialog.FileName);
            this.FileName = openFileDialog.FileName;
        }

        private void BtnSaveFile_Click(object sender, RoutedEventArgs e)
        {
            SaveData(this.FileName);
        }

        private void BtnExportFile_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.FileName = "ManagerData";
            dlg.DefaultExt = ".csv";
            dlg.Filter = "CSV documents (.csv)|*.csv";

            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                SaveData(dlg.FileName);
            }

        }
    }
}
