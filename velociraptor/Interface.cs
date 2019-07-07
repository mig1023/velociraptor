﻿using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace velociraptor
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

        static void PageContent(Pages page, ref Canvas canvas)
        {
            Label title = new Label();
            title.Content = page.Title;
            title.FontSize = 100;
            title.Foreground = Brushes.White;
            title.Margin = new Thickness(100, 100, 0, 0);

            canvas.Children.Add(title);

            Label text = new Label();
            text.Content = page.MainText;
            text.FontSize = 25;
            text.Foreground = Brushes.White;
            text.Margin = new Thickness(100, 230, 0, 0);

            canvas.Children.Add(text);

            int position = 0;
            int el_number = r.Next(1, 8);

            for (int a = 0; a < page.ButtonsNames.Count; a++)
            {
                Button but = new Button();
                but.Content = page.ButtonsNames[a];
                but.Tag = page.ButtonsGoto[a];
                but.Click += main.moveOn_Click;
                but.Width = 150;
                but.Height = 50;

                but.Margin = new Thickness(100 + position, 280, 0, 0);

                canvas.Children.Add(but);

                position += (int)but.Width + 10;
            }

        }

        public static void Move(int n, moveDirection direction, Brush color)
        {
            double screenWidth = SystemParameters.PrimaryScreenWidth;
            double screenHeight = SystemParameters.PrimaryScreenHeight;
            Thickness zeroPosition = new Thickness(0, 0, 0, 0);

            if (containerToRemove != null)
            {
                containerToRemove.Children.Remove(canvasToRemove);
                containerToRemove.Children.Remove(currentCanvas);
                main.RootWindow.Children.Add(currentCanvas);
                currentCanvas.Margin = zeroPosition;
                main.RootWindow.Children.Remove(containerToRemove);
            }

            Canvas newCanvas = new Canvas();
            newCanvas.Background = color;
            newCanvas.Width = screenWidth;
            newCanvas.Height = screenHeight;

            PageContent(Pages.FindPageByIndex(n), ref newCanvas);

            Canvas containerCanvas = new Canvas();
            main.RootWindow.Children.Add(containerCanvas);
            containerCanvas.Children.Add(newCanvas);
            main.RootWindow.Children.Remove(currentCanvas);
            containerCanvas.Children.Add(currentCanvas);

            ThicknessAnimation move = new ThicknessAnimation();
            move.Duration = TimeSpan.FromSeconds(1);

            if ((direction == moveDirection.horizontal_right) || (direction == moveDirection.horizontal_left))
            {
                containerCanvas.Width = screenWidth * 2;
                containerCanvas.Height = newCanvas.Height;

                if (direction == moveDirection.horizontal_right)
                {
                    containerCanvas.Margin = zeroPosition;
                    newCanvas.Margin = new Thickness(screenWidth, 0, 0, 0);
                    currentCanvas.Margin = zeroPosition;
                    move.To = new Thickness(screenWidth * -1, 0, 0, 0);
                }
                else
                {
                    containerCanvas.Margin = new Thickness(screenWidth * -1, 0, 0, 0);
                    newCanvas.Margin = zeroPosition;
                    currentCanvas.Margin = new Thickness(screenWidth, 0, 0, 0);
                    move.To = zeroPosition;
                }
            }
            else
            {
                containerCanvas.Width = newCanvas.Width;
                containerCanvas.Height = screenHeight * 2;

                if (direction == moveDirection.vertical_top)
                {
                    containerCanvas.Margin = zeroPosition;
                    newCanvas.Margin = new Thickness(0, screenHeight, 0, 0);
                    currentCanvas.Margin = zeroPosition;
                    move.To = new Thickness(0, screenHeight * -1, 0, 0);
                }
                else
                {
                    containerCanvas.Margin = new Thickness(0, screenHeight * -1, 0, 0);
                    newCanvas.Margin = zeroPosition;
                    currentCanvas.Margin = new Thickness(0, screenHeight, 0, 0);
                    move.To = zeroPosition;
                }
            }

            move.From = containerCanvas.Margin;

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
