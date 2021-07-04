using System;
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

namespace MEP2020_Exit {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        private Tcp_Client tcpClient;
        public MainWindow() {
            InitializeComponent();
            tcpClient = new Tcp_Client();
        }

        private void Button_PersonLeft_Click(object sender, RoutedEventArgs e) {
            tcpClient.SendPersonLeft(1);
        }
    }
}
