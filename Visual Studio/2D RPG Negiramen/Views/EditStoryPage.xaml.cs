namespace _2D_RPG_Negiramen.Views;

/// <summary>
///     😁 編集ストーリー・ページ
/// </summary>
public partial class EditStoryPage : ContentPage
{
    public EditStoryPage()
    {
        InitializeComponent();
    }

    async void HomeBtn_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//HomePage");
    }
}