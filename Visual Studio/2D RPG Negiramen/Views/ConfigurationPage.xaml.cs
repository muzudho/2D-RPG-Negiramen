namespace _2D_RPG_Negiramen.Views;

using _2D_RPG_Negiramen.ViewModels;
using System.ComponentModel;
using System.Diagnostics;

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

    //#region イベントハンドラ（［ネギラーメンのワークスペース・ディレクトリー］の［開く］ボタン・クリック時）
    ///// <summary>
    /////     ［ネギラーメンのワークスペース・ディレクトリー］の［開く］ボタン・クリック時
    ///// </summary>
    ///// <param name="sender">このイベントを呼び出したコントロール</param>
    ///// <param name="e">この発生イベントの制御変数</param>
    //private void NegiramenWorkspaceDirectoryButton_Clicked(object sender, EventArgs e)
    //{
    //    var context = this.ConfigurationPageVM;

    //    // ディレクトリーを開く場合、末尾はセパレーターにする必要がある
    //    string path = context.NegiramenWorkspaceFolderPathAsStr;
    //    if (!path.EndsWith(System.IO.Path.DirectorySeparatorChar))
    //    {
    //        path += System.IO.Path.DirectorySeparatorChar;
    //    }

    //    try
    //    {
    //        // 隠しフォルダにはアクセスできない
    //        Trace.WriteLine($"[ConfigurationPage.xaml.cs NegiramenWorkspaceDirectoryButton_Clicked] path: [{path}]");
    //        Process.Start(path,);
    //    }
    //    catch (Win32Exception win32Exception)
    //    {
    //        // TODO エラー処理どうするかまだ決めてない（＾～＾）
    //        // The system cannot find the file specified...
    //        Trace.WriteLine($"[ConfigurationPage.xaml.cs NegiramenWorkspaceDirectoryButton_Clicked] e: {win32Exception.Message}");
    //    }
    //}
    //#endregion
}