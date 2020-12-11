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

namespace mini_spotify.View
{
    /// <summary>
    /// Interaction logic for TestApp.xaml
    /// </summary>
    public partial class TestApp : Window
    {
        public TestApp()
        {
            InitializeComponent();
        }

        private void btnEnterName_Click(object sender, RoutedEventArgs e)
        {
            TestDialog inputDialog = new TestDialog("Please enter your name:", "John Doe");
            if (inputDialog.ShowDialog() == true)
                lblName.Text = inputDialog.Answer;
        }
    }
}
