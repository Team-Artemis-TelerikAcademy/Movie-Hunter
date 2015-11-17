using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using MovieHunter.Messenger.DeserializationResults;
using Newtonsoft.Json;

namespace MovieHunter.Messenger
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            var authKey = this.Login(this.Username.Text, this.Password.Text);
            if (authKey == string.Empty)
            {
                this.LoginInformation.Text = "Incorrect username or password!";
                return;
            }

            var view = new MessengerWindow(this.Username.Text, authKey);

            view.Show();

            this.Close();
        }

        private string Login(string username, string password)
        {
            var requester = new DataRequester();

            var postData = string.Format("grant_type=password&username={0}&password={1}", username, password);
            var response = requester.Request("http://localhost:52189/api/Account/Token", postData);

            var auth = JsonConvert.DeserializeObject<AuthInfo>(response).access_token;

            return auth;
        }

        private KeyValuePair<string, string> Header(string key, string value)
        {
            return new KeyValuePair<string, string>(key, value);
        }

        private void Username_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
