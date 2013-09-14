using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ShinyChat.Dependencies;
using ShinyChat.Core.Server;
using System.Net;
using ShinyChat.Core.Entities;
using ShinyChat.Core.Managers;
using ShinyChat.Common.Entities;
using System.Threading;
using ShinyChat.Common.Server;
using System.ComponentModel;

namespace ShinyChat
{
    /// <summary>
    /// Interaktionslogik für TestWindow.xaml
    /// </summary>
    public partial class TestWindow : Window, IServerSubscriber, INotifyPropertyChanged
    {
        private ChatServer server;
        private Channel channel;

        public TestWindow()
        {
            InitializeComponent();
            DependenciesBootstrapper.Init();
        }

        private void ConnectToServerBtn_Click(object sender, RoutedEventArgs e)
        {
            server = new ChatServer();
            server.Ip = IPAddress.Parse("91.67.100.107");
            server.Port = 50007;
            server.Endpoint = new IPEndPoint(server.Ip, server.Port);
            var success = server.OpenConnection();
            server.Subscribe(this);
        }

        private void DisconnectFromServerBtn_Click(object sender, RoutedEventArgs e)
        {
            server.CloseConnection();
        }

        private void SendToServerBtn_Click(object sender, RoutedEventArgs e)
        {
            var message2 = new MessageBuilder().BuildServerMessage(channel, "jug-eared", SendMessageText.Text);
            server.SendMessage(message2);
        }

        private void JoinChannelBtn_Click(object sender, RoutedEventArgs e)
        {
            channel = new Channel() { Name = JoinChannelText.Text, MessageLog = new MessageLog(), IsOpened = true };
            var message = new MessageBuilder().BuildCommand(Enums.CommandType.JoinChannel, channel, "jug-eared");
            server.SendMessage(message);
        }

        private string messageReceivedText;
        public string MessageReceivedText 
        { 
            get 
            {
                return messageReceivedText;
            }
            set
            {
                messageReceivedText = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("MessageReceivedText"));
            }
        }

        public void OnServerMessageReceived()
        {
            MessageReceivedText += "Message Received;";
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
