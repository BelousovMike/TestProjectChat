using MessageSender;
using MessageSender.Commands;
using MessageSender.Interfaces;
using System;
using System.Windows;

namespace ChatClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Client _chatClient = null;
        private IMessageSender _messageSender = null;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void _chatClient_OnException(object sender, Exception e)
        {
            MessageBox.Show(e.Message);
        }

        private void _chatClient_Disconnected(object sender, EventArgs e)
        {
            btnConnect.Dispatcher.BeginInvoke(new Action(() => btnConnect.Content = "Подключиться"));
            tbkMainChatText.Dispatcher.BeginInvoke(new Action(() => tbkMainChatText.Text += "Отключено\r\n"));
        }

        private void _chatClient_Connected(object sender, EventArgs e)
        {
            btnConnect.Dispatcher.BeginInvoke(new Action(() => btnConnect.Content = "Отключиться"));
            tbkMainChatText.Dispatcher.BeginInvoke(new Action(() => tbkMainChatText.Text += "Подключено\r\n"));
        }

        private void _chatClient_OnGetMessage(object sender, string e)
        {
            Dispatcher.Invoke(new Action(() =>
            {
                tbkMainChatText.Text += "-> " + e + "\r\n";
            }));

        }

        private void btnConnect_Click(object sender, RoutedEventArgs e)
        {
            if (_chatClient is null || _chatClient.IsConnected == false)
            {
                var ipAddress = ucIpAddress.Address;
                var port = int.Parse(tbxPort.Text);
                var _adapter = new TcpClientAdapter(new System.Net.Sockets.TcpClient());
                _messageSender = new MessageSender.MessageSender(_adapter);
                _chatClient = new Client(_messageSender);
                _chatClient.ReadMessage += _chatClient_OnGetMessage;
                _chatClient.Connected += _chatClient_Connected;
                _chatClient.Disconnected += _chatClient_Disconnected;
                _chatClient.OnException += _chatClient_OnException;
                _chatClient.Connect(ipAddress, port);
            }
            else
            {
                _chatClient.Disconnect();
                _chatClient.ReadMessage -= _chatClient_OnGetMessage;

            }
        }

        private void btnSendMessage_Click(object sender, RoutedEventArgs e)
        {
            if (_chatClient is null)
                return;

            SendMessage(tbxMessage.Text);
            tbkMainChatText.Text += "<- " + tbxMessage.Text + "\r\n";
            tbxMessage.Text = string.Empty;
        }

        private void gridTitle_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            try
            {
                DragMove();
            }
            catch { }
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnGetLog_Click(object sender, RoutedEventArgs e)
        {
            SendMessage(Commands.GetLogCommand());
        }

        private void SendMessage(string message)
        {
            if (_chatClient is null)
                return;

            _chatClient.SendMessage(message.Replace("\r\n", string.Empty));
        }
    }
}
