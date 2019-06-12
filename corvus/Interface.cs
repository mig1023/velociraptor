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
        static Random r = new Random();

        public enum moveDirection { horizontal_left, horizontal_right, vertical_top, vertical_bottom };

        public static Canvas currentCanvas = null;
        public static Canvas canvasToRemove = null;
        public static Canvas containerToRemove = null;
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
            containerToRemove.Children.Remove(canvasToRemove);
            containerToRemove.Children.Remove(currentCanvas);
            main.RootWindow.Children.Add(currentCanvas);
            currentCanvas.Margin = new Thickness(0, 0, 0, 0);
            main.RootWindow.Children.Remove(containerToRemove);
        }

        public static void MoveCanvas(Canvas moveCanvas, Canvas prevCanvas, moveDirection direction)
        {
            double left = 0;
            double top = 0;

            ThicknessAnimation move = new ThicknessAnimation();
            move.Duration = TimeSpan.FromSeconds(0.2);
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
            double screenWidth = SystemParameters.PrimaryScreenWidth;
            double screenHeight = SystemParameters.PrimaryScreenHeight;


            /////////////////////////////
            if (containerToRemove != null)
            {
                containerToRemove.Children.Remove(canvasToRemove);
                containerToRemove.Children.Remove(currentCanvas);
                main.RootWindow.Children.Add(currentCanvas);
                currentCanvas.Margin = new Thickness(0, 0, 0, 0);
                main.RootWindow.Children.Remove(containerToRemove);
            }
            //////////////////////////////////

            Canvas newCanvas = new Canvas();
            newCanvas.Background = color;
            newCanvas.Width = screenWidth;
            newCanvas.Height = screenHeight;

            Canvas containerCanvas = new Canvas();
            main.RootWindow.Children.Add(containerCanvas);
            containerCanvas.Children.Add(newCanvas);
            main.RootWindow.Children.Remove(currentCanvas);
            containerCanvas.Children.Add(currentCanvas);

            ThicknessAnimation move = new ThicknessAnimation();
            move.Duration = TimeSpan.FromSeconds(1);

            switch (direction)
            {
                case moveDirection.horizontal_right:
                    containerCanvas.Width = screenWidth * 2;
                    containerCanvas.Height = newCanvas.Height;
                    containerCanvas.Margin = new Thickness(0, 0, 0, 0);
                    newCanvas.Margin = new Thickness(screenWidth, 0, 0, 0);
                    currentCanvas.Margin = new Thickness(0, 0, 0, 0);

                    move.From = containerCanvas.Margin;
                    move.To = new Thickness(screenWidth * -1, 0, 0, 0);
                    break;
                case moveDirection.horizontal_left:
                    containerCanvas.Width = screenWidth * 2;
                    containerCanvas.Height = newCanvas.Height;
                    containerCanvas.Margin = new Thickness(screenWidth * -1, 0, 0, 0);
                    newCanvas.Margin = new Thickness(0, 0, 0, 0);
                    currentCanvas.Margin = new Thickness(screenWidth, 0, 0, 0);

                    move.From = containerCanvas.Margin;
                    move.To = new Thickness(0, 0, 0, 0);
                    break;

                case moveDirection.vertical_bottom:
                    containerCanvas.Width = newCanvas.Width;
                    containerCanvas.Height = screenWidth * 2;
                    containerCanvas.Margin = new Thickness(0, 0, 0, 0);
                    newCanvas.Margin = new Thickness(0, screenHeight, 0, 0);
                    currentCanvas.Margin = new Thickness(0, 0, 0, 0);

                    move.From = containerCanvas.Margin;
                    move.To = new Thickness(0, screenHeight * -1, 0, 0);
                    break;

                case moveDirection.vertical_top:
                    containerCanvas.Width = newCanvas.Width;
                    containerCanvas.Height = screenHeight * 2;
                    containerCanvas.Margin = new Thickness(0, screenHeight * -1, 0, 0);
                    newCanvas.Margin = new Thickness(0, 0, 0, 0);
                    currentCanvas.Margin = new Thickness(0, screenHeight, 0, 0);

                    move.From = containerCanvas.Margin;
                    move.To = new Thickness(0, 0, 0, 0);
                    break;
            }

            //move.Completed += DestroyCanvas;

            Canvas.SetZIndex(newCanvas, 100);
            Canvas.SetZIndex(currentCanvas, 50);
            Canvas.SetZIndex(main.moveOn, 150);

            containerCanvas.BeginAnimation(FrameworkElement.MarginProperty, move);
            
            containerToRemove = containerCanvas;
            canvasToRemove = currentCanvas;
            currentCanvas = newCanvas;
        }
    }
}
