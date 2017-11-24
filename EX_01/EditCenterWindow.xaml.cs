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
using System.Windows.Controls;

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

            center.BeginEdit();
            Centre.DataContext = center;
        }

        /*
         Meeting Centre

            Name – název budovy (řetězec o délce 2..100 znaků).
            Code – unikátní identifikátor budovy (řetězec o délce 5..50 znaků, který může obsahovat velká a malá písmena anglické abecedy a znaky: tečka, dvojtečka a podtržítko).
            Description – slovní popis budovy (řetězec o délce 10..300 znaků).
          */



        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            (Centre.DataContext as MeetingCenter).CancelEdit();
            this.Close();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            (Centre.DataContext as MeetingCenter).EndEdit();
            this.Close();
        }
    }
}
