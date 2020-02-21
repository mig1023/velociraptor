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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace velociraptor
{
    public partial class MainWindow : Window
    {
        static int nextNumber = 0;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            nextNumber += 1;

            OperatorBoard.NextNumber(String.Format("A{0:D3}", nextNumber));
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            OperatorBoard.Clean();
        }
    }
}
