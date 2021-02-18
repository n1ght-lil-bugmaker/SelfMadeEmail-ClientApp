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

namespace ClientApp
{
    /// <summary>
    /// Логика взаимодействия для FullEmail.xaml
    /// </summary>
    public partial class FullEmail : Window
    {
        public FullEmail(Email email)
        {
            InitializeComponent();

            ThemeLabel.Content = email.Theme;
            SendedLabel.Content = email.SendedTime.ToString("dd.MM.yyyy, HH:mm");
            Textarea.Text = email.Text;

            if(email.Target == MainWindow.CurrentUser.EmailAdress)
            {
                SenderLadel.Content = email.Source;
            }
            else
            {
                SenderOrReceiver.Content = "Receiver";
                SenderLadel.Content = email.Target;
                AnswerButton.IsEnabled = false;
            }
            
            AnswerButton.Click += (sender, args) =>
            {
                new AddEmail(email.Source, email.Theme).Show();
            };
        }
    }
}
