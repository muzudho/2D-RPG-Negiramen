/// <summary>
///     😁 ビュー
/// </summary>
namespace _2D_RPG_Negiramen.Views;

public partial class CreateTalkingViewPage : ContentPage
{
    public CreateTalkingViewPage()
    {
        InitializeComponent();
    }

    async void HomeBtn_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//MainPage");
    }
}