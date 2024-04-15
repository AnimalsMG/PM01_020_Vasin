using Demo_Event.DataBase;
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
    /// Логика взаимодействия для PartipiantsListPage.xaml
    /// </summary>
    public partial class PartipiantsListPage : Page
    {
        public PartipiantsListPage()
        {
            InitializeComponent();
            DGridPartipiants.ItemsSource = Entities.GetContext().Parcipiants.ToList();
        }
    }
}
