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
        //
        // 構成の保存
        // ==========
        //
        var newProjectId = new ProjectId(
            yourCircleFolderName: this.Login2PageVM.YourCircleFolderName,
            yourWorkFolderName: this.Login2PageVM.YourWorkFolderName);

        // 構成ファイルの更新差分
        var configurationDifference = new Models.FileEntries.ConfigurationBuffer()
        {
        };

        if (App.GetOrLoadConfiguration().ProjectIdList.Contains(newProjectId))
        {
            Trace.WriteLine($"[Login1Page SaveConfigurationToml] 構成ファイルの保存　エントリーは既存");
        }
        else
        {
            Trace.WriteLine($"[Login1Page SaveConfigurationToml] 構成ファイルの保存　エントリーは新規");
            // FIXME こうしなくても直接追加できてしまうような
            configurationDifference.ProjectIdList = App.GetOrLoadConfiguration().ProjectIdList.ToList();
            configurationDifference.ProjectIdList.Add(newProjectId);
        }

        if (Models.FileEntries.Configuration.SaveTOML(App.GetOrLoadConfiguration(), configurationDifference, out Models.FileEntries.Configuration newConfiguration))
        {
            // グローバル変数を更新
            App.SetConfiguration(newConfiguration);
        }
        else
        {
            // TODO 異常時の処理
        }

        //
        // プロジェクト構成の保存
        // ======================
        //

        // プロジェクト構成ファイルの更新差分
        var projectConfigurationDifference = new Models.FileEntries.ProjectConfigurationBuffer()
        {
            StarterKitFolder = this.Login2PageVM.StarterKitFolder,
            UnityAssetsFolder = this.Login2PageVM.UnityAssetsFolder,
        };

        if (Models.FileEntries.ProjectConfiguration.SaveTOML(App.GetOrLoadProjectConfiguration(), projectConfigurationDifference, out Models.FileEntries.ProjectConfiguration newProjectConfiguration))
        {
            // グローバル変数を更新
            App.SetProjectConfiguration(newProjectConfiguration);
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

        this.Login2PageVM.StarterKitFolder = this.Login2PageVM.StarterKitFolder;
        this.Login2PageVM.UnityAssetsFolder = this.Login2PageVM.UnityAssetsFolder;
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