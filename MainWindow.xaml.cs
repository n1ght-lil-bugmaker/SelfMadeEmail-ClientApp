using ClientApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace ClientApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static User CurrentUser;
        public MainWindow()
        {
            InitializeComponent();

            Hide();
            var authorizedWindow = new Authorization();
            authorizedWindow.ShowDialog();

            if(CurrentUser == null)
            {
                Close();
            }

            Show();
            Title = CurrentUser.EmailAdress;

            GetReceivedEmails();

            AddButton.Click += AddButtonClick;
            DeleteButton.Click += DeleteButtonClick;
            DGrid.MouseDoubleClick += DGridMouseDoubleClick;
            LogoutButton.Click += LogoutButtonClick;
            MessagesType.SelectionChanged += MessagesTypeSelectionChanged;
        }

        private void AddButtonClick(object sender, RoutedEventArgs e)
        {
            var addWindow = new AddEmail();
            addWindow.Closing += (sender1, args1) => { RefreshEmails(); };
            addWindow.Show();
        }
        private void DeleteButtonClick(object sender, RoutedEventArgs e)
        {
            var selected = DGrid.SelectedItem as Email;
            if (selected != null)
            {
                Api.DeleteEmail(selected);
                RefreshEmails();
            }
        }
        private void DGridMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            new FullEmail(DGrid.SelectedItem as Email).Show();
        }
        private void MessagesTypeSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var binding = new Binding();

            if (MessagesType.SelectedIndex == 0)
            {
                SenderColumn.Header = "Sender";
                binding.Path = new PropertyPath("Source");
            }
            else
            {
                SenderColumn.Header = "Receiver";
                binding.Path = new PropertyPath("Target");
            }
            SenderColumn.Binding = binding;
            RefreshEmails();
        }
        private void LogoutButtonClick(object sender, RoutedEventArgs e)
        {
            Hide();
            CurrentUser = null;
            new MainWindow().Show();
            Close();
        }
        private void RefreshEmails()
        {
            if (MessagesType.SelectedIndex == 0)
            {
                GetReceivedEmails();
            }
            else
            {
                GetSendedEmails();
            }
        }
        private void GetReceivedEmails()
        {
            DGrid.ItemsSource = new ObservableCollection<Email>(Api.GetReceivedEmails(CurrentUser.EmailAdress));
        }
        private void GetSendedEmails()
        {
            DGrid.ItemsSource = new ObservableCollection<Email>(Api.GetSendedEmails(CurrentUser.EmailAdress));
        }
    }
}
