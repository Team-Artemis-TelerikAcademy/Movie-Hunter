using System;
using System.Collections.Generic;
using System.IO;
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

namespace ChatTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MessengerWindow : Window
    {
        private PubnubAPI chat;

        private string username = "gosho";

        public MessengerWindow(string username)
        {
            this.username = username;
            InitializeComponent();
            this.InitialiazePubNub();
        }

        private void InitialiazePubNub()
        {
            this.chat = new PubnubAPI(
                "pub-c-25499a0d-31e9-468c-b4e4-e1940acb2e46",               // PUBLISH_KEY
                "sub-c-73f3014a-8793-11e5-8e17-02ee2ddab7fe",               // SUBSCRIBE_KEY
                "sec-c-NzBlZGI4OGYtZjkzNS00MjQ1LWI2NzItMTZlODNlOTdjNDEz",   // SECRET_KEY
                true                                                        // SSL_ON?
            );
            string channel = "demo-channel";


            Task.Run(() =>
            {
                this.chat.Subscribe(
                   channel,
                   delegate (object message)
                   {
                       try
                       {
                           var m = message as Dictionary<string, object>;
                           var stuffToAppend = string.Format("{0} {1}:  {2}{3}", ((DateTime)m["Sent"]).ToShortTimeString(), m["Sender"], m["Content"], Environment.NewLine);
                           this.Dispatcher.Invoke((Action)(() => this.ChatContent.Text += stuffToAppend));
                       }
                       catch (Exception e)
                       {
                           File.AppendAllText("../../huchuc.txt", e.Message + Environment.NewLine);
                       }
                       return true;
                   });
            });
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            this.chat.Publish("demo-channel", new Message()
            {
                Content = this.ChatBox.Text,
                Sender = this.username,
                Sent = DateTime.Now
            });
        }
    }
}
