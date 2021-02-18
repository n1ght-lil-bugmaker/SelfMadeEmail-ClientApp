using ClientApp.Model;
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
using ClientApp.Model.Validation;

namespace ClientApp
{
    /// <summary>
    /// Логика взаимодействия для AddEmail.xaml
    /// </summary>
    public partial class AddEmail : Window
    {
        public AddEmail()
        {
            InitializeComponent();
            SendButton.Click += Add;
        }

        public AddEmail(string target, string theme)
        {
            InitializeComponent();
            Source.Text = target;
            Theme.Text = theme;

            Source.IsEnabled = false;
            Theme.IsEnabled = false;

            SendButton.Click += Add;
        }
        public void Add(object sender, EventArgs args)
        {
            var factory = new ValidatorFactory();

            factory.AddBlankLinesValidator(new string[] { Textarea.Text, Source.Text, Theme.Text })
                   .AddUserDataValidator(Source.Text, true);

            factory.ValidateAll();

            if(!factory.AllValidationsCompleted)
            {
                MessageBox.Show(factory.FailedValidationInfo.Message);
                return;
            }

            var email = new Email()
            {
                Target = Source.Text,
                Theme = Theme.Text,
                SendedTime = DateTime.Now,
                Source = MainWindow.CurrentUser.EmailAdress,
                Text = Textarea.Text
            };

            Api.AddEmail(email);
            Close();
        }
    }
}
