namespace MovieHunter.Messenger
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

    public class UIComponentProvider
    {
        public Border ContactTemplate(string username)
        {
            var contact = new Border()
            {
                BorderThickness = new Thickness(2),
                BorderBrush = new SolidColorBrush(Colors.CornflowerBlue),
                CornerRadius = new CornerRadius(10),
                Margin = new Thickness(0, 10, 0, 0),
                Child = new TextBlock()
                {
                    Text = username,
                    Height = 30,
                    Padding = new Thickness(10)
                }
            };

            return contact;
        }

        public TextBlock ChatLabel(string username)
        {
            var label = new TextBlock()
            {
                Text = "Currently chatting with: " + username,
                Foreground = new SolidColorBrush(Colors.CadetBlue)
            };

            return label;
        }

        public Border OpenChatLabel(string name)
        {
            var label = new Border()
            {
                Child = new TextBlock()
                {
                    Text = name,
                    Margin = new Thickness(2)
                },
                BorderBrush = new SolidColorBrush(Colors.AntiqueWhite),
                BorderThickness = new Thickness(1)
            };

            return label;
        }

        public Border MessageTemplate(bool sentByLocalUser, string content)
        {
            var message = new Border()
            {
                Background = new SolidColorBrush(sentByLocalUser ? Colors.CornflowerBlue : Colors.Gray),
                CornerRadius = new CornerRadius(10),
                Margin = new Thickness(5, 10, 5, 0),
                Width = 200,
                Child = new TextBlock()
                {
                    Text = content,
                    Foreground = new SolidColorBrush(Colors.White),
                    FontWeight = FontWeights.Bold,
                    Height = 30,
                    Padding = new Thickness(10),
                    HorizontalAlignment = sentByLocalUser ? HorizontalAlignment.Left : HorizontalAlignment.Right
                },
                HorizontalAlignment = sentByLocalUser ? HorizontalAlignment.Left : HorizontalAlignment.Right
            };

            return message;
        }
    }
}