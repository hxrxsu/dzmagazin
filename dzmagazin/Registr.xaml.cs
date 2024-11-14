using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace dzmagazin
{
    /// <summary>
    /// Interaction logic for Registr.xaml
    /// </summary>
    public partial class Registr : Page
    {
        public Registr()
        {
            InitializeComponent();
        }

        string filePath = "users.json";


        private void BN_Auth_Click(object sender, RoutedEventArgs e)
        {
            List<Person> userdata = JsonConvert.DeserializeObject<List<Person>>(File.ReadAllText(filePath)) ?? new List<Person>();
            var user = userdata.FirstOrDefault(u => u.login == TB_Login.Text && u.password == TB_Pass.Text);

            if (user != null)
            {
                MessageBox.Show("Успешно! Привет " + user.login);

                NavigationService.Navigate(new Cafe());
            }
            else
            {
                MessageBox.Show("Неправильный логин или пароль");
            }
        }

        private void Label_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    File.Create(filePath).Close();
                }




                List<Person> userdata = JsonConvert.DeserializeObject<List<Person>>(File.ReadAllText(filePath)) ?? new List<Person>();
                userdata.Add(new Person { login = TB_Login.Text, password = TB_Pass.Text });
                string json = JsonConvert.SerializeObject(userdata);
                File.WriteAllText(filePath, json);

                MessageBox.Show("Вы зарегистрировались");
            }
            catch (Exception ex) { MessageBox.Show($"Ошибка {ex}"); }
        }
    }
}
