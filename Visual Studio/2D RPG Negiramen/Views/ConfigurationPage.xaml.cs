using _2D_RPG_Negiramen.ViewModels;

namespace _2D_RPG_Negiramen.Views;

/// <summary>
///		😁 ［構成］ページ・ビュー
/// </summary>
public partial class ConfigurationPage : ContentPage
{
    // - その他

    #region その他（生成）
    /// <summary>
    ///     生成
    /// </summary>
    public ConfigurationPage()
	{
		InitializeComponent();
	}
    #endregion

    // - パブリック・プロパティ

    #region プロパティ（ビューモデル）
    /// <summary>
    ///     ビューモデル
    /// </summary>
    public IConfigurationPageViewModel ConfigurationPageVM => (IConfigurationPageViewModel)this.BindingContext;
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
        var context = this.ConfigurationPageVM;
        context.InvalidateLocale();
    }
    #endregion
}