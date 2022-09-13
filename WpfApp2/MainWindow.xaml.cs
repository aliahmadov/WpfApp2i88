using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

        public bool test()
        {
            try
            {
                string address = $@"http://www.google.com";
                WebRequest request1 = WebRequest.Create(address);
                WebResponse response = request1.GetResponse();
            }
            catch (Exception)
            {

                return false;
            }
            return true;
        }

        private string TranslateToEnglish(string text)
        {
            var client = TranslationClient.CreateFromApiKey("AIzaSyC1gksuh6zdF1v-usvS4C90mZU5C2VRL5I");
            var entext = client.TranslateText(text, LanguageCodes.English, LanguageCodes.Azerbaijani);
            return entext.TranslatedText;

        }

        private string TranslateToAzerbaijani(string text)
        {
            var client = TranslationClient.CreateFromApiKey("AIzaSyC1gksuh6zdF1v-usvS4C90mZU5C2VRL5I");
            var azText = client.TranslateText(text, LanguageCodes.Azerbaijani, LanguageCodes.English);
            return azText.TranslatedText;
        }

        public double X { get; set; }
        public double Y { get; set; }
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


            MessageUC myMessage = new MessageUC();
            myMessage.messageTxt.Text=myTxtBox.Text;
            myGrid.Children.Add(myMessage);
            Canvas.SetLeft(myMessage, 250);
            //Canvas.SetTop(myGrid, Y);


            //MessageUC messageUC = new MessageUC();
            //messageUC.messageTxt.Text = azeText;
            //myGrid.Children.Add(messageUC);
            //Canvas.SetLeft(myGrid, 400);
            //Canvas.SetTop(myGrid, Y+10);


           // myLabel.Content =azeText;


        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (test())
            {
                this.Title = "Connection Exist";
            }
            else
            {
                this.Title = "No Connection";
            }
        }
    }
}
