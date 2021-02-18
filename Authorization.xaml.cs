using ClientApp.Model;
using ClientApp.Model.Validation;
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

namespace ClientApp
{
    /// <summary>
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Window
    {
        public User AuthorizedUser { get; private set; }
        public Authorization()
        {
            InitializeComponent();
            SubmitButton.Click += AuthorizedButtonPressed;
            RegisterButton.Click += RegisterButtonPreessed;
        }
        public void AuthorizedButtonPressed(object sender, EventArgs args)
        {
            var factory = new ValidatorFactory();

            factory.AddBlankLinesValidator(new string[] { UsermaneField.Text, PasswordField.Password })
                   .AddPasswordValidator(UsermaneField.Text, PasswordField.Password);

            factory.ValidateAll();

            if(!factory.AllValidationsCompleted)
            {
                MessageBox.Show(factory.FailedValidationInfo.Message);
                return;
            }

            MainWindow.CurrentUser = Api.GetUser(UsermaneField.Text);
            Close();
        }

        public void RegisterButtonPreessed(object sender, EventArgs args)
        {
            new Register().Show();
        }        
    }
}
