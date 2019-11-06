using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Album_Manager.View
{
    /// <summary>
    /// Interaction logic for EditCollectionWindow.xaml
    /// </summary>
    public partial class EditCollectionWindow : Window
    {
        public EditCollectionWindow()
        {
            InitializeComponent();
        }

        private void backHomeButton_Click(object sender, RoutedEventArgs e)
        {
            StartWindow startWindow = new StartWindow();
            startWindow.Show();
            this.Close();
        }
    }
}
