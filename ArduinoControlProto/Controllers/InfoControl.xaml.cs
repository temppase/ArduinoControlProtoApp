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

namespace ArduinoControlProto.Controllers
{
    /// <summary>
    /// Interaction logic for InfoControl.xaml
    /// </summary>
    /// 
    public partial class InfoControl : UserControl
    {
        TcpClass tcp = new();
        public InfoControl()
        {
            InitializeComponent();
            // Connect to arduino and return data...
            Header.Text = "Information";
            Content.Text = tcp.SendCommand("info");
        }
    }
}
