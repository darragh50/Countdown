namespace Countdown;

public partial class Introduction : ContentPage
{
    public Introduction()
    {
        InitializeComponent();
        CreateUI();
    }
    private void CreateUI()
    {
        this.BackgroundColor = Color.FromArgb(DefaultConstants.GetBackgroundColor());
        layout.Children.Add(CreateStyledButton("Continue", OnButtonClicked, 1));
    }
    private static Button CreateStyledButton(string text, EventHandler handler, int buttonType)
    {
      var button = new Button

      {
          Text = text,
          BackgroundColor = Color.FromArgb(DefaultConstants.GetButtonBackgroundColor(buttonType)),
          TextColor = Colors.White,
          FontSize = 20,
          HeightRequest = 60,
          WidthRequest = 250,
          CornerRadius = 10,
          Margin = 5,
          HorizontalOptions = LayoutOptions.Center
      };

    if (handler != null)
    {
         button.Clicked += handler;
    }
       return button;
    }

    //Navigate to login page
    private void OnButtonClicked(object sender, EventArgs e)
    {
       Navigation.PushAsync(new Login());
    }
}
