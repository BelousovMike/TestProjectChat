using Logger;
using System.Net;
using System.Windows;

namespace Server
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string logFileName = "myLog.log";

        private ChatServer _server = new ChatServer(new MainServer(), new Logger.Logger(new TextFileStorage(logFileName, false)));
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnListen_Click(object sender, RoutedEventArgs e)
        {
            var port = int.Parse(tbxPort.Text);
            var serverConnection = new TcpListenerAdapter(new System.Net.Sockets.TcpListener(IPAddress.Any ,port));
            _server.Listen(serverConnection);
            btnListen.IsEnabled = false;
            btnStop.IsEnabled = true;
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            _server.Stop();
            btnListen.IsEnabled = true;
            btnStop.IsEnabled = false;
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void gridTitle_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            try
            {
                DragMove();
            }
            catch { }
        }
    }
}
