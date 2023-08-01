namespace _2D_RPG_Negiramen.Views;

/// <summary>
///     😁 編集アイテム・ページ
/// </summary>
public partial class EditItemPage : ContentPage
{
    public EditItemPage()
    {
        InitializeComponent();
    }

    async void HomeBtn_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//HomePage");
    }
}