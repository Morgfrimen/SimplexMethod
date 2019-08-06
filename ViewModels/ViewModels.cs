using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Лабораторная_работа.ViewModels
{
    class ViewModels:INotifyPropertyChanged
    {
        private Window _window;
        private TextBox textBox1;
        private InkCanvas _inkCanv;
        private StackPanel stackinter;
        public ViewModels(Window win,TextBox textBox,InkCanvas inkCanvas,StackPanel stackPanel )//конструктор класса
        {
            textBox.Text = $@"C:\Users\{Environment.UserName}\Documents";
            textBox.Foreground = Brushes.Black;
            inkCanvas.EditingMode = InkCanvasEditingMode.Select;
            inkCanvas.Cursor = System.Windows.Input.Cursors.Pen;
            textBox1 = textBox;
            _inkCanv = inkCanvas;
            stackinter = stackPanel;
            _window = win;
            Models.Bin.BindingInkCanvas(_inkCanv);
            
        }

        private Models.Exit _exit;
        public Models.Exit Exit
        {
            get
            {

                return _exit ??
                  (_exit = new Models.Exit(obj =>
                  {
                      Models.Bin.ExitCommand(_window);
                  }));
            }
        }
        private Models.Minimaze _minimaze;
        public Models.Minimaze Minimaze
        {
            get
            {
                return _minimaze ??
                 (_minimaze = new Models.Minimaze(obj =>
                 {
                     Models.Bin.MinimazeCommand(_window);
                 }));
            }
        }

        private Models.OpenFileDialogs _openFile;
        public Models.OpenFileDialogs OpenFileDialog
        {
            get
            {
                return _openFile ??
                 (_openFile = new Models.OpenFileDialogs(obj =>
                 {
                     Models.Bin.OpenDialog(textBox1);
                 }));
            }
        }

        private List<Models.Generation> Listgeneraton=new List<Models.Generation>();
        public List<Models.Generation> Generations { get { return Listgeneraton; }set { Listgeneraton = value;OnPropertyChanged(""); } }

        private List<Models.Nagruzka> _listNag = new List<Models.Nagruzka>();
        public List<Models.Nagruzka> ListNag { get { return _listNag; }set { _listNag = value; OnPropertyChanged(""); } }

        private int _idGeneration;
        public int IDGeneraton { get { return _idGeneration; }set { _idGeneration = value; } }

        private int _idNag;
        public int IDNagruzka { get { return _idNag; } set { _idNag = value; } }


        private Models.PlusButton plus;
        public Models.PlusButton Plus
        {
            get
            {
                return plus ??
                 (plus = new Models.PlusButton(obj =>
                 {
                     ComboBox combo = (ComboBox)_window.FindName("ComboBoxes");
                     if (combo.SelectedIndex == 0)
                     {
                         Models.Generation generation = new Models.Generation();
                         Listgeneraton.Add(generation);
                         _idGeneration = Models.Bin.PlusGeneration(_window, _inkCanv, Listgeneraton[Listgeneraton.Count - 1], _idGeneration);
                     }
                     else
                     {
                         Models.Nagruzka Nag = new Models.Nagruzka();
                         _listNag.Add(Nag);
                         _idNag = Models.Bin.PlusNagr(_window, _inkCanv, _listNag[_listNag.Count-1], _idNag);
                     }
                     Models.Bin.InterfacePlus((StackPanel)_window.FindName("Interface"),(ViewModels)_window.DataContext);
                     

                 }));
            }
        }

        private Models.MinusButton minus;
        public Models.MinusButton Minus
        {
            get
            {
                return minus ??
                 (minus = new Models.MinusButton(obj =>
                 {
                     ComboBox combo = (ComboBox)_window.FindName("ComboBoxes");
                     if (combo.SelectedIndex == 1)
                     {
                         try
                         {
                             ListNag.RemoveAt(ListNag.Count - 1);
                             IDNagruzka=Models.Bin.MinusButton(_inkCanv);
                         }
                         catch (ArgumentOutOfRangeException)
                         {
                             MessageBox.Show("Удаление невозможно, так как такого узла нет","Предупреждение",MessageBoxButton.OK,MessageBoxImage.Exclamation);
                         }
                        
                     }
                     else
                     {
                         try
                         {
                             Generations.RemoveAt(Generations.Count - 1);
                             IDGeneraton = Models.Bin.MinusButton(_inkCanv,IDGeneraton);
                         }
                         catch (ArgumentOutOfRangeException)
                         {
                             MessageBox.Show("Удаление невозможно, так как такого узла нет", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                         }
                     }


                 }));
            }
        }









        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
