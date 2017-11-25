using System.Windows;

namespace EX_01
{
    /// <summary>
    /// Interaction logic for EditCenterWindow.xaml
    /// </summary>
    public partial class EditCenterWindow : Window
    {
        public EditCenterWindow(MeetingCenter center)
        {
            InitializeComponent();
            center.BeginEdit();
            Centre.DataContext = center;
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            (Centre.DataContext as MeetingCenter).CancelEdit();
            this.Close();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            var center = Centre.DataContext as MeetingCenter;
            center.EndEdit();
            center.Rooms.ForEach(r => r.MeetingCenterCode = center.Code);
            this.Close();

        }
    }
}
