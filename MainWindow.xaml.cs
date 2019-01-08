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
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //String win1 = "Test Window"; //The name of the window
            //CvInvoke.NamedWindow(win1); //Create the window using the specific name

            //Mat img1 = new Mat(200, 400, DepthType.Cv8U, 3); //Create a 3 channel image of 400x200
            //img1.SetTo(new Bgr(255, 0, 0).MCvScalar); // set it to Blue color

            ////Draw "Hello, world." on the image using the specific font
            //CvInvoke.PutText(
            //   img1,
            //   "Hello, world",
            //   new System.Drawing.Point(10, 80),
            //   FontFace.HersheyComplex,
            //   1.0,
            //   new Bgr(0, 255, 0).MCvScalar);


            //CvInvoke.Imshow(win1, img1); //Show the image
            //CvInvoke.WaitKey(0);  //Wait for the key pressing event
            //CvInvoke.DestroyWindow(win1); //Destroy the window if key is pressed


            //////////////////////////////////////////////////////////////

            //Load the image from file and resize it for display
            Image<Bgr, Byte> img =
               new Image<Bgr, byte>("1.png")
               .Resize(400, 400, Emgu.CV.CvEnum.Inter.Linear, true);

            imageBox.Source = BitmapSourceConvert.ToBitmapSource(img);
        }

        public static class BitmapSourceConvert
        {
            [DllImport("gdi32")]
            private static extern int DeleteObject(IntPtr o);

            public static BitmapSource ToBitmapSource(IImage image)
            {
                using (System.Drawing.Bitmap source = image.Bitmap)
                {
                    IntPtr ptr = source.GetHbitmap();

                    BitmapSource bs = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                        ptr,
                        IntPtr.Zero,
                        Int32Rect.Empty,
                        System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions());

                    DeleteObject(ptr);
                    return bs;
                }
            }
        }
    }
}
