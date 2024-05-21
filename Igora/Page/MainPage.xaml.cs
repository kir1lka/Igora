using Igora.Db;
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
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : System.Windows.Controls.Page
    {
        public MainPage()
        {
            InitializeComponent();
            DataGrid.ItemsSource = igoraDb.GetContext().Zakas.ToList();
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e) => UpdateClients();


        private void UpdateClients()
        {
            List<Zakas> zakas = igoraDb.GetContext().Zakas.ToList();

            if (ComboBoxSearch != null && !string.IsNullOrEmpty(SearchTextBox.Text))
            {
                switch (ComboBoxSearch.SelectedIndex)
                {
                    case 0: { zakas = zakas.Where(d => d.Client.fullName.ToLower().StartsWith(SearchTextBox.Text.ToLower())).ToList(); break; }

                }
                if (DataGrid != null)
                {
                    DataGrid.ItemsSource = zakas.ToList();

                    if (zakas.Count == 0 && !string.IsNullOrEmpty(SearchTextBox.Text))
                    {
                        MessageBox.Show("По вашему запросу ничего не найдено");
                        SearchTextBox.Text = "";
                        DataGrid.ItemsSource = igoraDb.GetContext().Zakas.ToList();
                    }
                }
            }
            if (string.IsNullOrEmpty(SearchTextBox.Text))
            {
                DataGrid.ItemsSource = igoraDb.GetContext().Zakas.ToList();
            }

        }
    }
}
