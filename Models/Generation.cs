using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Лабораторная_работа.Models
{
    class Generation: INotifyPropertyChanged
    {
        private int _id;
        public int ID { get => _id; set { _id = value;OnPropertyChanged("ID"); } }

        private double _p;
        public double P { get=>_p; set
            {
                if (value >= 0)
                {
                    _p = value;
                }
                else
                {
                    MessageBox.Show("Значение должно быть больше 0", "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    _p = 0;
                }
                OnPropertyChanged("Pmax");
            }}

        private double _k;
        public double K { get=>_k;
            set
            {
                if (value >= 0)
                {
                    _k = value;
                }
                else
                {
                    MessageBox.Show("Значение должно быть больше 0", "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    _k = 0;
                }
                OnPropertyChanged("K");
            } }

        private double _t;
        public double T
        {
            get => _t;
            set
            {
                if (value >= 0)
                {
                    _t = value;
                }
                else
                {
                    MessageBox.Show("Значение должно быть больше 0", "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    _t = 0;
                }
                OnPropertyChanged("Tnb");
            }
        }

        private double _y;
        public double Y
        {
            get => _y;
            set
            {
                if (value >= 0)
                {
                    _y = value;
                }
                else
                {
                    MessageBox.Show("Значение должно быть больше 0", "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    _y = 0;
                }
                OnPropertyChanged("Y");
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }


    }
}
