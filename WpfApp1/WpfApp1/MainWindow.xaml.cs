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
using System.Reflection.Emit;
using System.Windows.Threading;

namespace WpfApp1
{
    /// <summary>
    /// MainWindow.xaml etkileşim mantığı
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer timer;
        public MainWindow()
        {
            InitializeComponent();


            


            List<PlanRem> loadedData = LocalStorage.LoadData();

            foreach (PlanRem item in loadedData)
            {
                createPlanGrid(item.title.ToString(), item.text.ToString(), item.date.ToString());
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

                createPlanGrid(title.Text, text.Text, calendar.SelectedDate.ToString());
                
                doneTextError.Visibility = Visibility.Collapsed;
                doneText.Visibility = Visibility.Visible;
                timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromSeconds(2); // 5 seconds
                timer.Tick += Timer_Tick;
                timer.Start();
            }
            else
            {
                doneTextError.Visibility = Visibility.Visible;
                timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromSeconds(2); // 5 seconds
                timer.Tick += Timer_Tick;
                timer.Start();
            }
            
            


        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            doneText.Visibility = Visibility.Collapsed;
            doneTextError.Visibility = Visibility.Collapsed;
            timer.Stop();
        }


        private void resetButton_Click(object sender, RoutedEventArgs e)
        {
            LocalStorage.DeleteAllData();
            cont.Content = null;
        }

        public void createPlanGrid(String title, String text, String date)
        {
            /*
             <Grid Margin="0,10,0,0" Name="aPlan">
                                <Border Padding="10" BorderThickness="1" BorderBrush="LightGray">
                                    <StackPanel Name="planStack">
                                        <TextBlock Name="titleBlock" Margin="0,7,0,0" FontSize="25">Title</TextBlock>
                                        <TextBlock x:Name="testLabel" Margin="0,7,0,0" FontSize="15">Text</TextBlock>
                                        <TextBlock x:Name="dateBlock" Margin="0,7,0,0" FontSize="15">06/12/2023</TextBlock>
                                    </StackPanel>
                                </Border>
                            </Grid>
             */

            Grid aPlan = new Grid();
            aPlan.Margin = new Thickness(0,10,0,0);

            Border border = new Border();
            border.Padding = new Thickness(10);
            border.BorderThickness = new Thickness(1);
            border.BorderBrush = Brushes.LightGray;
            

            StackPanel planStack = new StackPanel();

            TextBlock titleBlock = new TextBlock();
            titleBlock.Margin = new Thickness(0,7,0,0);
            titleBlock.FontSize = 25;

            TextBlock textBlock = new TextBlock();
            textBlock.Margin = new Thickness(0, 7, 0, 0);
            titleBlock.FontSize = 15;

            TextBlock dateBlock = new TextBlock();
            dateBlock.Margin = new Thickness(0, 7, 0, 0);
            dateBlock.FontSize = 15;

            titleBlock.TextWrapping = TextWrapping.Wrap;
            textBlock.TextWrapping= TextWrapping.Wrap;
            dateBlock.TextWrapping= TextWrapping.Wrap;

            titleBlock.Text = title;
            textBlock.Text = text;
            dateBlock.Text = date;

            titleBlock.Width = 600;
            textBlock.Width = 600;
            dateBlock.Width = 600;

            titleBlock.FontWeight = FontWeights.Bold;

            planStack.Children.Add(titleBlock);
            planStack.Children.Add(textBlock);
            planStack.Children.Add(dateBlock);

            border.Child = planStack;
            aPlan.Children.Add(border);
            
            contentPanel.Children.Add(aPlan);
           

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
