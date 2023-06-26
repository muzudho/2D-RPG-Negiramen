namespace _2D_RPG_Negiramen.Views;

public partial class OverwriteProjectToUnityPage : ContentPage
{
    public OverwriteProjectToUnityPage()
    {
        InitializeComponent();
    }

    async void HomeBtn_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//MainPage");
    }
}