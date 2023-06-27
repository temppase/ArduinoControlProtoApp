using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ArduinoControlProto.Classes;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;

namespace ArduinoControlProto.Controllers
{
    /// <summary>
    /// Interaction logic for ParametersControl.xaml
    /// </summary>
    public partial class ParametersControl : UserControl
    {
        double Total_S = 0;
        double Total_L = 0;
        double Interval_S = 0;
        double Intervals = 0;
        double Interval_L = 0;
        int Direction = 1;
        int Offset = 0;
        TcpClass tcp = new();
        public ParametersControl()
        {
            InitializeComponent();
            Header.Text = "Parameters";
            DD.Text = "00";
            HH.Text = "00";
            MM.Text = "00";
            I_MM.Text = "00";
            I_SS.Text = "00";
            TL.Text = "350";
            SliderV.Text = "0";
            PlayBtn.Visibility = Visibility.Hidden;
            SendBtn.Visibility = Visibility.Hidden;
        }
        private void CountBtn_Click(object sender, RoutedEventArgs e)
        {
            Total_S = TimeSpan.Parse($"{DD.Text}:{HH.Text}:{MM.Text}:00").TotalSeconds;
            Total_L = Convert.ToDouble(TL.Text);
            Interval_S = TimeSpan.Parse($"00:{I_MM.Text}:{I_SS.Text}").TotalSeconds;
            Intervals = Math.Round(Total_S / Interval_S, 0);
            Interval_L = Math.Round(Total_L / Intervals,2);
            if(Interval_L < 0.01)
            {
                Interval_L = 0.01;
                I_count.Text = ": " + Total_S.ToString() + " s / " + Interval_S.ToString() + " s = " 
                    + Intervals + " Intervals";
                L_count.Text = " Interval lenght mm: " + Interval_L + " (replaced value)";
            }
            else
            {
                I_count.Text = ": " + Total_S.ToString() + " s / " + Interval_S.ToString() + " s = "
                    + Intervals + " Intervals";
                L_count.Text = " Intervals, interval lenght mm: " + Interval_L;
            }
            SendBtn.Visibility = Visibility.Visible;
            DirBtn.Visibility = Visibility.Visible;
        }
        private void SendBtn_Click(Object sender, RoutedEventArgs e)
        {
            tcp.SendCommand(Data());
            PlayBtn.Visibility = Visibility.Visible;
        }
        private void Sledge_ValueChanged(object sender, EventArgs e)
        {
            Offset = Convert.ToInt32(Math.Round(Sledge_position.Value, 0));
            SliderV.Text = Offset.ToString();
        }
        private string Data()
        {
            string data = $"{Intervals}|{Interval_L}|{Interval_S}|{Direction}|{Offset}";
            return data;
        }

        private void PlayBtn_Click(object sender, RoutedEventArgs e)
        {
            tcp.SendCommand("play");
            Process.Start(Process.GetCurrentProcess().ProcessName);
            App.Current.Shutdown();
        }

        private void DirBtn_Click(object sender, RoutedEventArgs e)
        {
            if(Direction == 1)
            {
                Direction = -1;
                DirBtn.Content = "<<";
            }
            else
            {
                Direction = 1;
                DirBtn.Content = ">>";
            }
        }
    }
}
