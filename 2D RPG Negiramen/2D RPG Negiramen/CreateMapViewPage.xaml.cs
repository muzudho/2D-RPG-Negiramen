namespace _2D_RPG_Negiramen;

public partial class CreateMapViewPage : ContentPage
{
	public CreateMapViewPage()
	{
		InitializeComponent();
	}

    async void HomeBtn_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//MainPage");
    }
}