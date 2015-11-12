﻿using System;
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

namespace ChatTest
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
            if(!this.Login(this.Username.Text, ""))
            {
                return;
            }

            var view = new MessengerWindow(this.Username.Text);

            view.Show();

            this.Close();
        }

        private bool Login(string username, string password)
        {
            var h = new List<KeyValuePair<string, string>>() { new KeyValuePair<string, string>("method", "get") };
            var requester = new DataRequester();
            
            var response = requester.Request("http://localhost:52189/api/Users?username=" + username);

            return response == "\"ok\"";
        }
    }
}
