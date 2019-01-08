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
using Emgu.CV.UI;
using Emgu.CV.Structure;
using System.Drawing;
using System.Timers;
using System.Threading;
using System.Runtime.InteropServices;

namespace corvus
{
    public partial class MainWindow : Window
    {
        public static System.Timers.Timer video = new System.Timers.Timer(500);

        public static ImageViewer viewer = new ImageViewer();
        public static VideoCapture capture = new VideoCapture();

        public MainWindow()
        {
            InitializeComponent();

            video.Elapsed += new ElapsedEventHandler(VideoFrameCapture);
            video.Enabled = true;
            video.Start();
        }

        public static void VideoFrameCapture(object obj, ElapsedEventArgs e)
        {
            viewer.Image = capture.QueryFrame();

            Application.Current.Dispatcher.BeginInvoke(new ThreadStart(delegate
            {
                MainWindow main = (MainWindow)Application.Current.MainWindow;
                main.image.Source = BitmapSourceConvert.ToBitmapSource(viewer.Image);
            }));
        }
        public static class BitmapSourceConvert
        {
            [DllImport("gdi32")]
            private static extern int DeleteObject(IntPtr o);

            public static BitmapSource ToBitmapSource(IImage image)
            {
                using (Bitmap source = image.Bitmap)
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
