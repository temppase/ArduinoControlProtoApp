using ArduinoControlProto.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
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

namespace ArduinoControlProto.Controllers
{
    /// <summary>
    /// Interaction logic for SetupControl.xaml
    /// </summary>
    public partial class SetupControl : UserControl
    {
        TcpClass tcp = new();
        public SetupControl()
        {
            InitializeComponent();
            Header.Text = "Arduino Ethernet Control";
            if (!tcp.TestConnection())
            {
                Content.Text = "No device available. Start Arduino and press restart.";
                Sledge_position.Visibility = Visibility.Hidden;
            }
            else
            {
                Content.Text = tcp.SendCommand("info");
            }
        }
        private void Sledge_ValueChanged(object sender, EventArgs e)
        {
            Content.Text = "Sledge position: " + Math.Round(Sledge_position.Value, 0).ToString() + " mm";
        }

        private void Sledge_MouseUp(object sender, MouseButtonEventArgs e)
        {
            tcp.SendCommand($"move|{Math.Round(Sledge_position.Value, 0)}");
        }
    }
}
