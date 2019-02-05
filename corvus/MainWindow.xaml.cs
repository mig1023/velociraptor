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
using System.Windows.Interop;
using System.IO;

namespace corvus
{
    public partial class MainWindow : Window
    {
        static string PHOTO_DIR = "photo";
        static string VIDEO_DIR = "video";

        public static VideoCapture capture = new VideoCapture();
        public static VideoWriter writer;
        public static CascadeClassifier cascadeClassifier = new CascadeClassifier("frontalFace.xml");

        public static int screenIndex = 0;
        public static bool startVideo = false;

        static System.Windows.Media.Brush recordOn = System.Windows.Media.Brushes.DarkRed;
        static System.Windows.Media.Brush recordOff = System.Windows.Media.Brushes.ForestGreen;

        public MainWindow()
        {
            InitializeComponent();

            if (!Directory.Exists(PHOTO_DIR))
                Directory.CreateDirectory(PHOTO_DIR);

            if (!Directory.Exists(VIDEO_DIR))
                Directory.CreateDirectory(VIDEO_DIR);

            ComponentDispatcher.ThreadIdle += new System.EventHandler(VideoFrameCapture);
        }

        public static void VideoFrameCapture(object obj, EventArgs e)
        {
            if (writer != null)
            {
                Mat m = capture.QueryFrame();
                writer.Write(m);
            }
            else if (startVideo)
            {
                System.Drawing.Size size = new System.Drawing.Size(capture.Width, capture.Height);

                int framecount = (int)Math.Floor(capture.GetCaptureProperty(Emgu.CV.CvEnum.CapProp.FrameCount));
                double framerate = capture.GetCaptureProperty(Emgu.CV.CvEnum.CapProp.Fps);

                writer = new VideoWriter(
                    fileName: VIDEO_DIR + "\\video" + screenIndex.ToString() + ".avi",
                    compressionCode: VideoWriter.Fourcc('M', 'P', '4', 'V'),
                    fps: 5,
                    size: size,
                    isColor: true
                );
            }
            

            using (var imageFrame = capture.QueryFrame().ToImage<Bgr, Byte>())
            {
                screenIndex += 1;

                if (imageFrame != null)
                {
                    var grayFrame = imageFrame.Convert<Gray, byte>();
                    var faces = cascadeClassifier.DetectMultiScale(grayFrame, 1.1, 10, System.Drawing.Size.Empty);

                    if (faces.Count() > 0)
                        startVideo = true;
                    else
                    {
                        startVideo = false;
                        writer = null;
                    }

                    var newFrameRgb = imageFrame.Convert<Rgb, byte>();

                    foreach (var face in faces)
                        newFrameRgb.Draw(face, new Rgb(System.Drawing.Color.LightSeaGreen), 1);

                    Application.Current.Dispatcher.BeginInvoke(new ThreadStart(delegate
                    {
                        MainWindow main = (MainWindow)Application.Current.MainWindow;

                        if (main != null)
                        {
                            string saveScreen = (faces.Count() > 0 ? PHOTO_DIR + "\\image_" + screenIndex.ToString() + ".jpg" : String.Empty);

                            main.image.Source = BitmapSourceConvert.ToBitmapSource(newFrameRgb, saveScreen);
                            main.recordMarker.Background = (saveScreen != String.Empty ? recordOn : recordOff);
                        }
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

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (writer != null)
                writer.Dispose();

            writer = null;
        }
    }
}
