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
    /// Interaction logic for Cafe.xaml
    /// </summary>
    public partial class Cafe : Page
    {
        public Cafe()
        {
            InitializeComponent();

            LoadDataToListBox();
        }
        string filePath = "ordersinfofile.json";
        private void BN_Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                string newItem = $"{TB_OrderName.Text} + {TB_Desc.Text} + {TB_Price.Text}";
                if (!string.IsNullOrWhiteSpace(newItem))
                {
                    LB_Items.Items.Add(newItem);

                    MessageBox.Show($"{newItem}");

                    string json = ListBoxToJsonSerializer(LB_Items);
                    File.WriteAllText(filePath, json);

                    TB_OrderName.Clear();
                    TB_Desc.Clear();
                    TB_Price.Clear();
                }
            }
            catch (Exception ex) { MessageBox.Show($"Ошибка! {ex}"); }
        }

        private string ListBoxToJsonSerializer(ListBox listBox)
        {
            var items = new List<string>();
            foreach (var item in LB_Items.Items)
            {
                items.Add(item.ToString());
            }
            return JsonConvert.SerializeObject(items);
        }

        private void JsonToListboxDeserializer(ListBox listbox, string jsonString)
        {
            var items = JsonConvert.DeserializeObject<List<string>>(jsonString);
            foreach (var item in items)
            {
                listbox.Items.Add(item);
            }
        }

        private void LoadDataToListBox()
        {
            try
            {
                string json = File.ReadAllText(filePath);
                JsonToListboxDeserializer(LB_Items, json);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}");
            }
        }
        private void BN_Del_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LB_Items.Items.RemoveAt(LB_Items.SelectedIndex);

                string json = ListBoxToJsonSerializer(LB_Items);
                File.WriteAllText(filePath, json);
            }
            catch(Exception ex) { }

        }
    }
}
