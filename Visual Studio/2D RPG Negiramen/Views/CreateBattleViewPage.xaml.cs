namespace _2D_RPG_Negiramen.Views;

/// <summary>
///     😁 作成バトル・ビュー・ページ
/// </summary>
public partial class CreateBattleViewPage : ContentPage
{
	public CreateBattleViewPage()
	{
		InitializeComponent();
	}

    async void HomeBtn_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//HomePage");
    }
}