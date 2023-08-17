using System;
using System.Collections;
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

namespace WpfApp1
{
    /// <summary>
    /// MainWindow.xaml etkileşim mantığı
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
            
            
        }

        private void newButton_Click(object sender, RoutedEventArgs e)
        {
            TextBox planBox = new TextBox();
            //Margin="60" Width="600" Height="60"
            planBox.Margin = new Thickness(50);
            planBox.Width = 500;
            planBox.Height = 60;
            planBox.TextWrapping = TextWrapping.Wrap;
            newGrid.Children.Add(planBox);
            newGrid.Visibility = Visibility.Visible;
            oldGrid.Visibility = Visibility.Collapsed;


        }

        private void oldsButton_Click(object sender, RoutedEventArgs e)
        {
            newGrid.Visibility = Visibility.Collapsed;
            oldGrid.Visibility = Visibility.Visible;
        }

        private void submit_Click(object sender, RoutedEventArgs e)
        {
            if(text.Text != "" && title.Text != "" && calendar != null) {
                PlanRem newPlanRem = new PlanRem(text.Text, calendar, title.Text);
                doneText.Visibility = Visibility.Visible;
            }
            else
            {
                doneTextError.Visibility = Visibility.Visible;
            }
            

        }
    }

    public class PlanRem
    {
        String text;
        Calendar date;
        String title;
        
        public PlanRem(String text, Calendar date, String title)
        {
            this.text = text;
            this.date = date;
            this.title = title;
        }
    }
}
