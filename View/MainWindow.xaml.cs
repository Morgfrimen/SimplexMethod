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

namespace Лабораторная_работа
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// полностью уйти от событий не получается, поэтому некотрый код здесь будет
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new ViewModels.ViewModels(this.windows,Путь,InkCanv,Interface);
        }

        private void DockPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) //отошел от MVVM так как как подписать на события другим способом я не знаю
        {
            windows.DragMove();
        }


    }
}
