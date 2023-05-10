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
using System.Windows.Threading;

namespace StudentPr18Generalskiy
{
    /// <summary>
    /// Логика взаимодействия для Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
            Height += 25;
        }
        public User User { get; private set; }
        private void Window_Activated(object sender, EventArgs e)
        {
            tbLogin.Focus();
            Data.Login = false;
            _timer = new DispatcherTimer();
            _timer.Interval = new TimeSpan(0, 0, 10);
            _timer.Tick += new EventHandler(timer_Tick);
        }
        DispatcherTimer _timer;
        int _countLogin = 1;
        StudentsEntities _db = StudentsEntities.GetContext();
        
        void GetCaptcha()
        {
            string masChar = "QWERTYUIOPLKJHGFDSAZXCVBNMmnbvcxzasdfghjk" +
                "lpoiuytrewq1234567890";
            string captcha = "";
            Random rnd = new Random();
            for(int i = 1; i <= 6; i++)
            {
                captcha = captcha + masChar[rnd.Next(0, masChar.Length)];
            }
            Grid.Visibility = Visibility.Visible;
            txtCaprcha.Text = captcha;
            tbCaptcha.Text = null;
            txtCaprcha.LayoutTransform = new RotateTransform(rnd.Next(-15, 15));
            line.X1 = 10;
            line.Y1 = rnd.Next(10, 40);
            line.X2 = 280;
            line.Y2 = rnd.Next(10, 40);
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            stackPanel.IsEnabled = true;
        }
        private void Enter_Click(object sender, RoutedEventArgs e)
        {
            var user = from p in _db.Users
                       where p.UserLogin == tbLogin.Text 
                       && p.UserPassword.ToString() == tbPassword.Password
                       select p;
            if(user.Count() == 1 && txtCaprcha.Text == tbCaptcha.Text)
            {
                Data.Login = true;
                Data.Familia = user.First().UserSurname;
                Data.Name = user.First().UserName;
                Data.Otchestvo = user.First().UserPatronymic;
                Data.Right = user.First().Role.RoleName;
                Close();
            }
            else
            {
                if (user.Count() == 1)
                {
                    MessageBox.Show("Капча неверна, повторите ввод");
                }
                else
                {
                    MessageBox.Show("Логин и пароль неверны, повторите ввод");
                }
                GetCaptcha();
                if(_countLogin >= 2)
                {
                    stackPanel.IsEnabled = false;
                    _timer.Start();
                }
                _countLogin++;
                tbLogin.Focus();
            }
        }

        private void Esc_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Guest_Click(object sender, RoutedEventArgs e)
        {
            Data.Login = true;
            Data.Familia = "Гость";
            Data.Name = "";
            Data.Otchestvo = " ";
            Data.Right = "Клиент";
            Close();
        }
    }
}
