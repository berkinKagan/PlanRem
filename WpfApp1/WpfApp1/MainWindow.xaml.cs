using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Timers;
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
        private static System.Timers.Timer aTimer;
        public MainWindow()
        {
            InitializeComponent();



            List<PlanRem> loadedData = LocalStorage.LoadData();

            foreach (PlanRem item in loadedData)
            {
                testLabel.Content += item.title.ToString() + "\n";
                testLabel.Content += item.text.ToString() + "\n";
                testLabel.Content += item.date.ToString() + "\n";
                testLabel.Content += "\n";
            }





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
            if(text.Text != "" && title.Text != "" && calendar.SelectedDate.ToString() != "") {
   
         
                PlanRem dataObject = new PlanRem {text = text.Text, title = title.Text ,  date = calendar.SelectedDate.ToString() };
                
                LocalStorage.SaveData(dataObject);

                List<PlanRem> loadedData = LocalStorage.LoadData();
                aTimer = new System.Timers.Timer(2000);
                
                testLabel.Content += dataObject.title.ToString() + "\n";
                testLabel.Content += dataObject.text.ToString() + "\n";
                testLabel.Content += dataObject.date.ToString() + "\n";
                testLabel.Content += "\n";
                aTimer.Enabled = true;
                doneTextError.Visibility = Visibility.Collapsed;
                doneText.Visibility = Visibility.Visible;
            }
            else
            {
                doneTextError.Visibility = Visibility.Visible;
            }
            

        }

        private void resetButton_Click(object sender, RoutedEventArgs e)
        {
            LocalStorage.DeleteAllData();
            testLabel.Content = string.Empty;
        }
    }


    public class PlanRem
    {
        public String text { get; set; }
        public String title { get; set; }

        public String date { get; set; }

        public String toString()
        {
            String formatted = "";
            formatted += title.ToString() + "\n";
            formatted += text.ToString() + "\n";
            formatted += date.ToString() + "\n";

            return formatted;
        }
    }

    public static class LocalStorage
    {
        public static List<PlanRem> LoadData()
        {
            if (File.Exists("data.json"))
            {
                string dataJson = File.ReadAllText("data.json");
                return JsonConvert.DeserializeObject<List<PlanRem>>(dataJson);
            }
            return new List<PlanRem>();
        }

        public static void SaveData(PlanRem data)
        {
            List<PlanRem> dataList = LoadData();
            dataList.Add(data);

            string dataJson = JsonConvert.SerializeObject(dataList);
            File.WriteAllText("data.json", dataJson);
        }

        public static void DeleteAllData()
        {
            List<PlanRem> dataList = LoadData();
            while (dataList.Count > 0)
            {
                dataList.RemoveAt(0);
            }
            string dataJson = JsonConvert.SerializeObject(dataList);
            File.WriteAllText("data.json", dataJson);
        }
    }

}
