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
using ClientApp.Model;
using ClientApp.Model.Validation;
using Newtonsoft.Json;

namespace ClientApp
{
    /// <summary>
    /// Логика взаимодействия для Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        public Register()
        {
            InitializeComponent();
            SubmitButton.Click += SubmitButtonClicked;
        }
        private void SubmitButtonClicked(object sender, RoutedEventArgs e)
        {
            if(!int.TryParse(AgeField.Text, out _))
            {
                MessageBox.Show("Age is incorrect");
                return;
            }
            if(PasswordField.Password != ConfirmPasswordField.Password)
            {
                MessageBox.Show("There are 2 different passwords!");
                return;
            }

            try
            {
                var factory = new ValidatorFactory();

                factory.AddBlankLinesValidator(new string[]
                {
                    NameField.Text,
                    SurnameField.Text,
                    EmailField.Text,
                    PasswordField.Password,
                    ConfirmPasswordField.Password
                });

                factory.AddUserDataValidator(EmailField.Text, false);

                factory.ValidateAll();

                if(!factory.AllValidationsCompleted)
                {
                    MessageBox.Show(factory.FailedValidationInfo.Message);
                    return;
                }
                
                var user = new User()
                {
                    Name = NameField.Text,
                    Surname = SurnameField.Text,
                    Age = int.Parse(AgeField.Text),
                    EmailAdress = EmailField.Text,
                    Password = PasswordField.Password
                };

                Api.AddUser(user);
                Close();
            }
            catch
            {
                MessageBox.Show("There are some problems with input data. Try to avoid letters not from English alphabet");
            }
        }
    }
}
