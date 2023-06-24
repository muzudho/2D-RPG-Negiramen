namespace _2D_RPG_Negiramen;

public partial class MapExplorerPage : ContentPage
{
	public MapExplorerPage()
	{
		InitializeComponent();
	}

    async void HomeBtn_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//MainPage");
    }

    /// <summary>
    /// ［項目をダブルクリック］ボタン押下時
    /// </summary>
    /// <param name="sender">このイベントを呼び出したコントロール</param>
    /// <param name="e">この発生イベントの制御変数</param>
    async void DoubleClickItemBtn_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//CreateMapViewPage");
    }
}