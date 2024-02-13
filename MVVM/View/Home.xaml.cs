﻿using System;
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
using TicketZ.MVVM.Model;

namespace TicketZ.MVVM.View
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : UserControl
    {
        public Home()
        {
            InitializeComponent();
        }

        private void Print(object sender, RoutedEventArgs e)
        {
            if (sender is not Button) return;

            if(((Button)sender).DataContext is Shift shift)
            {
                PrintDialog dialog = new PrintDialog();
                dialog.ShowDialog();
                MessageBox.Show($"Print Ticekt {shift.Id}?", "Print", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
            }
        }
    }
}
