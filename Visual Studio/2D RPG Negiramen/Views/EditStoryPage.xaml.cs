/// <summary>
///     😁 ビュー
/// </summary>
namespace _2D_RPG_Negiramen.Views;

public partial class EditStoryPage : ContentPage
{
    public EditStoryPage()
    {
        InitializeComponent();
    }

    async void HomeBtn_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//MainPage");
    }
}