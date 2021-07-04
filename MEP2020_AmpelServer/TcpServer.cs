using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace MEP2020_AmpelServer {
    public class TcpServer {
        private const int port = 7654;
        private IPEndPoint ipEndPoint;
        private TcpListener tcpListener;
        private Thread thread;

        public event EventHandler<PersonLeftEventArgs> PersonLeft;

        public TcpServer() {
            this.ipEndPoint = new IPEndPoint(IPAddress.Any, port);
            this.tcpListener = new TcpListener(this.ipEndPoint);
            this.tcpListener.Start();

            this.thread = new Thread(() => Run());
            this.thread.Name = "Tcp Server";
            this.thread.Start();

        }

      private void Run() {
            bool connected = false;
            while (true) {
                TcpClient tcpClient = this.tcpListener.AcceptTcpClient();

                //recieve data
                StreamReader sr = new StreamReader(tcpClient.GetStream());
                connected = true;
                string line;
                while (tcpClient.Connected) {
                    try {
                        line = sr?.ReadLine();

                        string[] s = line.Split(':', 2);
                        string topic = s[0];
                        string msg = s[1];

                        if (topic == "person/left") {
                            int countLeft;
                            if (Int32.TryParse(msg, out countLeft)) {
                                this.OnPersonLeft(countLeft);
                            }

                        }
                    } catch (IOException) {
                        connected = false;
                    }
                    
                }
            }

        }

        private void OnPersonLeft(int count) {
            this.PersonLeft?.Invoke(this, new PersonLeftEventArgs(count));
        }
    }
}
