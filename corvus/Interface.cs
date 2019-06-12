using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace corvus
{
    class Interface
    {
        public enum moveDirection { horizontal_left, horizontal_right, vertical_top, vertical_bottom };

        public static Canvas currentCanvas = null;
        public static Canvas canvasToRemove = null;
        public static MainWindow main = null;

        public static void FullScreen()
        {
            main.WindowState = WindowState.Maximized;
            main.WindowStyle = WindowStyle.None;

            main.StartWindow.Width = SystemParameters.PrimaryScreenWidth;
            main.StartWindow.Height = SystemParameters.PrimaryScreenHeight;

            currentCanvas = main.StartWindow;
        }

        public static void DestroyCanvas(object sender, EventArgs e)
        {
            if (canvasToRemove != null)
                main.RootWindow.Children.Remove(canvasToRemove);
        }

        public static void MoveCanvas(Canvas moveCanvas, Canvas prevCanvas, moveDirection direction)
        {
            double left = 0;
            double top = 0;

            ThicknessAnimation move = new ThicknessAnimation();
            move.Duration = TimeSpan.FromSeconds(1);
            move.From = moveCanvas.Margin;

            move.To = new Thickness(left, top, moveCanvas.Margin.Right, moveCanvas.Margin.Bottom);

            moveCanvas.BeginAnimation(FrameworkElement.MarginProperty, move);

            switch (direction)
            {
                case moveDirection.horizontal_right:
                    left = SystemParameters.PrimaryScreenWidth * -1;
                    break;
                case moveDirection.vertical_bottom:
                    top = SystemParameters.PrimaryScreenHeight * -1;
                    break;
                case moveDirection.horizontal_left:
                    left = SystemParameters.PrimaryScreenWidth;
                    break;
                case moveDirection.vertical_top:
                    top = SystemParameters.PrimaryScreenHeight;
                    break;
            }

            move.From = prevCanvas.Margin;

            move.To = new Thickness(left, top, prevCanvas.Margin.Right, prevCanvas.Margin.Bottom);

            prevCanvas.BeginAnimation(FrameworkElement.MarginProperty, move);

            move.Completed += DestroyCanvas;
        }

        public static void Move(moveDirection direction, Brush color)
        {
            Canvas newCanvas = new Canvas();

            newCanvas.Background = color;
            newCanvas.Width = SystemParameters.PrimaryScreenWidth;
            newCanvas.Height = SystemParameters.PrimaryScreenHeight;

            switch (direction)
            {
                case moveDirection.horizontal_right:
                    newCanvas.Margin = new Thickness(SystemParameters.PrimaryScreenWidth + 5, 0, 0, 0);
                    break;
                case moveDirection.vertical_bottom:
                    newCanvas.Margin = new Thickness(0, SystemParameters.PrimaryScreenHeight + 5, 0, 0);
                    break;
                case moveDirection.horizontal_left:
                    newCanvas.Margin = new Thickness(SystemParameters.PrimaryScreenWidth * -1, 0, 0, 0);
                    break;
                case moveDirection.vertical_top:
                    newCanvas.Margin = new Thickness(0, SystemParameters.PrimaryScreenHeight * -1, 0, 0);
                    break;
            }

            main.RootWindow.Children.Add(newCanvas);

            Canvas.SetZIndex(newCanvas, 100);
            Canvas.SetZIndex(currentCanvas, 50);
            Canvas.SetZIndex(main.moveOn, 150);

            canvasToRemove = currentCanvas;

            MoveCanvas(moveCanvas: newCanvas, prevCanvas: currentCanvas, direction: direction);

            currentCanvas = newCanvas;
        }
    }
}
