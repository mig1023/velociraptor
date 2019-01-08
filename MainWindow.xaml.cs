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

using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Cvb;
using System.Runtime.InteropServices;
using System.Diagnostics;
using Emgu.CV.Util;

namespace openCVtest
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            String win1 = "Test Window";
            CvInvoke.NamedWindow(win1);

            Mat img1 = new Mat(200, 400, DepthType.Cv8U, 3);
            img1.SetTo(new Bgr(255, 0, 0).MCvScalar);

            CvInvoke.PutText(
               img1,
               "Hello, world",
               new System.Drawing.Point(10, 80),
               FontFace.HersheyComplex,
               1.0,
               new Bgr(0, 255, 0).MCvScalar);

            CvInvoke.Imshow(win1, img1);
            CvInvoke.WaitKey(0);
            CvInvoke.DestroyWindow(win1);
        }
    }
}
