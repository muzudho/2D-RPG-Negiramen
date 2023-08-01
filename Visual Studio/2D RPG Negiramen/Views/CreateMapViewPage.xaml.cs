namespace _2D_RPG_Negiramen.Views;

/// <summary>
///     😁 作成マップ・ビュー・ページ
/// </summary>
public partial class CreateMapViewPage : ContentPage
{
    public CreateMapViewPage()
    {
        InitializeComponent();
    }

    /// <summary>
    ///     パンくずリスト
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    async void HomeBtn_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//HomePage");
    }

    /// <summary>
    ///     パンくずリスト
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    async void MapExplorerBtn_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//MapExplorerPage");
    }
}