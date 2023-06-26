namespace _2D_RPG_Negiramen.Views;

public partial class StartupConfigurationPage : ContentPage
{
	public StartupConfigurationPage()
	{
		InitializeComponent();
	}

    async void HomeBtn_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//MainPage");
    }
}