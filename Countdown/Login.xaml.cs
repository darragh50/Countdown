using Microsoft.Maui.Controls;

namespace Countdown
{
    public partial class Login : ContentPage
    {
        private Entry Username;
        private Entry Password;
        private Button LoginBut;

        public Login()
        {
            InitializeComponent();

            this.BackgroundColor = Color.FromArgb(DefaultConstants.GetBackgroundColor());

            CreateUI();

            LoginBut.Clicked += LoginButtonClicked;
        }

        private void CreateUI()
        {
            Label headerLabel = new Label
            {
                Text = "Login",
                FontSize = 30,
                TextColor = Colors.White,
                HorizontalTextAlignment = TextAlignment.Center,
                Margin = new Thickness(0, 0, 0, 20)
            };

            Username = new Entry
            {
                Placeholder = "Username",
                HorizontalOptions = LayoutOptions.Center,
                PlaceholderColor = Colors.LightGray,
                WidthRequest = 240,
                Margin = new Thickness(0, 0, 0, 10),
                FontSize = 16
            };

            Password = new Entry
            {
                Placeholder = "Password",
                IsPassword = true,
                HorizontalOptions = LayoutOptions.Center,
                PlaceholderColor = Colors.LightGray,
                WidthRequest = 240,
                Margin = new Thickness(0, 0, 0, 20),
                FontSize = 16
            };

            LoginBut = new Button
            {
                Text = "Login",
                TextColor = Colors.White,
                BackgroundColor = Color.FromArgb(DefaultConstants.GetButtonBackgroundColor(1)),
                FontSize = 18,
                HorizontalOptions = LayoutOptions.Center,
                WidthRequest = 120,
                Margin = new Thickness(0, 0, 0, 0)
            };

            Content = new VerticalStackLayout
            {
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Children = { headerLabel, Username, Password, LoginBut }
            };
        }

        private async void LoginButtonClicked(object sender, EventArgs e)
        {

            string username = Username.Text;
            string password = Password.Text;


            if (!string.IsNullOrWhiteSpace(username) && !string.IsNullOrWhiteSpace(password))
            {
                await Navigation.PushAsync(new MainPage());
            }
            else
            {

                await DisplayAlert("Login Error", "Invalid username or password.", "OK");
            }
        }
    }
}
