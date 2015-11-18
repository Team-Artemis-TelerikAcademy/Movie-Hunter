namespace MovieHunter.Messenger
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;
    using Newtonsoft.Json;
    using FLExtensions.Common;
    using Common.PubNubMessaging.Core;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MessengerWindow : Window
    {
        private const string PubnubPublishKey = "pub-c-1c7a2191-1e9e-4439-9249-b43dcba27922";
        private const string PubnubSubstribeKey = "sub-c-f6b6aa8e-885e-11e5-84ee-0619f8945a4f";
        private const string PubnubSecretKey = "sec-c-ODQyN2E2YjEtYWQ5Yy00YTJjLTgwOTEtY2IzNWRlYWUyMjI1";
        private const string MessagesRequestUrl = "http://localhost:52189/api/Messages";
        private const string UsersRequestUrl = "http://localhost:52189/api/Users?username=";

        private static readonly Action<string> EmptyAction = parameter => { };
        private static readonly Action<PubnubClientError> DefaultErrorHandler = error =>
        {
            var report = string.Format("{0}: The following error occured:{1}{2}{1}In channel{3}", DateTime.Now, Environment.NewLine, error.Message, error.Channel);
            File.AppendAllText("../../error-log.txt", report);
        };

        private Pubnub chat;

        private string username;
        private string authKey;
        private string currentChannel;
        private KeyValuePair<string, string> authHeader;

        private UIComponentProvider uiElements;
        private DataRequester requester;

        private IDictionary<string, StackPanel> chatPanels;
        private IDictionary<string, Border> chatLabels;
        

        public MessengerWindow(string username, string authKey, DataRequester requester, UIComponentProvider uiComponentProvider)
        {
            this.username = username;
            this.authKey = authKey;
            this.authHeader = new KeyValuePair<string, string>("Authorization", "bearer " + this.authKey);

            this.requester = requester;
            this.uiElements = uiComponentProvider;

            this.chatPanels = new Dictionary<string, StackPanel>();
            this.chatLabels = new Dictionary<string, Border>();

            this.InitializeComponent();
            this.InitializeStyles();
            this.InitialiazePubNub();
            this.AttachEventsToKeys();
        }

        private void InitializeStyles()
        {
            SetHoverColor(this.ExitButton, Colors.CadetBlue, Colors.CornflowerBlue);
        }

        private void AttachEventsToKeys()
        {
            this.ChatBox.KeyDown += (s, e) =>
            {
                if (e.Key == Key.Enter)
                {
                    this.SendMessage(s, e);
                }
            };
        }

        private void InitialiazePubNub()
        {
            this.chat = new Pubnub(PubnubPublishKey, PubnubSubstribeKey, PubnubSecretKey);
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
                           var json = Regex.Match(m, "{.+}").Value;

                           var messageToAppend = JsonConvert.DeserializeObject<Message>(json);
                           this.Dispatcher.Invoke(() => this.UpdateChatPanels(messageToAppend));
                       }
                       catch (Exception e)
                       {
                           File.AppendAllText("../../huchuc.txt", e.Message + Environment.NewLine);
                       }
                   },
                   EmptyAction,
                   DefaultErrorHandler
                   );
            });


        }

        private void SendMessage(object sender, RoutedEventArgs e)
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

            this.ChatContent.ScrollToEnd();

            var response = this.requester.RequestWithJsonBody(MessagesRequestUrl, JsonConvert.SerializeObject(msg), new List<KeyValuePair<string, string>>()
            {
                this.authHeader
            });

            this.ChatBox.Text = string.Empty;
            this.chat.Publish<string>(currentChannel, message, EmptyAction, DefaultErrorHandler);

        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var match = sender.CastTo<TextBox>().Text;

            var response = this.requester.Request(UsersRequestUrl + match);

            var users = JsonConvert.DeserializeObject<string[]>(response);

            this.Templatize(users);

        }

        private void Templatize(string[] users)
        {
            this.Users.Children.Clear();

            users.ForEach(userNickname =>
            {
                if(userNickname == this.username)
                {
                    return;
                }

                var userTemplate = this.uiElements.ContactTemplate(userNickname);

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

                    this.ChatContent.Content = this.chatPanels[this.currentChannel];

                    this.UpdateChatLabels(userNickname);

                    this.OpenHistory();
                    this.Subscribe();
                };

                this.Users.Children.Add(userTemplate);
            });

        }

        private void UpdateChatLabels(string username)
        {
            if (!this.chatLabels.ContainsKey(username))
            {
                var label = this.uiElements.OpenChatLabel(username);

                label.MouseDown += (s, e) =>
                {
                    this.UpdateChatLabel(s.CastTo<Border>().Child.CastTo<TextBlock>().Text);
                };

                SetHoverColor(label, Colors.DeepSkyBlue);

                this.OpenChats.Children.Add(label);
                this.chatLabels.Add(username, label);
            }
        }

        private void OpenHistory()
        {
            this.chat.DetailedHistory<string>(
                                              this.currentChannel,
                                              1000,
                                              x =>
                                              {
                                                  var match = Regex.Match(x, "\\[\\{.+\\}\\]");
                                                  var messageArray = JsonConvert.DeserializeObject<Message[]>(match.Value);

                                                  messageArray.ForEach(historyMessage =>
                                                  {
                                                      this.Dispatcher.Invoke(() => this.UpdateChatPanels(historyMessage));
                                                  });
                                              },
                                              DefaultErrorHandler);
        }

        private void UpdateChatLabel(string other)
        {
            this.currentChannel = this.chatPanels.Keys.FirstOrDefault(ch => ch.Split(' ').Contains(other));
            this.ChatContent.Content = this.chatPanels[this.currentChannel];
            this.CurrentChat.Children[0].CastTo<TextBlock>().Text = "Currently chatting with: " + other;
        }

        public void UpdateChatPanels(Message msg)
        {
            if (!this.chatPanels.ContainsKey(this.currentChannel))
            {
                this.chatPanels.Add(this.currentChannel, new StackPanel());
            }

            var userTemplate = this.uiElements.MessageTemplate(msg.Sender == this.username, msg.Content);

            this.chatPanels[this.currentChannel].Children.Add(userTemplate);
        }

        private static void SetHoverColor(Border element, Color color, Color? colorOnLeave = null)
        {
            element.MouseEnter += (o, v) =>
            {
                element.Background = new SolidColorBrush(color);
            };

            element.MouseLeave += (o, v) =>
            {
                element.Background = new SolidColorBrush(colorOnLeave ?? Colors.White);
            };
        }

        private void Border_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown(42);
        }
    }
}
