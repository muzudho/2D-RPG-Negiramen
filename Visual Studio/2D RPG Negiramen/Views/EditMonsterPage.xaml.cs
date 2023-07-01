namespace _2D_RPG_Negiramen.Views;

/// <summary>
///     😁 編集モンスター・ページ
/// </summary>
public partial class EditMonsterPage : ContentPage
{
    public EditMonsterPage()
    {
        InitializeComponent();
    }

    async void HomeBtn_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//MainPage");
    }
}