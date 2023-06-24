namespace _2D_RPG_Negiramen;

public partial class MainPage : ContentPage
{
	int count = 0;

	public MainPage()
	{
		InitializeComponent();
	}

	private void OnCounterClicked(object sender, EventArgs e)
	{
		/*
		count++;

		if (count == 1)
			CounterBtn.Text = $"Clicked {count} time";
		else
			CounterBtn.Text = $"Clicked {count} times";

		SemanticScreenReader.Announce(CounterBtn.Text);
		*/
	}

	/// <summary>
	/// ［マップを作る］ボタン押下時
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
    async void CreateMapBtn_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//CreateMapPage");
    }
}

