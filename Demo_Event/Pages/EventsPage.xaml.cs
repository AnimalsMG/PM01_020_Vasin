using Demo_Event.DataBase;
using Demo_Event.Model;
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

namespace Demo_Event.Pages
{
    /// <summary>
    /// Логика взаимодействия для EventsPage.xaml
    /// </summary>
    public partial class EventsPage : Page
    {
        public EventsPage()
        {
            InitializeComponent();

            LViewEvents.ItemsSource = Entities.GetContext().Event.ToList();
        }

        private void BtnAuth_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AuthPage());
        }
    }
}
