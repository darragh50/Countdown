namespace Countdown
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            
            //Set start-up page to Introduction page
            MainPage = new NavigationPage(new Introduction());
        }
    }
}
