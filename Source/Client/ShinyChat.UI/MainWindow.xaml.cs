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
            var chan = new Channel();
            chan.Name = "Testchannel";
            var msgBuilder = new MessageBuilder();
            var testMessageYo = msgBuilder.BuildCommand(0, Enums.CommandType.JoinChannel, chan, "jug-eared");
        }
    }
}
