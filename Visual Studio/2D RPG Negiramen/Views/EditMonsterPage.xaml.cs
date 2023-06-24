namespace _2D_RPG_Negiramen;

public partial class EditMonsterPage : ContentPage
{
    public EditMonsterPage()
    {
        InitializeComponent();
    }

    async void HomeBtn_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//MainPage");
    }
}