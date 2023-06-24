namespace _2D_RPG_Negiramen;

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
        await Shell.Current.GoToAsync("//MainPage");
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