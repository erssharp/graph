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

namespace Graph
{
    /// <summary>
    /// Логика взаимодействия для Weight.xaml
    /// </summary>
    public partial class Weight : Window
    {
        public Weight()
        {
            InitializeComponent();
        }

        private void wght_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Key <= Key.D9 && e.Key >= Key.D0) || (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9))
                return;
            else
                e.Handled = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
