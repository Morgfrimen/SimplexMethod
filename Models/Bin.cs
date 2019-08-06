using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Лабораторная_работа.Models
{
    class Bin
    {
        /// <summary>
        /// Предоставляет логику приложения
        /// </summary>
        //переменные
        private Window _window;
        private static List<TextBox> _texts = new List<TextBox>();
        public static List<TextBox> Texts { get => _texts; set { _texts = value;} }
        public Bin(Window win)//конструктор класса
        {
            _window = win;
        }

       
        //команды и методы
        private static List<TextBox> StackPanelPlusInterface(TextBox textBox)
        {            
            StackPanel panel = (StackPanel)textBox.Parent;
            StackPanel stackPanel = (StackPanel)panel.Parent;
            Window window = (Window)stackPanel.Parent;
            List<TextBox> texts = new List<TextBox>();
            if (window.FindName("Osn") == null)
            {
                StackPanel Osnovnaya = new StackPanel(){Name="Osn"};
                window.RegisterName(Osnovnaya.Name, Osnovnaya);
                stackPanel.Children.Insert(stackPanel.Children.Count - 1,Osnovnaya);
            }
            ViewModels.ViewModels view = (ViewModels.ViewModels)window.DataContext;
            int integer;
            try
            {
                integer = Convert.ToInt32(view.IDGeneraton);
            }
            catch
            {
                integer = 0;
                return texts;
            }
            
            StackPanel Vertical = new StackPanel();
            Vertical.Children.RemoveRange(0, Vertical.Children.Count);
            if (integer < 10)
            {                 
                StackPanel Horizontal1 = new StackPanel() {Orientation=Orientation.Horizontal};
                Vertical.Children.Add(Horizontal1);
                for (int i = 0; i < integer; i++)
                {
                    TextBox text = new TextBox() { Margin = new Thickness(2, 2, 0, 0),
                        Width = 25,Text=$"X{i+1}",
                    Foreground=Brushes.Gray};
                    text.GotFocus += TextOgr_GotFocus;
                    text.LostFocus += TextOgr_LostFocus;
                    texts.Add(text);
                    Horizontal1.Children.Add(text);                                                        
                }
                StackPanel os = (StackPanel)window.FindName("Osn");
                os.Children.RemoveRange(0, os.Children.Count);
                os.Children.Add(Vertical);
                return texts;
            }
            else if (integer>=10 & integer<=20)
            {
                StackPanel Horizontal1 = new StackPanel() { Orientation = Orientation.Horizontal };
                Vertical.Children.Add(Horizontal1);
                for (int i = 0; i < 10; i++)
                {
                    TextBox text = new TextBox()
                    {
                        Margin = new Thickness(2, 2, 0, 0),
                        Width = 25,
                        Text = $"X{i + 1}",
                        Foreground = Brushes.Gray
                    };
                    text.GotFocus += TextOgr_GotFocus;
                    text.LostFocus += TextOgr_LostFocus;
                    texts.Add(text);
                    Horizontal1.Children.Add(text);
                }
                StackPanel Horizontal2 = new StackPanel() { Orientation = Orientation.Horizontal };
                Vertical.Children.Add(Horizontal2);
                for (int i = 10; i < integer; i++)
                {
                    TextBox text = new TextBox()
                    {
                        Margin = new Thickness(2, 2, 0, 0),
                        Width = 25,
                        Text = $"X{i + 1}",
                        Foreground = Brushes.Gray
                    };
                    text.GotFocus += TextOgr_GotFocus;
                    text.LostFocus += TextOgr_LostFocus;
                    texts.Add(text);
                    Horizontal2.Children.Add(text);
                }
                StackPanel os = (StackPanel)window.FindName("Osn");
                os.Children.RemoveRange(0, os.Children.Count);
                os.Children.Add(Vertical);
                return texts;
            }
            else
            {
                MessageBox.Show(messageBoxText: "Больше 20 ограничений ввести нельзя.",caption:"Уведомление",button:
                    MessageBoxButton.OK,icon:MessageBoxImage.Exclamation);
                textBox.Text = "0";
                
            }
            return texts;

        }
        public static void ExitCommand(Window window)
        {
            window.Close();
        }
        public static void MinimazeCommand(Window window)
        {
            window.WindowState = WindowState.Minimized;
        }
        public static void OpenDialog(TextBox textBox)
        {


            OpenFileDialog myDialog = new OpenFileDialog();
            myDialog.Filter = "Текстовой документ(*.docx;*.txt)|*.docx;*.txt" + "|Все файлы (*.*)|*.* ";
            myDialog.CheckFileExists = true;
            myDialog.Multiselect = true;
            if (myDialog.ShowDialog() == true)
            {
                textBox.Text = myDialog.FileName;
            }
        }
        public static int PlusGeneration(Window win,InkCanvas canvas, Models.Generation generation,int ID)
        {
            ID+=1;
            if (ID > 20)
            {
                MessageBox.Show("Достигнут максимум генераторых узлов", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return 19;

            }
            else
            {
                generation.ID = ID;

                StackPanel panel = new StackPanel();
                panel.Name = $"Г{ID}";
                win.RegisterName(panel.Name, panel);
                Ellipse ellipse = new Ellipse();
                ellipse.Fill = Brushes.Red;
                ellipse.Width = 25;
                ellipse.Height = 25;
                TextBlock textBlock = new TextBlock();
                textBlock.Text = $"Г-{ID}";
                textBlock.TextAlignment = TextAlignment.Center;

                Popup popup = new Popup();

                StackPanel dockPanel = new StackPanel();
                dockPanel.Background = Brushes.White;
                dockPanel.Orientation = Orientation.Horizontal;
                StackPanel inform = new StackPanel();
                inform.Width = 75;
                inform.Margin = new Thickness(0);
                Border border1 = new Border();
                border1.BorderThickness = new Thickness(0, 0, 0, 1);
                border1.BorderBrush = Brushes.Black;
                TextBlock P = new TextBlock();
                P.FontSize = 14;
                P.FontFamily = new FontFamily("Times New Romans");
                P.Text = "Pнб,[МВт]:";
                P.Margin = new Thickness(0, 0, 0, 1);
                border1.Child = P;
                Border border2 = new Border();
                border2.BorderThickness = new Thickness(0, 0, 0, 1);
                border2.BorderBrush = Brushes.Black;
                TextBlock K = new TextBlock();
                K.FontSize = 14;
                K.FontFamily = new FontFamily("Times New Romans");
                K.Text = "K,[тр/кВт]:";
                K.Margin = new Thickness(0, 0, 0, 1);
                border2.Child = K;
                Border border3 = new Border();
                border3.BorderThickness = new Thickness(0, 0, 0, 1);
                border3.BorderBrush = Brushes.Black;
                TextBlock T = new TextBlock();
                T.FontSize = 14;
                T.FontFamily = new FontFamily("Times New Romans");
                T.Text = "Tнб,[ч]:";
                T.Margin = new Thickness(0);
                border3.Child = T;
                Border border4 = new Border();
                border4.BorderThickness = new Thickness(0, 0, 0, 1);
                border4.BorderBrush = Brushes.Black;
                TextBlock Y = new TextBlock();
                Y.FontSize = 14;
                Y.FontFamily = new FontFamily("Times New Romans");
                Y.Text = "γ,[р/кВт⋅ч]:";
                Y.Margin = new Thickness(0, 0, 0, 0);
                border4.Child = Y;
                inform.Children.Add(border1);
                inform.Children.Add(border2);
                inform.Children.Add(border3);
                inform.Children.Add(border4);
                dockPanel.Children.Add(inform);


                StackPanel vvod = new StackPanel();
                vvod.Width = 50;
                vvod.Margin = new Thickness(0);
                vvod.Background = Brushes.White;

                TextBox PP = new TextBox();
                PP.FontSize = 14;
                PP.FontFamily = new FontFamily("Times New Romans");
                PP.BorderThickness = new Thickness(0, 0, 0, 1.2);
                PP.BorderBrush = Brushes.Black;
                PP.Margin = new Thickness(0, 0.5, 0, 0.95);
                BindingsElements(PP, generation, "P");

                TextBox KK = new TextBox();
                KK.FontSize = 14;
                KK.FontFamily = new FontFamily("Times New Romans");
                KK.BorderThickness = new Thickness(0, 0, 0, 1.2);
                KK.BorderBrush = Brushes.Black;
                KK.Margin = new Thickness(0, 0.15, 0, 0);
                BindingsElements(KK, generation, "K");

                TextBox TT = new TextBox();
                TT.FontSize = 14;
                TT.FontFamily = new FontFamily("Times New Romans");
                TT.BorderThickness = new Thickness(0, 0, 0, 1.2);
                TT.BorderBrush = Brushes.Black;
                TT.Margin = new Thickness(0, 0.05, 0, 0);
                BindingsElements(TT, generation, "T");

                TextBox YY = new TextBox();
                YY.FontSize = 14;
                YY.FontFamily = new FontFamily("Times New Romans");
                YY.BorderThickness = new Thickness(0, 0, 0, 1.2);
                YY.BorderBrush = Brushes.Black;
                YY.Margin = new Thickness(0, 0.25, 0, 0);
                BindingsElements(YY, generation, "Y");

                vvod.Children.Add(PP);
                vvod.Children.Add(KK);
                vvod.Children.Add(TT);
                vvod.Children.Add(YY);
                dockPanel.Children.Add(vvod);

                Border border = new Border();
                border.BorderThickness = new Thickness(1);
                border.BorderBrush = Brushes.Black;
                border.Child = dockPanel;
                popup.Child = border;
                popup.PopupAnimation = PopupAnimation.Slide;

                popup.Placement = PlacementMode.MousePoint;
                popup.HorizontalOffset = -15;
                popup.VerticalOffset = -10;
                popup.PopupAnimation = PopupAnimation.Slide;
                popup.Name = $"PoPG{generation.ID}";
                win.RegisterName(popup.Name, popup);
                panel.Children.Add(ellipse);
                panel.Children.Add(textBlock);


                panel.ToolTip = popup;
                panel.MouseRightButtonDown += Panel_MouseRightButtonDown;


                canvas.Children.Add(panel);
                InkCanvas.SetLeft(panel, ellipse.Width * (ID - 1));//пасхалочку можно сделать
                canvas.EditingMode = InkCanvasEditingMode.None;

                popup.MouseLeave += Panel_MouseLeave;

                win.Closed += Win_Closed;
                InterfcePluss(win);
                return ID;
            }

        }
        public static int PlusNagr(Window win, InkCanvas canvas, Models.Nagruzka generation, int ID)
        {
            ID += 1;
            if (ID > 1)
            {
                MessageBox.Show("Нагрузочный узел может быть только один","Предупреждение",MessageBoxButton.OK,MessageBoxImage.Exclamation);
                return 1;

            }
            else
            {
                generation.ID = ID;

                StackPanel panel = new StackPanel();
                panel.Name = $"Н{generation.ID}";
                win.RegisterName(panel.Name, panel);
                Ellipse ellipse = new Ellipse();
                ellipse.Fill = Brushes.Blue;
                ellipse.Width = 25;
                ellipse.Height = 25;
                TextBlock textBlock = new TextBlock();
                textBlock.Text = $"Н-{ID}";
                textBlock.TextAlignment = TextAlignment.Center;

                Popup popup = new Popup();

                StackPanel dockPanel = new StackPanel();
                dockPanel.Background = Brushes.White;
                dockPanel.Orientation = Orientation.Horizontal;
                StackPanel inform = new StackPanel();
                inform.Width = 75;
                inform.Margin = new Thickness(0);
                Border border1 = new Border();
                border1.BorderThickness = new Thickness(0, 0, 0, 1);
                border1.BorderBrush = Brushes.Black;
                TextBlock P = new TextBlock();
                P.FontSize = 14;
                P.FontFamily = new FontFamily("Times New Romans");
                P.Text = "Pнб,[МВт]:";
                P.Margin = new Thickness(0, 0, 0, 1);
                border1.Child = P;

                Border border3 = new Border();
                border3.BorderThickness = new Thickness(0, 0, 0, 1);
                border3.BorderBrush = Brushes.Black;
                TextBlock T = new TextBlock();
                T.FontSize = 14;
                T.FontFamily = new FontFamily("Times New Romans");
                T.Text = "Tнб,[ч]:";
                T.Margin = new Thickness(0);
                border3.Child = T;
                inform.Children.Add(border1);
                inform.Children.Add(border3);

                dockPanel.Children.Add(inform);


                StackPanel vvod = new StackPanel();
                vvod.Width = 50;
                vvod.Margin = new Thickness(0);
                vvod.Background = Brushes.White;

                TextBox PP = new TextBox();
                PP.FontSize = 14;
                PP.FontFamily = new FontFamily("Times New Romans");
                PP.BorderThickness = new Thickness(0, 0, 0, 1.2);
                PP.BorderBrush = Brushes.Black;
                PP.Margin = new Thickness(0, 0.5, 0, 0.95);
                BindingsElements(PP, generation, "P");

                TextBox TT = new TextBox();
                TT.FontSize = 14;
                TT.FontFamily = new FontFamily("Times New Romans");
                TT.BorderThickness = new Thickness(0, 0, 0, 1.2);
                TT.BorderBrush = Brushes.Black;
                TT.Margin = new Thickness(0, 0.05, 0, 0);
                BindingsElements(TT, generation, "T");

                vvod.Children.Add(PP);
                vvod.Children.Add(TT);
                dockPanel.Children.Add(vvod);

                Border border = new Border();
                border.BorderThickness = new Thickness(1);
                border.BorderBrush = Brushes.Black;
                border.Child = dockPanel;
                popup.Child = border;
                popup.PopupAnimation = PopupAnimation.Slide;

                popup.Placement = PlacementMode.MousePoint;
                popup.HorizontalOffset = -15;
                popup.VerticalOffset = -10;
                popup.PopupAnimation = PopupAnimation.Slide;
                popup.Name = $"PoPN{generation.ID}";
                win.RegisterName(popup.Name, popup);
                panel.Children.Add(ellipse);
                panel.Children.Add(textBlock);


                panel.ToolTip = popup;
                panel.MouseRightButtonDown += Panel_MouseRightButtonDown;

                canvas.Children.Add(panel);
                InkCanvas.SetLeft(panel, win.Width-ellipse.Width*2);
                canvas.EditingMode = InkCanvasEditingMode.None;

                popup.MouseLeave += Panel_MouseLeave;

                win.Closed += Win_Closed;
                
                return ID;
            }
        }
        public static int MinusButton(InkCanvas inkCanvas,int ID=0)
        {
            if (ID == 0)//нагрузка
            {
                
                Window window = (Window)((StackPanel)((Border)inkCanvas.Parent).Parent).Parent;
                StackPanel panel = (StackPanel)window.FindName("Н1");
                window.UnregisterName(((Popup)panel.ToolTip).Name);
                window.UnregisterName(panel.Name);
                inkCanvas.Children.Remove(panel);
                return 0;

            }
            else
            {
                Window window = (Window)((StackPanel)((Border)inkCanvas.Parent).Parent).Parent;
                StackPanel panel = (StackPanel)window.FindName($"Г{ID}");
                window.UnregisterName(((Popup)panel.ToolTip).Name);
                window.UnregisterName(panel.Name);
                inkCanvas.Children.Remove(panel);
                ViewModels.ViewModels view = (ViewModels.ViewModels)window.DataContext;
                view.IDGeneraton = ID-1;
                InterfcePluss(window,2);
                view.IDGeneraton += 1;
                return ID-1;
            }
        }
        public static void BindingInkCanvas(InkCanvas inkCanvas)
        {
            inkCanvas.SizeChanged += InkCanvas_SizeChanged;
            inkCanvas.MouseLeftButtonDown += InkCanvas_StackPanel_MouseLeftButtonDown;
            inkCanvas.MouseLeave += InkCanvas_MouseLeave;
            inkCanvas.ActiveEditingModeChanged += InkCanvas_ActiveEditingModeChanged;
            
        }
        public static void InterfacePlus(StackPanel StackInterface,ViewModels.ViewModels view)
        {
            Window window = (Window)((StackPanel)StackInterface.Parent).Parent;
            if ((DataGrid)window.FindName("Generations") == null & (DataGrid)window.FindName("Nagruzka") == null)
            {
                StackInterface.Orientation = Orientation.Horizontal;
                Binding BindingG = new Binding();
                BindingG.Source = view;
                BindingG.Path = new PropertyPath("Generations");
                BindingG.Mode = BindingMode.TwoWay;

                DataGrid GridGeneration = new DataGrid();
                GridGeneration.Name = "Generation";
                window.RegisterName(GridGeneration.Name, GridGeneration);
                GridGeneration.SetBinding(DataGrid.ItemsSourceProperty, BindingG);
                GridGeneration.MaxHeight = 120;


                Binding BindingN = new Binding();
                BindingN.Source = view;
                BindingN.Path = new PropertyPath("ListNag");
                BindingN.Mode = BindingMode.TwoWay;

                DataGrid GridNagruzka = new DataGrid();
                GridNagruzka.Name = "Nagruzka";
                window.RegisterName(GridNagruzka.Name, GridNagruzka);
                GridNagruzka.SetBinding(DataGrid.ItemsSourceProperty, BindingN);
                GridNagruzka.Margin = new Thickness(10, 0, 0, 0);

                StackInterface.Children.Add(GridGeneration);
                
                StackInterface.Children.Add(GridNagruzka);

            }
            
            
        }
        private static void InterfcePluss(Window window, int number = 0)
        {
            if (number == 0)
            {
                if ((StackPanel)window.FindName("PanelPlus") == null)
                {
                    StackPanel stack = (StackPanel)window.Content;
                    StackPanel panel = new StackPanel();
                    panel.Name = "PanelPlus";
                    panel.Orientation = Orientation.Horizontal;
                    TextBlock textBlock = new TextBlock();
                    ViewModels.ViewModels view = (ViewModels.ViewModels)window.DataContext;
                    textBlock.Text = $"Количество Х: {view.IDGeneraton + 1}";
                    panel.Children.Add(textBlock);
                    window.RegisterName(panel.Name, panel);
                    TextBlock textBlock1 = new TextBlock();
                    textBlock1.Text = "; Количество уравнений ограничений: ";
                    TextBox textBox = new TextBox();
                    textBox.Text = "0";
                    textBox.Width = 30;
                    textBox.Name = "Ogr";
                    //textBox.TextChanged += TextBoxInStackPanel_TextChanged;
                    window.RegisterName(textBox.Name, textBox);
                    panel.Children.Add(textBlock1);
                    panel.Children.Add(textBox);
                    stack.Children.Insert(stack.Children.Count - 1, panel);

                }
                else
                {
                    StackPanel stack = (StackPanel)window.FindName("PanelPlus");
                    TextBlock textBlock = (TextBlock)stack.Children[0];
                    ViewModels.ViewModels view = (ViewModels.ViewModels)window.DataContext;
                    textBlock.Text = $"Количество Х: {view.IDGeneraton + 1}";
                }

            }
            else
            {
                if ((StackPanel)window.FindName("PanelPlus") == null)
                {
                    StackPanel stack = (StackPanel)window.Content;
                    StackPanel panel = new StackPanel();
                    panel.Name = "PanelPlus";
                    panel.Orientation = Orientation.Horizontal;
                    TextBlock textBlock = new TextBlock();
                    ViewModels.ViewModels view = (ViewModels.ViewModels)window.DataContext;
                    TextBox block = new TextBox();
                    block.Text = $"{view.IDGeneraton + 1}";
                    block.TextChanged += TextBoxInStackPanel_TextChanged;
                    textBlock.Text = $"Количество Х: {block.Text}";
                    panel.Children.Add(textBlock);
                    panel.Children.Add(block);//тут траблы
                    window.RegisterName(panel.Name, panel);

                    stack.Children.Insert(stack.Children.Count - 1, panel);

                }
                else
                {
                    StackPanel stack = (StackPanel)window.FindName("PanelPlus");
                    TextBlock textBlock = (TextBlock)stack.Children[0];
                    ViewModels.ViewModels view = (ViewModels.ViewModels)window.DataContext;
                    textBlock.Text = $"Количество Х: {view.IDGeneraton}";
                }
            }

        }
        private static void BindingsElements(TextBox TextBoxes, Generation generation, string nameproperty)
        {
            Binding binding = new Binding();
            binding.Source = generation;
            binding.Path = new PropertyPath(nameproperty);
            binding.TargetNullValue = "0";
            TextBoxes.SetBinding(TextBox.TextProperty, binding);

        }
        private static void BindingsElements(TextBox TextBoxes, Nagruzka nagruzka, string nameproperty)
        {
            Binding binding = new Binding();
            binding.Source = nagruzka;
            binding.Path = new PropertyPath(nameproperty);
            binding.TargetNullValue = "0";
            TextBoxes.SetBinding(TextBox.TextProperty, binding);

        }






        //События
        private static void TextOgr_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox text = (TextBox)sender;
            try
            {
                Convert.ToDouble(text.Text);
            }
            catch
            {
                StackPanel panel = (StackPanel)text.Parent;
                text.Text = $"X{panel.Children.IndexOf(text)+1}";
                text.Foreground = Brushes.Gray;
            }
        }
        private static void TextOgr_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox text = (TextBox)sender;
            text.Foreground = Brushes.Black;
            text.Text = "";
        }
        private static void TextBoxInStackPanel_TextChanged(object sender, TextChangedEventArgs e)
        {
            Models.Bin.Texts=StackPanelPlusInterface((TextBox)sender);
        }
        private static void InkCanvas_ActiveEditingModeChanged(object sender, RoutedEventArgs e)
        {
            InkCanvas inkCanvas = (InkCanvas)sender;
            if (inkCanvas.EditingMode == InkCanvasEditingMode.Select)
            {
                foreach (var item in inkCanvas.Children)
                {
                    if (item is Polyline)
                    {
                        inkCanvas.Children.Remove((Polyline)item);
                        Window window= (Window)((StackPanel)((Border)((InkCanvas)sender).Parent).Parent).Parent;
                        window.UnregisterName("Линия");
                        break;
                    }
                }
            }

        }
        private static void InkCanvas_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {

            InkCanvas inkCanvas= (InkCanvas)sender;
            inkCanvas.EditingMode = InkCanvasEditingMode.None;
            Window window = (Window)((StackPanel)((Border)((InkCanvas)sender).Parent).Parent).Parent;
            if (window.FindName("Линия") == null & window.FindName("Н1")!=null & window.FindName("Г1") != null)
            {
                Polyline polyline = new Polyline();
                polyline.Stroke = Brushes.Black;
                polyline.StrokeThickness = 1;
                polyline.Name = "Линия";
                window.RegisterName(polyline.Name, polyline);
                Point nagruzka = ((StackPanel)window.FindName("Н1")).TransformToVisual(inkCanvas).Transform(new Point());
                nagruzka.X += ((Ellipse)((StackPanel)window.FindName("Н1")).Children[0]).Width/2;
                nagruzka.Y+= ((Ellipse)((StackPanel)window.FindName("Н1")).Children[0]).Height / 2;
                polyline.Points.Add(nagruzka);
                foreach (var item in inkCanvas.Children)
                {
                    if (item is StackPanel)
                    {
                        if (((StackPanel)item).Name[0] == 'Г')
                        {
                            StackPanel panel = (StackPanel)item;
                            Ellipse ellipse = (Ellipse)panel.Children[0];
                            Point point = (ellipse.TransformToVisual(inkCanvas).Transform(new Point()));
                            point.X += ellipse.Width / 2;
                            point.Y += ellipse.Height / 2;
                            polyline.Points.Add(point);
                            polyline.Points.Add(nagruzka);
                        }
                    }

                }
                inkCanvas.Children.Add(polyline);
            }
            else if(window.FindName("Н1") != null & window.FindName("Г1") != null)
            {
                Polyline polyline=(Polyline)window.FindName("Линия");
                polyline.Points.Clear();
                Point nagruzka = ((StackPanel)window.FindName("Н1")).TransformToVisual(inkCanvas).Transform(new Point());
                nagruzka.X += ((Ellipse)((StackPanel)window.FindName("Н1")).Children[0]).Width / 2;
                nagruzka.Y += ((Ellipse)((StackPanel)window.FindName("Н1")).Children[0]).Height / 2;

                foreach (var item in inkCanvas.Children)
                {
                    if (item is StackPanel)
                    {
                        if (((StackPanel)item).Name[0] == 'Г')
                        {
                            StackPanel panel = (StackPanel)item;
                            Ellipse ellipse = (Ellipse)panel.Children[0];
                            Point point = (ellipse.TransformToVisual(inkCanvas).Transform(new Point()));
                            point.X += ellipse.Width/2;
                            point.Y += ellipse.Height/2;
                            polyline.Points.Add(point);
                            polyline.Points.Add(nagruzka);
                        }
                    }

                }
            }
        }
        private static void InkCanvas_StackPanel_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if(sender is InkCanvas)
            {
                ((InkCanvas)sender).EditingMode = InkCanvasEditingMode.Select;
            }
            else if(sender is StackPanel)
            {
                ((InkCanvas)((StackPanel)sender).Parent).EditingMode = InkCanvasEditingMode.Select;
            }
        }
        private static void InkCanvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            InkCanvas inkCanvas = (InkCanvas)sender;
            int Chet=0;
            if (inkCanvas.Children != null)
            {
                foreach (var item in inkCanvas.Children)
                {
                    if(item is StackPanel)
                    {
                        try
                        {
                            StackPanel stack = (StackPanel)item;
                            Point point = new Point();
                            point = stack.TransformToVisual(inkCanvas).Transform(new Point());
                            if (point.X >= (inkCanvas.Width * 3 / 4))
                            {
                                Chet += 1;
                                if (Chet > 1)
                                {
                                    InkCanvas.SetLeft(stack, inkCanvas.Width - 75 - 25 * Chet);
                                    InkCanvas.SetBottom(stack, 50);

                                }
                                else
                                {
                                    InkCanvas.SetLeft(stack, inkCanvas.Width - 75);
                                }

                            }
                        }
                        catch (TypeAccessException)
                        {
                            MessageBox.Show("Ошибка приведения");
                        }
                    }
                    else
                    {
                        ((Polyline)item).Points.Clear();
                    }
                }
            }
            Chet = 0;
           
        }
        private static void Win_Closed(object sender, EventArgs e)
        {
            ViewModels.ViewModels view = (ViewModels.ViewModels)((Window)sender).DataContext;
            for (int i = 1; i <= view.IDGeneraton; i++)
            {
                Popup popup = (Popup)((Window)sender).FindName($"PoPG{i}");
                popup.IsOpen = false;
            }
            for (int i = 1; i <= view.IDNagruzka; i++)
            {
                Popup popup = (Popup)((Window)sender).FindName($"PoPN{i}");
                popup.IsOpen = false;
            }
        }
        private static void Panel_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            try
            {
                
                Popup popup = (Popup)sender;
                popup.IsOpen = false;
            }
            catch
            {
                MessageBox.Show("Неизвестная ошибка, попробуйте ещё раз");
            }
        }
        private static void Panel_MouseRightButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            StackPanel panel = (StackPanel)sender;
            Popup popup = (Popup)panel.ToolTip;
            popup.IsOpen = true;
            popup.Focus();

        }
    }
}
