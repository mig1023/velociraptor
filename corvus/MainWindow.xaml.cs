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

namespace corvus
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Interface.FullScreen(this);
        }

        private void toWhite_Click(object sender, RoutedEventArgs e)
        {
            Interface.Move(
                moveCanvas: white,
                prevCanvas: red
            );
        }

        private void toRed_Click(object sender, RoutedEventArgs e)
        {
            Interface.Move(
                moveCanvas: red,
                prevCanvas: white
            );
        }

        private void toWhiteFromBlue_Click(object sender, RoutedEventArgs e)
        {
            Interface.Move(
                moveCanvas: white,
                prevCanvas: blue,
                direction: Interface.moveDirection.vertical
            );
        }

        private void toBlue_Click(object sender, RoutedEventArgs e)
        {
            Interface.Move(
                moveCanvas: blue,
                prevCanvas: white,
                direction: Interface.moveDirection.vertical
            );
        }
    }
}
