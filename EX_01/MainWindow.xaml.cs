using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.VisualBasic.FileIO;

namespace EX_01
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<MeetingCenter> centres = new ObservableCollection<MeetingCenter>();
        private const string InputFileName = "ImportData.csv";

        public MainWindow()
        {
            InitializeComponent();
            LoadEmployeeData();
            DataGridCenters.DataContext = centres;
        }

        private void DataGridCenters_CurrentCellChanged(object sender, EventArgs e) {
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
            
            MeetingRoom room = new MeetingRoom();
            room.Name = TBoxRoomName.Text;
            room.Code = TBoxRoomCode.Text;
            room.Description = TBoxRoomDescription.Text;
            room.Capacity = Int32.Parse(TBoxRoomCapacity.Text);
            room.VideoConference = CBoxVideo.IsChecked.Value;
            room.MeetingCenterCode = (CurrentCentre.DataContext as MeetingCenter).Code;
            (CurrentCentre.DataContext as MeetingCenter).Rooms.Add(room);
            DataGridRooms.Items.Refresh();
        }

        private void BtnCreateCenter_Click(object sender, RoutedEventArgs e)
        {
            MeetingCenter center = new MeetingCenter();
            center.Name = TBoxCenterName.Text;
            center.Code = TBoxCenterCode.Text;
            center.Code = TBoxCenterDescription.Text;
            centres.Add(center);
            DataGridCenters.Items.Refresh();
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
            DataGridRooms.Items.Refresh();
            DataGridCenters.Items.Refresh();
        }

        private void BtnEditCenter_Click(object sender, RoutedEventArgs e)
        {

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

        }

        private MeetingCenter GetMeetingCenterByName(string Name) {
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
