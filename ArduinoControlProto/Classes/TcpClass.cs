using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Net.NetworkInformation;
using System.Security.Policy;
using System.Text.RegularExpressions;

namespace ArduinoControlProto.Classes
{
    class TcpClass
    {
        TcpClient client = new();
        private string url = "192.168.1.177";
        int port = 23;
        string mes;
        public TcpClass() { }
        public string SendCommand(string command)
        {
            try
            {
                using TcpClient client = new TcpClient(url, port);
                byte[] data = Encoding.ASCII.GetBytes($"{command}*");
                NetworkStream stream = client.GetStream();
                stream.Write(data, 0, data.Length);
                data = new byte[256];
                string responseData = string.Empty;
                int bytes = stream.Read(data, 0, data.Length);
                responseData = Encoding.ASCII.GetString(data, 0, bytes);
                mes = $"Received:\n {responseData}\n";
                // Explicit close is not necessary since TcpClient.Dispose() will be
                // called automatically.
            }
            catch (ArgumentNullException e)
            {
                mes += $"No connection\nArgument null exception: \n{e} \n";
            }
            catch (SocketException e)
            {
                mes += $"No connection\nSocket exception:\n{e}";
            }
            return mes;
        }
        public bool TestConnection()
        {
            bool pingable = false;
            Ping? pinger = null;
            try
            {
                pinger = new Ping();
                PingReply reply = pinger.Send("192.168.1.177");
                pingable = reply.Status == IPStatus.Success;
            }
            catch (PingException)
            {
                // Discard PingExceptions and return false;
            }
            finally
            {
                pinger?.Dispose();
            }

            return pingable;
        }
        public double stateCheck(string text, int index)
        {
            var res = Regex.Split(text, "\r\n|\r|\n");
            var r = res[index].Split(':');
            double p = double.Parse(r[1].Replace('.', ','));
            return p;
        }
    }
}
