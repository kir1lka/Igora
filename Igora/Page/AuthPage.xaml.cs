using Igora.Db;
using Igora.Properties;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Igora.Page
{
    /// <summary>
    /// Логика взаимодействия для AuthPage.xaml
    /// </summary>
    public partial class AuthPage : System.Windows.Controls.Page
    {
        igoraDb db = new igoraDb();
        int count = 0;
        Random _random = new Random();
        string stringCaptcha;

        public AuthPage()
        {
            InitializeComponent();
            UpdateCaptcha();
        }

        private async void Auth_click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(PasswordTB.Password) | !string.IsNullOrEmpty(LoginTB.Text))
            {
                if (CapthaTextbox.Text == stringCaptcha)
                {
                    if (db.Sotrudnik.Select(user => user.Login + " " + user.Password).Contains(LoginTB.Text + " " + PasswordTB.Password))
                    {
                        MessageBox.Show("Добро пожаловать");
                        NavigationService.Navigate(new MainPage());
                        return;
                    }
                    count++;

                    if (count == 3)
                    {
                        LoginTB.IsEnabled = false;
                        PasswordTB.IsEnabled = false;
                        BtnLog.IsEnabled = false;
                        MessageBox.Show($"вы заблокированы ");
                        await Task.Delay(1000);
                        LoginTB.IsEnabled = true;
                        PasswordTB.IsEnabled = true;
                        BtnLog.IsEnabled = true;

                        count = 0;

                        return;
                    }
                    else
                    {
                        MessageBox.Show($"не правильные данные, попыток {3 - count}");
                    }

                }

                else MessageBox.Show("Ошибка при вводе символов!");
                }
            else MessageBox.Show("Заполните поля!");



        }

        private void Update_Captcha_Button_Click(object sender, RoutedEventArgs e) => UpdateCaptcha();
        private void UpdateCaptcha()
        {
            stringCaptcha = "";
            Symbols_panel.Children.Clear();
            Noise_Canvas.Children.Clear();

            GenerateSymbols(4);
        }
        private void GenerateSymbols(int count)
        {
            string alphabet = "WERPASFHKXVBM2345678";

            for (int i = 0; i <= count; i++)
            {
                char symbol = alphabet.ElementAt(_random.Next(0, alphabet.Length));
                TextBlock lbl = new TextBlock();

                lbl.Text = symbol.ToString();
                lbl.Foreground = new SolidColorBrush(Color.FromRgb((byte)_random.Next(100, 256), (byte)_random.Next(100, 256), (byte)_random.Next(100, 256)));
                lbl.FontSize = _random.Next(20, 50);
                lbl.RenderTransform = new RotateTransform(_random.Next(-45, 45));
                lbl.Margin = new Thickness(10, 10, 10, 0);

                stringCaptcha += symbol;
                Symbols_panel.Children.Add(lbl);
            }
        }
       
    }
}
