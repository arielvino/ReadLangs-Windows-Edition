using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ReadLangs
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string savedTextPath = Environment.CurrentDirectory + "/main_text.txt";
        string savedScrollPath = Environment.CurrentDirectory + "/savedScroll.txt";

        public MainWindow()
        {
            InitializeComponent();

            //load cached text:
            if (File.Exists(savedTextPath))
            {
                string text = File.ReadAllText(savedTextPath);
                mainTextBox.Text = text;
            }

            //load saved scroll:
            if (File.Exists(savedScrollPath))
            {
                string value = File.ReadAllText(savedScrollPath);
                double scrollValue = 0;
                double.TryParse(value, out scrollValue);
                mainTextBox.ScrollToVerticalOffset(scrollValue);
            }
        }

        private void mainTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //always save text to a file:
            File.WriteAllText(savedTextPath, mainTextBox.Text);
        }

        private async void mainTextBox_SelectionChanged(object sender, RoutedEventArgs e)
        {

            //this line is here because on startup this method is called before the translationView is initialized. Otherwise, a NullException will be thrown:
            if (translationView is not null)
            {
                translationView.Text = await App.Translator.TranslateAsync(FindWord());
            }
        }

        private void saveKeyButton_Click(object sender, RoutedEventArgs e)
        {
            GoogleApiTranslator.GoogleApiKey.Key = googleApiKeyInput.Text;
            googleApiKeyInput.Text = "";
            MessageBox.Show("Key saved");
        }

        private void mainTextBox_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            //always save scroll value to a file:
            File.WriteAllText(savedScrollPath, mainTextBox.VerticalOffset.ToString());
        }

        private string FindWord()
        {
            const string forbiddenCharacters = " ,./?<>()[]{}\\|+=-_!&'";

            int start = mainTextBox.SelectionStart;
            int end = mainTextBox.SelectionStart + mainTextBox.SelectionLength;

            //find word start:
            while(start > 0)
            {
                if (forbiddenCharacters.Contains(mainTextBox.Text[start - 1]))
                {
                    break;
                }
                else
                {
                    start--;
                }
            }

            //find word end:
            while(end < mainTextBox.Text.Length)
            {
                if (forbiddenCharacters.Contains(mainTextBox.Text[end]))
                {
                    break;
                }
                else
                {
                    end++;
                }
            }

            return mainTextBox.Text.Substring(start, end - start);
        }
    }
}