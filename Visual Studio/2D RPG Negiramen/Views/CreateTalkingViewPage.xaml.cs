namespace _2D_RPG_Negiramen.Views;

/// <summary>
///     😁 作成会話ビュー・ページ
/// </summary>
public partial class CreateTalkingViewPage : ContentPage
{
    public CreateTalkingViewPage()
    {
        InitializeComponent();
    }

    async void HomeBtn_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//HomePage");
    }
}