using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace MEP2020_Exit {
    public class Tcp_Client {
        private string hostname = "localhost";
        private int port = 7654;
        private TcpClient tcpClient;
        private Thread thread;
        private NetworkStream ns;
        private StreamWriter sw;

        public Tcp_Client() {
            this.tcpClient = new TcpClient(this.hostname, this.port);
            this.ns = tcpClient.GetStream();
            this.sw = new StreamWriter(ns);
        }

        public void SendPersonLeft(int count) {
            this.sw.WriteLine($"person/left:{count}");
            this.sw.Flush();
        }

    }
}
