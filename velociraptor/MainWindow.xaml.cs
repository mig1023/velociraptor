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
        Random r = new Random();

        public MainWindow()
        {
            InitializeComponent();

            Interface.main = this;

            Interface.FullScreen();

            Pages.LoadPages();
        }

        public void moveOn_Click(object sender, RoutedEventArgs e)
        {
            Array values = Enum.GetValues(typeof(Interface.moveDirection));
            Interface.moveDirection randomDirection = (Interface.moveDirection)values.GetValue(r.Next(values.Length));

            Button clickedButton = sender as Button;

            int gotoIndex = 1;

            if (clickedButton.Tag != null)
            {
                int index = 0;

                bool indexExists = int.TryParse(clickedButton.Tag.ToString(), out index);

                if (indexExists && (index != 0))
                    gotoIndex = index;
            }

            Interface.Move(gotoIndex, randomDirection);
        }
    }
}
