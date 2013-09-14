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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ShinyChat.Core.Entities;
using ShinyChat.Core.Managers;
using ShinyChat.Common.Entities;
using ShinyChat.Dependencies;
using Castle.Windsor;
using ShinyChat.Core.DI;
using ShinyChat.Core.Server;
using System.Net;
using System.IO;

namespace ShinyChat.UI
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DependenciesBootstrapper.Init();

            //var channel2 = new Channel() { Name = "HalloChrisiehChannelBla", MessageLog = new MessageLog(), IsOpened = true };
            //var message2 = new MessageBuilder().BuildCommand(Enums.CommandType.JoinChannel, channel2, "jug-eared");

            var server = new ChatServer();
            server.Ip = IPAddress.Parse("91.67.100.107");
            server.Port = 50007;
            server.Endpoint = new IPEndPoint(server.Ip, server.Port);
            var success = server.OpenConnection();
            var channel = new Channel() { Name = "HalloChrisiehChannelBla", MessageLog = new MessageLog(), IsOpened = true };
            var message = new MessageBuilder().BuildCommand(Enums.CommandType.JoinChannel, channel, "jug-eared");
            server.SendMessage(message);
            var message2 = new MessageBuilder().BuildServerMessage(channel, "jug-eared", "Heyho was geht so?");
            server.SendMessage(message2);
        }
    }
}
