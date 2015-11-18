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

        private UIComponentProvider uiElements;
        private DataRequester requester;

        private IDictionary<string, StackPanel> chatPanels;

        public MessengerWindow(string username, string authKey)
        {
            this.username = username;
            this.authKey = authKey;
            InitializeComponent();
            this.InitialiazePubNub();
            this.chatPanels = new Dictionary<string, StackPanel>();
            this.uiElements = new UIComponentProvider();
            this.requester = new DataRequester();
            this.ChatBox.KeyDown += (s, e) =>
            {
                if (e.Key == Key.Enter)
                {
                    this.button_Click(s, e);
                }
            };
        }

        private void InitialiazePubNub()
        {
            this.chat = new Pubnub("pub-c-25499a0d-31e9-468c-b4e4-e1940acb2e46", "sub-c-73f3014a-8793-11e5-8e17-02ee2ddab7fe");
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
                           this.Dispatcher.Invoke(() => this.ChatPanel(stuffToAppend));
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

            var msg = new MessageViewModel()
            {
                Content = message.Content,
                TimeSent = DateTime.Now,
                Author = this.username,
                Recepient = this.currentChannel.Split(' ')[1]
            };

            var response = this.requester.RequestWithJsonBody("http://localhost:52189/api/Messages", JsonConvert.SerializeObject(msg), new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("Authorization", "bearer " + this.authKey)
            });

            MessageBox.Show(response);

            this.ChatBox.Text = "";
            this.chat.Publish<string>(currentChannel, message, x => { }, x => { });

        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var match = sender.CastTo<TextBox>().Text;

            var response = this.requester.Request("http://localhost:52189/api/Users?username=" + match);

            this.Templatize(response);
        }

        private void Templatize(string response)
        {
            var users = JsonConvert.DeserializeObject<string[]>(response);

            this.Users.Children.Clear();

            users.ForEach(x =>
            {
                var userTemplate = this.uiElements.ContactTemplate(x);

                SetHoverColor(userTemplate, Colors.DeepSkyBlue);

                userTemplate.MouseDown += (s, e) =>
                {
                    var usernames = new string[] { this.username, s.CastTo<Border>().Child.CastTo<TextBlock>().Text };

                    var el = this.uiElements.ChatLabel(usernames[1]);

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
                        var b = this.uiElements.OpenChatLabel(k.Split(' ').FirstOrDefault(h => h != this.username));

                        b.MouseDown += (o, v) =>
                        {
                            this.ChatContent.Children.Clear();
                            this.currentChannel = this.chatPanels.Keys.FirstOrDefault(ch => ch.Split(' ').Contains(o.CastTo<Border>().Child.CastTo<TextBlock>().Text));
                            this.ChatContent.Children.Add(this.chatPanels[this.currentChannel]);
                            this.CurrentChat.Children[0].CastTo<TextBlock>().Text = "Currently chatting with: " + this.currentChannel.Split(' ')[1];
                        };

                        SetHoverColor(b, Colors.Aquamarine);

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

            var userTemplate = this.uiElements.MessageTemplate(msg.Sender == this.username, msg.Content);

            this.chatPanels[this.currentChannel].Children.Add(userTemplate);
        }

        private static void SetHoverColor(Border element, Color color)
        {
            element.MouseEnter += (o, v) =>
            {
                element.Background = new SolidColorBrush(color);
            };

            element.MouseLeave += (o, v) =>
            {
                element.Background = new SolidColorBrush(Colors.White);
            };
        }
    }
}
