namespace _2D_RPG_Negiramen.Views;

/// <summary>
///     😁 作成メニュー・ビュー・ページ
/// </summary>
public partial class CreateMenuViewPage : ContentPage
{
    public CreateMenuViewPage()
    {
        InitializeComponent();
    }

    async void HomeBtn_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//HomePage");
    }
}