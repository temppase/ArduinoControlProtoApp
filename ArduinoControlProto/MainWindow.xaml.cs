using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ArduinoControlProto.Controllers;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ArduinoControlProto.Classes;
using System.Diagnostics;

namespace ArduinoControlProto
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        TcpClass tcp = new();
        public MainWindow()
        {
            InitializeComponent();
            View.Content = new SetupControl();
            if (!tcp.TestConnection())
            {
                Restart.Visibility = Visibility.Visible;
                Fill.Visibility = Visibility.Visible;
                Information.Visibility = Visibility.Hidden;
            }
            else
            {
                if(tcp.stateCheck(tcp.SendCommand("info"), 8) == 1)
                {
                    Parameters.Visibility = Visibility.Hidden;
                    Setup.Visibility = Visibility.Hidden;
                }
                else if(tcp.stateCheck(tcp.SendCommand("info"), 9) == 1)
                {
                    Parameters.Visibility = Visibility.Visible;
                }
                else
                {
                    Parameters.Visibility = Visibility.Hidden;
                    Information.Text = "No reference point set";
                }
                Setup.Visibility = Visibility.Visible;
                Info.Visibility = Visibility.Visible;
                Reset.Visibility = Visibility.Visible;
                Restart.Visibility = Visibility.Hidden;
                Fill.Visibility = Visibility.Hidden;
                Information.Visibility= Visibility.Visible;
            }
        }
        private void Info_Click(object sender, RoutedEventArgs e)
        {
            View.Content = new InfoControl();
        }
        private void Parameters_Click(object sender, RoutedEventArgs e)
        {
            View.Content = new ParametersControl();
        }
        private void Setup_Click(object sender, RoutedEventArgs e)
        {
            View.Content = new SetupControl();
        }
        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            tcp.SendCommand("reset");
            Process.Start(Process.GetCurrentProcess().ProcessName);
            App.Current.Shutdown();
        }

        private void Info_MouseEnter(object sender, MouseEventArgs e)
        {
            Information.Text = "Information for current state.";
        }

        private void Parameters_MouseEnter(object sender, MouseEventArgs e)
        {
            Information.Text = "Set and send parameters here.";
        }

        private void Setup_MouseEnter(object sender, MouseEventArgs e)
        {
            Information.Text = "Possible setups.";
        }

        private void Reset_MouseEnter(object sender, MouseEventArgs e)
        {
            Information.Text = "Process reset.";
        }

        private void View_MouseEnter(object sender, MouseEventArgs e)
        {
            Information.Text = "";
        }

        private void Information_MouseEnter(object sender, MouseEventArgs e)
        {
            Information.Text = "";
        }

        private void Restart_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(Process.GetCurrentProcess().ProcessName);
            App.Current.Shutdown();
        }

        private void Restart_MouseEnter(object sender, MouseEventArgs e)
        {
            Fill.Text = "Restart app";
        }
    }
}
