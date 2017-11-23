using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;


namespace EX_01
{
    /// <summary>
    /// Логика взаимодействия для EditRoomWindow.xaml
    /// </summary>
    public partial class EditRoomWindow : Window
    {
        public EditRoomWindow(MeetingRoom room)
        {
            InitializeComponent();
            CurrentRoom.DataContext = room;
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }
    }
}
