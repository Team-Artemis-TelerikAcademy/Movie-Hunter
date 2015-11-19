namespace MovieHunter.Messenger
{
    using System;
    using System.Windows;
    using System.Windows.Input;
    using DeserializationResults;
    using Newtonsoft.Json;

    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private const string LoginFailMessage = "Incorrect username or password!";
        private const string TokenRequestUrl = "http://localhost:52189/api/Account/Token";
        private const string TokenRequestUrlParams = "grant_type=password&username={0}&password={1}";

        public LoginWindow()
        {
            InitializeComponent();
        }

        private void LoginButtonClick(object sender, RoutedEventArgs e)
        {
            var authKey = this.GetAuthToken(this.Username.Text, this.Password.Password);

            if (authKey == string.Empty)
            {
                this.LoginInformation.Text = LoginFailMessage;
                return;
            }

            var view = new MessengerWindow(this.Username.Text, authKey, new DataRequester(), new UIComponentProvider());

            view.Show();

            this.Close();
        }

        private string GetAuthToken(string username, string password)
        {
            var requester = new DataRequester();

            var postData = string.Format(TokenRequestUrlParams, username, password);
            var response = requester.Request(TokenRequestUrl, postData);

            var auth = JsonConvert.DeserializeObject<AuthInfo>(response).access_token;

            return auth;
        }

        private void AppExit(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown(42);
        }
    }
}
