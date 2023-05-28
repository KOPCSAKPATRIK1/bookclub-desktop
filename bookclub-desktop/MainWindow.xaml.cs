using MySql.Data.MySqlClient;
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

namespace bookclub_desktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DBHelper dbHelper;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                dbHelper = new DBHelper();
                members.ItemsSource = dbHelper.ReadMembers();
            }
            catch (MySqlException ex)
            {

                MessageBox.Show(ex.Message);
                this.Close();
            }
        }

        private void TiltasButton_Click(object sender, RoutedEventArgs e)
        {
            Member member = members.SelectedItem as Member;
            if (member == null)
            {
                MessageBox.Show("Tiltas modositasahoz elobb valasszon ki klibbtagod");
                return;
            }

            string message = member.Banned ?
                "Biztos szeretne visszavonni a kivalasztott klubtag tiltasat?" :
                "Biztos szeretne kitiltani a kivalasztott klubtagot?";
           MessageBoxResult result = MessageBox.Show(message, "Biztos?",MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                UpdateBanned(member);
            }
        }

        private void UpdateBanned(Member member)
        {
            try
            {
                if (dbHelper.UpdateBnned(member))
                {
                    MessageBox.Show("Sikeres modositasz");
                }
                else
                {
                    MessageBox.Show("Sikertelen modositas");
                }
                members.ItemsSource = dbHelper.ReadMembers();

            }
            catch (MySqlException ex)
            {

                MessageBox.Show("Hiba totrtent a modositas soran");
            }
        }
    }
}
