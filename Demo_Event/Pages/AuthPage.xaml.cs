using Demo_Event.DataBase;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Demo_Event.Pages
{
    /// <summary>
    /// Логика взаимодействия для AuthPage.xaml
    /// </summary>
    public partial class AuthPage : Page
    {

        private int attemptCount = 0;
        private const int maxAttempts = 3;
        private const int lockoutDurationSeconds = 10;
        private DateTime lockoutEndTime = DateTime.MinValue;
        private string validCaptcha = "ABCD";

        public AuthPage()
        {
            InitializeComponent();
        }


        private void BtnAuth_Click(object sender, RoutedEventArgs e)
        {
            AuthUsr(TBox.Text, TBoxPassword.Password);
        }

        public bool AuthUsr(string login, string password)
        {

            if (string.IsNullOrEmpty(login) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Введите логин и пароль");
                return false;
            }


            using (var db = new Entities())
            {
                var moderator = db.Moderators.AsNoTracking().FirstOrDefault(u => u.Id.ToString() == login && u.Password == password);
                if (AuthenticateUser(moderator, new ModeratorPage(), "", ""))
                    return true;



                var organizer = db.Organizators.AsNoTracking().FirstOrDefault(u => u.Id.ToString() == login && u.Password == password);

                if (organizer == null)
                {
                    MessageBox.Show("Логин или пароль неверный");
                    return false;
                }

                if (AuthenticateUser(organizer, new OrganisatorPage(), "", ""))
                    return true;

                var participant = db.Parcipiants.AsNoTracking().FirstOrDefault(u => u.Id.ToString() == login && u.Password == password);
                if (AuthenticateUser(participant, new PartipiantsPage(), "", ""))
                    return true;
            }

            // Если попытка входа неудачна, увеличиваем счетчик попыток
            attemptCount++;

            // Проверяем, достигнуто ли максимальное количество попыток, и блокируем при необходимости
            if (attemptCount >= maxAttempts)
            {
                // Блокируем систему на указанное время
                LockoutSystem();
                MessageBox.Show("Неверный логин или пароль. Пожалуйста, подождите несколько секунд и попробуйте снова.");
            }
            return true;
        }

        private bool ValidateCaptcha(string enteredCaptcha)
        {
            return enteredCaptcha == validCaptcha;
        }

        private bool IsLockedOut()
        {
            return DateTime.Now < lockoutEndTime;
        }

        private void LockoutSystem()
        {

            lockoutEndTime = DateTime.Now.AddSeconds(lockoutDurationSeconds);
        }

        private bool AuthenticateUser(object authenticatedUser, Page nextPage, string name, string patronymic)
        {
            if (authenticatedUser != null)
            {
                NavigationService?.Navigate(nextPage);
                MessageBox.Show("Добрый" + " " + GetPeriodOfDay(DateTime.Now) + " " + name + " " + patronymic + " " + "время работы сейчас: " + DateTime.Now);

                return true;
            }

            return false;
        }

        public static string GetPeriodOfDay(DateTime time)
        {
            if (time.TimeOfDay >= new TimeSpan(9, 0, 0) && time.TimeOfDay <= new TimeSpan(11, 0, 0))
            {
                return "Утро";
            }
            else if (time.TimeOfDay > new TimeSpan(11, 0, 0) && time.TimeOfDay <= new TimeSpan(18, 0, 0))
            {
                return "День";
            }
            else if (time.TimeOfDay > new TimeSpan(18, 0, 0) && time.TimeOfDay <= new TimeSpan(23, 59, 59))
            {
                return "Вечер";
            }
            else
            {
                return "Ночь";
            }
        }


    }
}
