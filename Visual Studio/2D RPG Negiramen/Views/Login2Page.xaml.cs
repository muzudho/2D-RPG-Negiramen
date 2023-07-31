namespace _2D_RPG_Negiramen.Views;

using _2D_RPG_Negiramen.Models;
using _2D_RPG_Negiramen.ViewModels;
using System.Diagnostics;

/// <summary>
///     😁 ［ログイン２］ページ
/// </summary>
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

    // - プライベート・メソッド

    #region メソッド（構成ファイルの保存）
    /// <summary>
    ///     構成ファイルの保存
    /// </summary>
    void SaveConfigurationToml()
    {
        ConfigurationEntry newEntry = new ConfigurationEntry(
            yourCircleFolderName: App.GetOrLoadConfiguration().RememberYourCircleFolderName,
            yourWorkFolderName: App.GetOrLoadConfiguration().RememberYourWorkFolderName);

        // 構成ファイルの更新差分
        var configurationDifference = new Models.FileEntries.ConfigurationBuffer()
        {
        };

        if (App.GetOrLoadConfiguration().EntryList.Contains(newEntry))
        {
            Trace.WriteLine($"[Login1Page SaveConfigurationToml] 構成ファイルの保存　エントリーは既存");
        }
        else
        {
            Trace.WriteLine($"[Login1Page SaveConfigurationToml] 構成ファイルの保存　エントリーは新規");
            // FIXME こうしなくても直接追加できてしまうような
            configurationDifference.EntryList = App.GetOrLoadConfiguration().EntryList.ToList();
            configurationDifference.EntryList.Add(newEntry);
        }

        // 構成ファイルの保存
        if (Models.FileEntries.Configuration.SaveTOML(App.GetOrLoadConfiguration(), configurationDifference, out Models.FileEntries.Configuration newConfiguration))
        {
            // グローバル変数を更新
            App.SetConfiguration(newConfiguration);
        }
        else
        {
            // TODO 異常時の処理
        }
    }
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
    /// <param name="sender">このイベントを呼び出したコントロール</param>
    /// <param name="e">この発生イベントの制御変数</param>
    async void HomeBtn_Clicked(object sender, EventArgs e)
    {
        await PolicyOfView.ReactOnPushed((Button)sender);

        await Shell.Current.GoToAsync("//Login1Page");
    }
    #endregion

    #region イベントハンドラ（［新しく作る］ボタン・クリック時）
    /// <summary>
    ///     ［新しく作る］ボタン・クリック時
    /// </summary>
    /// <param name="sender">このイベントを呼び出したコントロール</param>
    /// <param name="e">この発生イベントの制御変数</param>
    async void CreateANewProjectButton_Clicked(object sender, EventArgs e)
    {
        // 構成ファイルの保存
        this.SaveConfigurationToml();

        await Shell.Current.GoToAsync(
            state: new ShellNavigationState("//MainPage"));
        // ここは通り抜ける。恐らく、UIスレッドを抜けた後に画面遷移する
    }
    #endregion
}