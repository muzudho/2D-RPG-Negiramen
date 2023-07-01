namespace _2D_RPG_Negiramen.Views;

/// <summary>
///     😁 編集・プレイヤー・キャラクター・ページ
/// </summary>
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