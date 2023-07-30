namespace _2D_RPG_Negiramen.Views;

using _2D_RPG_Negiramen.Models;
using _2D_RPG_Negiramen.ViewModels;
using System.Diagnostics;

/// <summary>
///     😁 ［ログイン１］ページ
/// </summary>
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

    // - インターナル・プロパティ

    #region プロパティ（ビューモデル）
    /// <summary>
    ///     ビューモデル
    /// </summary>
    internal ILogin1PageViewModel Login1PageVM => (ILogin1PageViewModel)this.BindingContext;
    #endregion

    // - プライベート・メソッド

    /// <summary>
    ///     構成ファイルの保存
    /// </summary>
    void SaveConfigurationToml()
    {
        ConfigurationEntry newEntry = new ConfigurationEntry(
            yourCircleFolderName: this.Login1PageVM.YourCircleFolderName,
            yourWorkFolderName: this.Login1PageVM.YourWorkFolderName);

        if (App.GetOrLoadConfiguration().EntryList.Contains(newEntry))
        {
            Trace.WriteLine($"[Login1Page SaveConfigurationToml] 構成ファイルの保存　エントリーは既存");
        }
        else
        {
            Trace.WriteLine($"[Login1Page SaveConfigurationToml] 構成ファイルの保存　エントリーは新規");
        }

        // 構成ファイルの更新差分
        var configurationDifference = new Models.FileEntries.ConfigurationBuffer()
        {
            RememberYourCircleFolderName = this.Login1PageVM.YourCircleFolderName,
            RememberYourWorkFolderName = this.Login1PageVM.YourWorkFolderName,
        };

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

    // - プライベート・イベントハンドラ

    #region イベントハンドラ（ページ読込完了時）
    /// <summary>
    ///     ページ読込完了時
    /// </summary>
    /// <param name="sender">このイベントを呼び出したコントロール</param>
    /// <param name="e">この発生イベントの制御変数</param>
    private void ContentPage_Loaded(object sender, EventArgs e)
    {
        // Trace.WriteLine($"[Login1Page ContentPage_Loaded] ページ読込完了");

        this.Login1PageVM.YourCircleFolderName = App.GetOrLoadConfiguration().RememberYourCircleFolderName;
        this.Login1PageVM.YourWorkFolderName = App.GetOrLoadConfiguration().RememberYourWorkFolderName;

        Trace.WriteLine($"[Login1Page ContentPage_Loaded] ページ読込完了 Circle: {this.Login1PageVM.YourCircleFolderName.AsStr}, Work: {this.Login1PageVM.YourWorkFolderName.AsStr}, EntryList.Count: {App.GetOrLoadConfiguration().EntryList.Count}");

        foreach (var entry in App.GetOrLoadConfiguration().EntryList)
        {
            Trace.WriteLine($"[Login1Page ContentPage_Loaded] Circle: {entry.YourCircleFolderName}, Work: {entry.YourWorkFolderName}");
        }
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

    #region イベントハンドラ（［サークル・フォルダ名］変更時）
    /// <summary>
    ///     ［サークル・フォルダ名］変更時
    /// </summary>
    /// <param name="sender">このイベントを呼び出したコントロール</param>
    /// <param name="e">この発生イベントの制御変数</param>
    void YourCircleFolderNameEntry_TextChanged(object sender, TextChangedEventArgs e)
    {
        Entry entry = (Entry)sender;

        this.Login1PageVM.YourCircleFolderNameLength = entry.Text.Length;
    }
    #endregion

    #region イベントハンドラ（［作品フォルダ名］変更時）
    /// <summary>
    ///     ［作品フォルダ名］変更時
    /// </summary>
    /// <param name="sender">このイベントを呼び出したコントロール</param>
    /// <param name="e">この発生イベントの制御変数</param>
    void YourWorkFolderNameEntry_TextChanged(object sender, TextChangedEventArgs e)
    {
        Entry entry = (Entry)sender;

        this.Login1PageVM.YourWorkFolderNameLength = entry.Text.Length;
    }
    #endregion

    #region イベントハンドラ（［エントリー・リスト］選択変更時）
    /// <summary>
    ///     ［エントリー・リスト］選択変更時
    /// </summary>
    /// <param name="sender">このイベントを呼び出したコントロール</param>
    /// <param name="e">この発生イベントの制御変数</param>
    private void EntryListPicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        Picker picker = (Picker)sender;

        ConfigurationEntry? entry = (ConfigurationEntry)picker.SelectedItem;

        // 永久ループ防止
        if (entry != null)
        {
            this.Login1PageVM.YourCircleFolderName = entry.YourCircleFolderName;
            this.Login1PageVM.YourWorkFolderName = entry.YourWorkFolderName;

            // 永久ループしないよう工夫すること
            this.Login1PageVM.SelectedEntry = null;
        }
    }
    #endregion

    #region イベントハンドラ（［続きから］ボタン・クリック時）
    /// <summary>
    ///     ［続きから］ボタン・クリック時
    /// </summary>
    /// <param name="sender">このイベントを呼び出したコントロール</param>
    /// <param name="e">この発生イベントの制御変数</param>
    async void ContinueButton_Clicked(object sender, EventArgs e)
    {
        // 構成ファイルの保存
        this.SaveConfigurationToml();

        await Shell.Current.GoToAsync(
            state: new ShellNavigationState("//MainPage"));
        // ここは通り抜ける。恐らく、UIスレッドを抜けた後に画面遷移する
    }
    #endregion

    #region イベントハンドラ（［次へ］ボタン・クリック時）
    /// <summary>
    ///     ［次へ］ボタン・クリック時
    /// </summary>
    /// <param name="sender">このイベントを呼び出したコントロール</param>
    /// <param name="e">この発生イベントの制御変数</param>
    async void NextButton_Clicked(object sender, EventArgs e)
    {
        // 構成ファイルの保存
        this.SaveConfigurationToml();

        await Shell.Current.GoToAsync(
            state: new ShellNavigationState("//Login2Page"));
        // ここは通り抜ける。恐らく、UIスレッドを抜けた後に画面遷移する
    }
    #endregion
}
