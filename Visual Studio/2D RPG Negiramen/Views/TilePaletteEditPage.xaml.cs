namespace _2D_RPG_Negiramen.Views;

/// <summary>
///     😁 タイル・パレット編集ページ
/// </summary>
public partial class TilePaletteEditPage : ContentPage
{
	public TilePaletteEditPage()
	{
		InitializeComponent();
	}
    async void HomeBtn_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//MainPage");
    }
}