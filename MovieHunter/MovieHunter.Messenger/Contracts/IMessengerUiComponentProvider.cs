namespace MovieHunter.Messenger.Contracts
{
    using System.Windows.Controls;

    public interface IMessengerUiComponentProvider
    {
        TextBlock ChatLabel(string username);
        Border ContactTemplate(string username);
        Border MessageTemplate(bool sentByLocalUser, string content);
        Border OpenChatLabel(string name);
    }
}