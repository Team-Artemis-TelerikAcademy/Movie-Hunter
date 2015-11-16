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
using Newtonsoft.Json;
using FLExtensions.Common;
using PubNubMessaging.Core;
using System.Windows.Markup;
using System.Xml;

namespace MovieHunter.Messenger
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MessengerWindow : Window
    {
        private Pubnub chat;

        private string username;
        private string authKey;
        private string currentChannel;

        private IDictionary<string, StackPanel> chatPanels;

        public MessengerWindow(string username, string authKey)
        {
            this.username = username;
            this.authKey = authKey;
            InitializeComponent();
            this.InitialiazePubNub();
            this.chatPanels = new Dictionary<string, StackPanel>();
        }

        private void InitialiazePubNub()
        {
            this.chat = new Pubnub("pub-c-25499a0d-31e9-468c-b4e4-e1940acb2e46", "sub-c-73f3014a-8793-11e5-8e17-02ee2ddab7fe");
            //    "pub-c-25499a0d-31e9-468c-b4e4-e1940acb2e46",               // PUBLISH_KEY
            //    "sub-c-73f3014a-8793-11e5-8e17-02ee2ddab7fe",               // SUBSCRIBE_KEY
            //    "sec-c-NzBlZGI4OGYtZjkzNS00MjQ1LWI2NzItMTZlODNlOTdjNDEz",   // SECRET_KEY
            //    true                                                        // SSL_ON?
            //);
            //this.currentChannel = "demo-channel";

            //this.Subscribe();
            //Task.Run(() =>
            //{
            //    this.chat.Subscribe(
            //       channel,
            //       delegate (object message)
            //       {
            //           try
            //           {
            //               var m = message as Dictionary<string, object>;
            //               var stuffToAppend = string.Format("{0} {1}:  {2}{3}", ((DateTime)m["Sent"]).ToShortTimeString(), m["Sender"], m["Content"], Environment.NewLine);
            //               this.Dispatcher.Invoke((Action)(() => this.ChatContent.Text += stuffToAppend));
            //           }
            //           catch (Exception e)
            //           {
            //               File.AppendAllText("../../huchuc.txt", e.Message + Environment.NewLine);
            //           }
            //           return true;
            //       });
            //});
        }

        private void Subscribe()
        {
            Task.Run(() =>
            {
                this.chat.Subscribe<string>(
                   this.currentChannel,
                   m =>
                   {
                       try
                       {
                           // var m = message as Dictionary<string, object>;
                           var stuffToAppend = m;// string.Format("{0} {1}:  {2}{3}", ((DateTime)m["Sent"]).ToShortTimeString(), m["Sender"], m["Content"], Environment.NewLine);
                           this.Dispatcher.Invoke((Action)(() => this.ChatContent.Text += stuffToAppend));
                       }
                       catch (Exception e)
                       {
                           File.AppendAllText("../../huchuc.txt", e.Message + Environment.NewLine);
                       }
                   },
                   k => { },
                   k => { }
                   );
            });
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            var message = new Message()
            {
                Content = this.ChatBox.Text,
                Sender = this.username,
                Sent = DateTime.Now
            };

            this.chat.Publish<string>(currentChannel, message.Content, x => { }, x => { });
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var match = (sender as TextBox).Text;

            var response = new DataRequester().Request("http://localhost:52189/api/Users?username=" + match);

            this.Templatize(response);
        }

        private void Templatize(string response)
        {
            var users = JsonConvert.DeserializeObject<string[]>(response);

            this.Users.Children.Clear();

            users.ForEach(x =>
            {
                var userTemplate = new Border()
                {
                    BorderThickness = new Thickness(2),
                    BorderBrush = new SolidColorBrush(Colors.CornflowerBlue),
                    CornerRadius = new CornerRadius(10),
                    Margin = new Thickness(0, 10, 0, 0),
                    Child = new TextBlock()
                    {
                        Text = x,
                        Height = 30,
                        Padding = new Thickness(10)
                    }
                };

                userTemplate.MouseEnter += (s, e) =>
                {
                    (s as Border).Background = new SolidColorBrush(Colors.DeepSkyBlue);
                };

                userTemplate.MouseLeave += (s, e) =>
                {
                    (s as Border).Background = new SolidColorBrush(Colors.White);
                };

                userTemplate.MouseDown += (s, e) =>
                {
                    var usernames = new string[] { this.username, s.CastTo<Border>().Child.CastTo<TextBlock>().Text };

                    string gridXaml = XamlWriter.Save(s);
                    StringReader stringReader = new StringReader(gridXaml);
                    XmlReader xmlReader = XmlReader.Create(stringReader);
                    var el = XamlReader.Load(xmlReader);
                    this.OpenChats.Children.Clear();
                    this.OpenChats.Children.Add(el.CastTo<Border>());
                    
                    this.currentChannel = usernames.Min() + " " + usernames.Max();
                    this.Subscribe();
                };

                this.Users.Children.Add(userTemplate);
            });

        }

        public StackPanel ChatPanel()
        {
            throw new NotImplementedException();
        }
    }
}
