using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;
using AIMLbot;
using DarrenLee.Translator;
using Google.Cloud.Translation.V2;



namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();


        }


        private string TranslateToEnglish(string text)
        {
            var client = TranslationClient.CreateFromApiKey("AIzaSyAaThOnUeV-iuFeTCVz1KCtBsMPLbvv7jY");
            var entext = client.TranslateText(text, LanguageCodes.English, LanguageCodes.Azerbaijani);
            return entext.TranslatedText;

        }

        private string TranslateToAzerbaijani(string text)
        {
            var client = TranslationClient.CreateFromApiKey("AIzaSyAaThOnUeV-iuFeTCVz1KCtBsMPLbvv7jY");
            var azText = client.TranslateText(text, LanguageCodes.Azerbaijani, LanguageCodes.English);
            return azText.TranslatedText;
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Bot AI = new Bot();
            AI.loadSettings();
            AI.loadAIMLFromFiles();
            AI.isAcceptingUserInput = false;
            User user = new User("Username here", AI);
            AI.isAcceptingUserInput = true;
            string english_test = TranslateToEnglish(myTxtBox.Text);
            Request request = new Request(english_test, user, AI);
            Result result = AI.Chat(request);
            string azeText = TranslateToAzerbaijani(result.Output);

            MessageUC messageUC = new MessageUC();
            messageUC.MessageText = myTxtBox.Text;
            int l = myTxtBox.Text.Length;

            if (myTxtBox.Text.Length > 14)
            {
                for (int i = 0; i < l / 14; i++)
                {
                    messageUC.Height += 10;
                    messageUC.textBlock.Height += 45;
                }
            }
            messageUC.HorizontalAlignment = HorizontalAlignment.Right;
            stack.Children.Add(messageUC);


            MessageUC myMessage = new MessageUC();
            myMessage.MessageText = azeText;
            int L = azeText.Length;
            
            if (myTxtBox.Text.Length > 14)
            {
                for (int i = 0; i < L / 14; i++)
                {
                    myMessage.Height += 10;
                    myMessage.textBlock.Height += 45;
                }
            }
            myMessage.HorizontalAlignment = HorizontalAlignment.Left;
            stack.Children.Add(myMessage);


            myTxtBox.Text = "";
        }

    }
}
