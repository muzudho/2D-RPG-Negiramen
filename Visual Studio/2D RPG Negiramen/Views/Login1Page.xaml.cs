namespace _2D_RPG_Negiramen.Views;

public partial class Login1Page : ContentPage
{
    // - その他

    #region その他（生成）
    /// <summary>
    ///     生成
    /// </summary>
    public Login1Page()
    {
        InitializeComponent();
    }
    #endregion

    // - イベントハンドラ

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

    #region イベントハンドラ（ロケール変更時）
    /// <summary>
    ///     ロケール変更時
    /// </summary>
    /// <param name="sender">このイベントを呼び出したコントロール</param>
    /// <param name="e">この発生イベントの制御変数</param>
    private void LocalePicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        // ＸＡＭＬではなく、Ｃ＃で動的に翻訳を行っている場合のための変更通知
        //var context = this.TileCropPageVM;
        //context.InvalidateLocale();
    }
    #endregion
}