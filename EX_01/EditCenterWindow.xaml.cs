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
    /// Логика взаимодействия для EditCenterWindow.xaml
    /// </summary>
    public partial class EditCenterWindow : Window
    {
        public EditCenterWindow(MeetingCenter center)
        {
            InitializeComponent();
            CurrentCentre.DataContext = center;
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
