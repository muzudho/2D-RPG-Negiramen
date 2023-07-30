namespace _2D_RPG_Negiramen.Views;

using _2D_RPG_Negiramen.ViewModels;
using System.Diagnostics;

public partial class Login2Page : ContentPage
{
    // - その他

    #region その他（生成）
    /// <summary>
    ///     生成
    /// </summary>
	public Login2Page()
	{
		InitializeComponent();
	}
    #endregion

    // - インターナル・プロパティ

    #region プロパティ（ビューモデル）
    /// <summary>
    ///     ビューモデル
    /// </summary>
    internal ILogin2PageViewModel Login2PageVM => (ILogin2PageViewModel)this.BindingContext;
    #endregion

    // - プライベート・イベントハンドラ

    #region イベントハンドラ（ページ読込完了時）
    /// <summary>
    ///     ページ読込完了時
    /// </summary>
    /// <param name="sender">このイベントを呼び出したコントロール</param>
    /// <param name="e">この発生イベントの制御変数</param>
    private void ContentPage_Loaded(object sender, EventArgs e)
    {
        Trace.WriteLine($"[Login2Page ContentPage_Loaded] ページ読込完了");

        this.Login2PageVM.StarterKitFolder = App.GetOrLoadConfiguration().StarterKitFolder;
        this.Login2PageVM.UnityAssetsFolder = App.GetOrLoadConfiguration().UnityAssetsFolder;
    }
    #endregion

    #region イベントハンドラ（ロケール変更時）
    /// <summary>
    ///     ロケール変更時
    /// </summary>
    /// <param name="sender">このイベントを呼び出したコントロール</param>
    /// <param name="e">この発生イベントの制御変数</param>
    void LocalePicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        // ＸＡＭＬではなく、Ｃ＃で動的に翻訳を行っている場合のための変更通知
        //var context = this.TileCropPageVM;
        //context.InvalidateLocale();
    }
    #endregion

    #region イベントハンドラ（［ホーム］ボタン・クリック時）
    /// <summary>
    ///     ［ホーム］ボタン・クリック時
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    async void HomeBtn_Clicked(object sender, EventArgs e)
    {
        await PolicyOfView.ReactOnPushed((Button)sender);

        await Shell.Current.GoToAsync("//MainPage");
    }
    #endregion
}