using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace corvus
{
    class Interface
    {
        public enum moveDirection { horizontal, vertical };

        public static void FullScreen(MainWindow main)
        {
            main.WindowState = WindowState.Maximized;
            main.WindowStyle = WindowStyle.None;

            foreach (Canvas canvas in new List<Canvas> { main.white, main.red, main.blue, main.green, main.gray })
            {
                canvas.Width = SystemParameters.PrimaryScreenWidth;
                canvas.Height = SystemParameters.PrimaryScreenHeight;
            }

            main.red.Margin = new Thickness(SystemParameters.PrimaryScreenWidth + 5, 0, 0, 0);
            main.blue.Margin = new Thickness(0, SystemParameters.PrimaryScreenHeight + 5, 0, 0);
            main.green.Margin = new Thickness(0, SystemParameters.PrimaryScreenHeight * -1, 0, 0);
            main.gray.Margin = new Thickness(SystemParameters.PrimaryScreenWidth * -1, 0, 0, 0);
        }

        public static void Move(Canvas moveCanvas, Canvas prevCanvas, moveDirection direction = moveDirection.horizontal)
        {
            double left = (direction == moveDirection.horizontal ? 0 : moveCanvas.Margin.Left);
            double top = (direction == moveDirection.vertical ? 0 : moveCanvas.Margin.Top);

            ThicknessAnimation move = new ThicknessAnimation();
            move.Duration = TimeSpan.FromSeconds(0.4);
            move.From = moveCanvas.Margin;

            move.To = new Thickness(left, top, moveCanvas.Margin.Right, moveCanvas.Margin.Bottom);

            moveCanvas.BeginAnimation(FrameworkElement.MarginProperty, move);

            left = (direction == moveDirection.horizontal ?
                prevCanvas.Margin.Left - moveCanvas.Margin.Left : prevCanvas.Margin.Left);
            top = (direction == moveDirection.vertical ?
                prevCanvas.Margin.Top - moveCanvas.Margin.Top : prevCanvas.Margin.Top);

            move.From = prevCanvas.Margin;

            move.To = new Thickness(left, top, prevCanvas.Margin.Right, prevCanvas.Margin.Bottom);

            prevCanvas.BeginAnimation(FrameworkElement.MarginProperty, move);
        }
    }
}
