namespace MovieHunter.Messenger
{
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
    using System.Text.RegularExpressions;

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
            this.ChatBox.KeyDown += (s, e) => 
            {
                if(e.Key == Key.Enter)
                {
                    this.button_Click(s, e);
                }
            };
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
                           var match = Regex.Match(m, "{.+}").Value;
                           var stuffToAppend = JsonConvert.DeserializeObject<Message>(match);// string.Format("{0} {1}:  {2}{3}", ((DateTime)m["Sent"]).ToShortTimeString(), m["Sender"], m["Content"], Environment.NewLine);
                           this.Dispatcher.Invoke((Action)(() => this.ChatPanel(stuffToAppend)));
                           this.Dispatcher.Invoke(() =>
                           {

                           });
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

            this.ChatBox.Text = "";
            this.chat.Publish<string>(currentChannel, message, x => {
                
            }, x => { });

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
                    s.CastTo<Border>().Background = new SolidColorBrush(Colors.White);
                    // this.activeChats.Enqueue(s.CastTo<Border>());
                };

                userTemplate.MouseDown += (s, e) =>
                {
                    var usernames = new string[] { this.username, s.CastTo<Border>().Child.CastTo<TextBlock>().Text };

                    //string gridXaml = XamlWriter.Save(s);
                    //StringReader stringReader = new StringReader(gridXaml);
                    //XmlReader xmlReader = XmlReader.Create(stringReader);
                    //var el = XamlReader.Load(xmlReader);

                    var el = new TextBlock()
                    {
                        Text = "Currently chatting with: " + usernames[1],
                        Foreground = new SolidColorBrush(Colors.CadetBlue)
                    };

                    this.CurrentChat.Children.Clear();
                    this.CurrentChat.Children.Add(el);

                    this.currentChannel = usernames.Min() + " " + usernames.Max();

                    if (!this.chatPanels.ContainsKey(this.currentChannel))
                    {
                        this.chatPanels.Add(this.currentChannel, new StackPanel());
                    }

                    this.ChatContent.Children.Clear();
                    this.ChatContent.Children.Add(this.chatPanels[this.currentChannel]);


                    this.OpenChats.Children.Clear();

                    this.chatPanels.Keys.ForEach(k =>
                    {
                        var b = new Border()
                        {
                            Child = new TextBlock()
                            {
                                Text = k.Split(' ').FirstOrDefault(h => h != this.username),
                                Margin = new Thickness(2)
                            },
                            BorderBrush = new SolidColorBrush(Colors.AntiqueWhite),
                            BorderThickness = new Thickness(1)
                        };

                        b.MouseDown += (o, v) => 
                        {
                            this.ChatContent.Children.Clear();
                            this.currentChannel = this.chatPanels.Keys.FirstOrDefault(ch => ch.Split(' ').Contains(o.CastTo<Border>().Child.CastTo<TextBlock>().Text));
                            this.ChatContent.Children.Add(this.chatPanels[this.currentChannel]);
                            this.CurrentChat.Children[0].CastTo<TextBlock>().Text = "Currently chatting with: " + this.currentChannel.Split(' ')[1];
                        };

                        b.MouseEnter += (o, v) => 
                        {
                            o.CastTo<Border>().Child.CastTo<TextBlock>().Background = new SolidColorBrush(Colors.Aquamarine);
                        };

                        b.MouseLeave += (o, v) => 
                        {
                            o.CastTo<Border>().Child.CastTo<TextBlock>().Background = new SolidColorBrush(Colors.White);
                        };

                        this.OpenChats.Children.Add(b);
                    });


                    this.Subscribe();
                };

                this.Users.Children.Add(userTemplate);
            });

        }

        public void ChatPanel(Message msg)
        {
            if (!this.chatPanels.ContainsKey(this.currentChannel))
            {
                this.chatPanels.Add(this.currentChannel, new StackPanel());
            }

            var userTemplate = new Border()
            {
                Background = new SolidColorBrush(msg.Sender == this.username ? Colors.CornflowerBlue : Colors.Red),
                CornerRadius = new CornerRadius(10),
                Margin = new Thickness(0, 10, 0, 0),
                Width = 200,
                Child = new TextBlock()
                {
                    Text = msg.Content,
                    Height = 30,
                    Padding = new Thickness(10),
                    HorizontalAlignment = msg.Sender == this.username ? HorizontalAlignment.Left : HorizontalAlignment.Right
                },
                HorizontalAlignment = msg.Sender == this.username ? HorizontalAlignment.Left : HorizontalAlignment.Right
            };

            this.chatPanels[this.currentChannel].Children.Add(userTemplate);
            //this.ChatContent.Children.Clear();
            //this.ChatContent.Children.Add(this.chatPanels[this.currentChannel]);
        }
    }
}
