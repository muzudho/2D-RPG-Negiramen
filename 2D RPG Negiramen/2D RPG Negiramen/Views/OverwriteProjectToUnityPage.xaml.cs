namespace _2D_RPG_Negiramen;

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

    /// <summary>
    /// ［Unityへプロジェクトを上書き］ボタン押下時
    /// </summary>
    /// <param name="sender">このイベントを呼び出したコントロール</param>
    /// <param name="e">この発生イベントの制御変数</param>
    private void OverwriteProjectToUnityBtn_Clicked(object sender, EventArgs e)
    {
        // TODO テキスト・ボックスの値を取得

        // TODO Unityへプロジェクトを上書き
    }
}