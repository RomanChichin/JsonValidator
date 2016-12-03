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

namespace JsonValidator_v2._0
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ParseJSON(object sender, TextChangedEventArgs e)
        {
            if (textbox.Text == null || textbox.Text == string.Empty)
            {
                layoutGrid.Background = new SolidColorBrush(Colors.White);
                textblock.Background = new SolidColorBrush(Colors.White);
                textblock.Text = "Input JSON to check it.";

            }
            else if (JsonAnalyzer.JsonAnalyzer.IsValidJson(textbox.Text))
            {
                layoutGrid.Background = new SolidColorBrush(Colors.LightGreen);
                textblock.Background = new SolidColorBrush(Colors.LightGreen);
                textblock.Text = "Success! It`s correct.";
            }
            else
            {
                layoutGrid.Background = new SolidColorBrush(Colors.IndianRed);
                textblock.Background = new SolidColorBrush(Colors.IndianRed);
                textblock.Text = "There is a mistake here.";
            }
        }
    }
}
