using System;
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

        const int MAX_BUTTONS_IN_LINE = 3;
        const int BORDER = 100;
        const int BUTTON_PADDING = 10;

        public enum moveDirection { horizontal_left, horizontal_right, vertical_top, vertical_bottom };

        public static Canvas currentCanvas = null;
        public static Canvas canvasToRemove = null;
        public static Canvas containerToRemove = null;
        public static Screen main = null;

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
            canvas.Background = page.BackColor;

            double buttonsInLine = (double)page.ButtonsNames.Count / MAX_BUTTONS_IN_LINE;
            int buttonsLine = (int)Math.Ceiling(buttonsInLine);

            Grid DynamicGrid = new Grid();
            DynamicGrid.Width = canvas.Width - (BORDER * 2);
            DynamicGrid.Margin = new Thickness(BORDER, BORDER, 0, 0);
            DynamicGrid.HorizontalAlignment = HorizontalAlignment.Left;
            DynamicGrid.VerticalAlignment = VerticalAlignment.Top;

            ColumnDefinition gridCol1 = new ColumnDefinition();
            DynamicGrid.ColumnDefinitions.Add(gridCol1);

            RowDefinition gridRow1 = new RowDefinition();
            DynamicGrid.RowDefinitions.Add(gridRow1);

            RowDefinition gridRow2 = new RowDefinition();
            DynamicGrid.RowDefinitions.Add(gridRow2);

            for (int a = 0; a < buttonsLine; a += 1)
            {
                RowDefinition gridRow3 = new RowDefinition();
                DynamicGrid.RowDefinitions.Add(gridRow3);
            }

            Label title = new Label();
            title.Content = page.Title;
            title.FontSize = 100;
            title.Foreground = Brushes.White;
            title.Margin = new Thickness(-15, 0, 0, 0);

            canvas.Children.Add(DynamicGrid);

            Grid.SetRow(title, 0);
            Grid.SetColumn(title, 0);

            DynamicGrid.Children.Add(title);

            TextBlock text = new TextBlock();
            text.Text = page.MainText;
            text.Width = canvas.Width - (BORDER * 2);
            text.TextWrapping = TextWrapping.Wrap;
            text.FontSize = 25;
            text.Foreground = Brushes.White;

            Grid.SetRow(text, 1);
            Grid.SetColumn(text, 0);

            DynamicGrid.Children.Add(text);

            int currentButton = 0;

            for (int a = 0; a < buttonsLine; a++)
            {
                StackPanel buttonPlace = new StackPanel();
                buttonPlace.Width = DynamicGrid.Width;
                buttonPlace.Height = 85;
                buttonPlace.Orientation = Orientation.Horizontal;

                Grid.SetRow(buttonPlace, 2 + a);
                Grid.SetColumn(buttonPlace, 0);

                DynamicGrid.Children.Add(buttonPlace);

                int position = 0;

                int buttonInThisLine = currentButton;

                int maxButtonInThisLine = page.ButtonsNames.Count - currentButton;

                if (maxButtonInThisLine > MAX_BUTTONS_IN_LINE)
                    maxButtonInThisLine = MAX_BUTTONS_IN_LINE;

                double buttonWidth = ((canvas.Width - (BORDER * 2)) / maxButtonInThisLine) - BUTTON_PADDING;

                if (page.ButtonsNames.Count == 1)
                    buttonWidth = ((canvas.Width - (BORDER * 2)) / 2) - BUTTON_PADDING;

                for (int b = buttonInThisLine; b < (buttonInThisLine + maxButtonInThisLine); b++)
                {
                    currentButton += 1;

                    Button but = new Button();
                    but.Content = page.ButtonsNames[b];
                    but.Tag = page.ButtonsGoto[b];
                    but.Click += main.moveOn_Click;
                    but.Width = buttonWidth;
                    but.Height = 70;
                    but.FontSize = 20;

                    but.Margin = new Thickness(0, 15, 15, 0);

                    buttonPlace.Children.Add(but);

                    position += (int)but.Width + BUTTON_PADDING;
                }
            }
        }

        public static void Move(string page, moveDirection direction)
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
            newCanvas.Width = screenWidth;
            newCanvas.Height = screenHeight;

            PageContent(Pages.FindPageByIndex(page), ref newCanvas);

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
