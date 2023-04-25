using System.Windows;
using System.Windows.Input;

namespace Wpf_GenyiIdiot.Pages
{
    /// <summary>
    /// Логика взаимодействия для EnterNamePage.xaml
    /// </summary>
    public partial class EnterNamePage : Window
    {
        
        public EnterNamePage()
        {
            InitializeComponent();
            inputTextBox.Focus();
        }

        void OnSaveButtonClick(object sender, RoutedEventArgs e)
        {
            if ( !string.IsNullOrWhiteSpace(inputTextBox.Text) || !string.IsNullOrEmpty(inputTextBox.Text) ) 
            {
                GamePage.Username = inputTextBox.Text;
            }
            this.Close();
        }

        void OnDontSaveButtonClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                OnSaveButtonClick(sender, e);
            }
            if (e.Key == Key.Escape)
            {
                OnDontSaveButtonClick(sender, e);
            }
        }
    }
}
