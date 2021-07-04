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

namespace MEP2020_AmpelServer {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {

        private int MaxPerson;
        private int ActPerson;
        private bool LimitReached;
        private TcpServer tcpServer;
        private FileHandler logFile;
        public MainWindow() {
            InitializeComponent();
            this.TextBox_ActPerson.IsEnabled = false;
            this.MaxPerson = 10;
            this.ActPerson = 0;
            this.LimitReached = false;
            this.TextBox_ActPerson.Text = Convert.ToString(this.ActPerson);
            this.TextBox_MaxPersonen.Text = Convert.ToString(this.MaxPerson);
            this.tcpServer = new TcpServer();
            this.tcpServer.PersonLeft += TcpServer_PersonLeft;
            this.logFile = new FileHandler("logFile");
        }

        private void TcpServer_PersonLeft(object sender, PersonLeftEventArgs e) {
            this.ActPerson -= e.NbrPersonLeft;
            Dispatcher.BeginInvoke(new Action(delegate () {
                if (this.ActPerson < 0) {
                    MessageBox.Show("A person Left, but there was nobody in the room!!",
                        "Suspicious", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    this.ActPerson = 0;
                } else {
                    this.logFile.AddLinetoLogFile(false, this.ActPerson, this.MaxPerson);
                    this.TextBox_ActPerson.Text = Convert.ToString(this.ActPerson);
                    this.CheckPerson();
                }
            }));
            
        }

        private void Button_Enter_Click(object sender, RoutedEventArgs e) {
            this.ActPerson++;
            this.TextBox_ActPerson.Text = Convert.ToString(this.ActPerson);
            this.logFile.AddLinetoLogFile(true, this.ActPerson, this.MaxPerson);
            this.CheckPerson();
        }

        private void TextBox_MaxPersonen_TextChanged(object sender, TextChangedEventArgs e) {
            this.MaxPerson = Convert.ToInt32(TextBox_MaxPersonen.Text);
            this.CheckPerson();
        }

        private void CheckPerson() {
            if(ActPerson < MaxPerson && LimitReached == true) { //if max now not reached but before -> change to green
                this.LimitReached = false;
                this.Rect_VisualiseEnter.Fill = new SolidColorBrush(Color.FromRgb(0, 255, 0));
                this.Button_Enter.IsEnabled = true;
                this.Label_Wait.Content = "Bitte Eintreten";
            } else if(ActPerson >= MaxPerson && LimitReached == false){
                this.Rect_VisualiseEnter.Fill = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                this.Button_Enter.IsEnabled = false;
                this.LimitReached = true;
                this.Label_Wait.Content = "Bitte Warten";
            }
        }
    }
}
