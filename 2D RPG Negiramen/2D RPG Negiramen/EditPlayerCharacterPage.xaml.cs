namespace _2D_RPG_Negiramen;

public partial class EditPlayerCharacterPage : ContentPage
{
	public EditPlayerCharacterPage()
	{
		InitializeComponent();
	}

    async void HomeBtn_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//MainPage");
    }
}