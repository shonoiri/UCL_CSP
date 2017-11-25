using System.Windows;


namespace EX_01
{
    /// <summary>
    /// Interaction logic for EditRoomWindow.xaml
    /// </summary>
    public partial class EditRoomWindow : Window
    {
        public EditRoomWindow(MeetingRoom room)
        {
            InitializeComponent();
            room.BeginEdit();
            CurrentRoom.DataContext = room;
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            (CurrentRoom.DataContext as MeetingRoom).CancelEdit();
            this.Close();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            (CurrentRoom.DataContext as MeetingRoom).EndEdit();
            this.Close();

        }

    }
}
