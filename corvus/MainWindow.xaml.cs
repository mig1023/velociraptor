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
using System.Drawing;

using Emgu.CV;
using Emgu.CV.UI;
using Emgu.CV.Structure;
using System.Timers;
using System.Threading;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;

namespace corvus
{
    public partial class MainWindow : Window
    {
        public static System.Timers.Timer video = new System.Timers.Timer(600);

        public static VideoCapture capture = new VideoCapture();

        public static CascadeClassifier cascadeClassifier = new CascadeClassifier("frontalFace.xml");

        public static int screenIndex = 0;

        public MainWindow()
        {
            InitializeComponent();

            video.Elapsed += new ElapsedEventHandler(VideoFrameCapture);
            video.Enabled = true;
            video.Start();
        }

        public static void VideoFrameCapture(object obj, ElapsedEventArgs e)
        {
            using (var imageFrame = capture.QueryFrame().ToImage<Bgr, Byte>())
            {
                screenIndex += 1;

                if (imageFrame != null)
                {
                    var grayFrame = imageFrame.Convert<Gray, byte>();
                    var faces = cascadeClassifier.DetectMultiScale(grayFrame, 1.1, 10, System.Drawing.Size.Empty);

                    var newFrameRgb = imageFrame.Convert<Rgb, byte>();

                    foreach (var face in faces)
                        newFrameRgb.Draw(face, new Rgb(System.Drawing.Color.LightSeaGreen), 1);

                    Application.Current.Dispatcher.BeginInvoke(new ThreadStart(delegate
                    {
                        MainWindow main = (MainWindow)Application.Current.MainWindow;

                        string saveScreen = (faces.Count() > 0 ? "image_" + screenIndex.ToString() + ".jpg" : String.Empty);

                        main.image.Source = BitmapSourceConvert.ToBitmapSource(newFrameRgb, saveScreen);

                        if (saveScreen != String.Empty)
                            main.saveLog.Text += saveScreen + "\n";
                    }));
                }


            }
        }
        public static class BitmapSourceConvert
        {
            [DllImport("gdi32")]
            private static extern int DeleteObject(IntPtr o);

            public static BitmapSource ToBitmapSource(IImage image, string saveScreen)
            {
                using (Bitmap source = image.Bitmap)
                {
                    IntPtr ptr = source.GetHbitmap();

                    if (saveScreen != String.Empty)
                        source.Save(saveScreen, ImageFormat.Jpeg);

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
