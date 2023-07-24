namespace _2D_RPG_Negiramen.Views;

using _2D_RPG_Negiramen.Models;
using _2D_RPG_Negiramen.ViewModels;
using CommunityToolkit.Maui.Views;
using System.Diagnostics;

/// <summary>
///     😁 メイン・ページ
/// </summary>
public partial class MainPage : ContentPage
{
    // - その他

    #region その他（生成）
    /// <summary>
    ///     生成
    /// </summary>
    public MainPage()
    {
        InitializeComponent();
    }
    #endregion

    // - パブリック・プロパティ

    #region プロパティ（ローカライゼーション資源管理インスタンス）
    /// <summary>
    ///     ローカライゼーション資源管理インスタンス
    /// </summary>
    public LocalizationResourceManager LocalizationResourceManager
        => LocalizationResourceManager.Instance;
    #endregion

    #region プロパティ（ビューモデル）
    /// <summary>
    ///     ビューモデル
    /// </summary>
    public IMainPageViewModel MainPageVM => (IMainPageViewModel)this.BindingContext;
    #endregion

    // - プライベート・メソッド

    #region メソッド（環境が構成ファイル通りか判定する）
    /// <summary>
    ///     環境が構成ファイル通りか判定する
    ///     
    ///     <list type="bullet">
    ///         <item>構成ファイルの設定は、ユーザーは苦手とするだろうから、必要となるまで設定を要求しないようにする仕掛け</item>
    ///         <item>📖 [同期メソッドを非同期メソッドに変換する（ex. Action → Func＜Task＞）](https://qiita.com/mxProject/items/81ba8dd331484717ee01)</item>
    ///     </list>
    /// </summary>
    /// <paramref name="onNotYetConfiguration">構成ファイル通りだ</paramref>
    /// <paramref name="onNotYetConfiguration">構成ファイル通りではない</paramref>
    async Task ReadyGoToNext(
        Func<Task> onOk,
        Func<Task> onNotYetConfiguration)
    {
        // 構成を取得
        var configuration = App.GetOrLoadConfiguration();

        // 構成通り準備できているなら、そのまま画面遷移する
        if (configuration.IsReady())
        {
            await onOk();
        }
        // そうでなければ、初期構成を要求
        else
        {
            await onNotYetConfiguration();
            // ここは通り抜ける。恐らく、UIスレッドを抜けた後に画面遷移する
        }
    }
    #endregion

    #region メソッド（構成ページへ移動）
    /// <summary>
    ///     <pre>
    ///         構成ページへ移動
    ///         
    ///         本来の移動先をグローバル変数へ記憶して、構成ページへ移動。
    ///         構成が終わったら、一旦構成ページから戻ったあと、本来の移動先へ遷移
    ///     </pre>
    /// </summary>
    /// <param name="shellNavigationState">本来の移動先</param>
    async Task GoToConfigurationPage(ShellNavigationState shellNavigationState)
    {
        App.NextPage.Push(shellNavigationState);
        await Navigation.PushAsync(new ConfigurationPage());
        // ここは通り抜ける。恐らく、UIスレッドを抜けた後に画面遷移する
    }
    #endregion

    // - プライベート・イベントハンドラ

    #region イベントハンドラ（別ページから、このページに訪れたときに呼び出される）
    /// <summary>
    ///     別ページから、このページに訪れたときに呼び出される
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        var context = this.MainPageVM;

        context.ReactOnVisited();
    }
    #endregion

    #region イベントハンドラ（［マップを作る］ボタン押下時）
    /// <summary>
    /// ［マップを作る］ボタン押下時
    /// </summary>
    /// <param name="sender">このイベントを呼び出したコントロール</param>
    /// <param name="e">この発生イベントの制御変数</param>
    async void CreateMapViewBtn_Clicked(object sender, EventArgs e)
    {
        var shellNavigationState = new ShellNavigationState("//MapExplorerPage");

        // 次のページへ遷移する。ただし、構成ファイルが設定されていないなら、その設定を要求する
        await ReadyGoToNext(
            onOk: async () =>
            {
                await Shell.Current.GoToAsync(shellNavigationState);
                // ここは通り抜ける。恐らく、UIスレッドを抜けた後に画面遷移する
            },
            onNotYetConfiguration: async () =>
            {
                await GoToConfigurationPage(shellNavigationState);
                // ここは通り抜ける。恐らく、UIスレッドを抜けた後に画面遷移する
            });
    }
    #endregion

    #region イベントハンドラ（［戦闘を作る］ボタン押下時）
    /// <summary>
    /// ［戦闘を作る］ボタン押下時
    /// </summary>
    /// <param name="sender">このイベントを呼び出したコントロール</param>
    /// <param name="e">この発生イベントの制御変数</param>
    async void CreateBattleBtn_Clicked(object sender, EventArgs e)
    {
        var shellNavigationState = new ShellNavigationState("//CreateBattleViewPage");

        // 次のページへ遷移する。ただし、構成ファイルが設定されていないなら、その設定を要求する
        await ReadyGoToNext(
            onOk: async () =>
            {
                await Shell.Current.GoToAsync(shellNavigationState);
                // ここは通り抜ける。恐らく、UIスレッドを抜けた後に画面遷移する
            },
            onNotYetConfiguration: async () =>
            {
                await GoToConfigurationPage(shellNavigationState);
                // ここは通り抜ける。恐らく、UIスレッドを抜けた後に画面遷移する
            });
    }
    #endregion

    #region イベントハンドラ（［メニューを作る］ボタン押下時）
    /// <summary>
    /// ［メニューを作る］ボタン押下時
    /// </summary>
    /// <param name="sender">このイベントを呼び出したコントロール</param>
    /// <param name="e">この発生イベントの制御変数</param>
    async void CreateMenuViewBtn_Clicked(object sender, EventArgs e)
    {
        var shellNavigationState = new ShellNavigationState("//CreateMenuViewPage");

        // 次のページへ遷移する。ただし、構成ファイルが設定されていないなら、その設定を要求する
        await ReadyGoToNext(
            onOk: async () =>
            {
                await Shell.Current.GoToAsync(shellNavigationState);
                // ここは通り抜ける。恐らく、UIスレッドを抜けた後に画面遷移する
            },
            onNotYetConfiguration: async () =>
            {
                await GoToConfigurationPage(shellNavigationState);
                // ここは通り抜ける。恐らく、UIスレッドを抜けた後に画面遷移する
            });
    }
    #endregion

    #region イベントハンドラ（［会話画面を作る］ボタン押下時）
    /// <summary>
    /// ［会話画面を作る］ボタン押下時
    /// </summary>
    /// <param name="sender">このイベントを呼び出したコントロール</param>
    /// <param name="e">この発生イベントの制御変数</param>
    async void CreateTalkingBtn_Clicked(object sender, EventArgs e)
    {
        var shellNavigationState = new ShellNavigationState("//CreateTalkingViewPage");

        // 次のページへ遷移する。ただし、構成ファイルが設定されていないなら、その設定を要求する
        await ReadyGoToNext(
            onOk: async () =>
            {
                await Shell.Current.GoToAsync(shellNavigationState);
                // ここは通り抜ける。恐らく、UIスレッドを抜けた後に画面遷移する
            },
            onNotYetConfiguration: async () =>
            {
                await GoToConfigurationPage(shellNavigationState);
                // ここは通り抜ける。恐らく、UIスレッドを抜けた後に画面遷移する
            });
    }
    #endregion

    #region イベントハンドラ（［プレイヤー・キャラクターを編集］ボタン押下時）
    /// <summary>
    /// ［プレイヤー・キャラクターを編集］ボタン押下時
    /// </summary>
    /// <param name="sender">このイベントを呼び出したコントロール</param>
    /// <param name="e">この発生イベントの制御変数</param>
    async void EditPlayerCharacterBtn_Clicked(object sender, EventArgs e)
    {
        var shellNavigationState = new ShellNavigationState("//EditPlayerCharacterPage");

        // 次のページへ遷移する。ただし、構成ファイルが設定されていないなら、その設定を要求する
        await ReadyGoToNext(
            onOk: async () =>
            {
                await Shell.Current.GoToAsync(shellNavigationState);
                // ここは通り抜ける。恐らく、UIスレッドを抜けた後に画面遷移する
            },
            onNotYetConfiguration: async () =>
            {
                await GoToConfigurationPage(shellNavigationState);
                // ここは通り抜ける。恐らく、UIスレッドを抜けた後に画面遷移する
            });
    }
    #endregion

    #region イベントハンドラ（［モンスターを編集］ボタン押下時）
    /// <summary>
    /// ［モンスターを編集］ボタン押下時
    /// </summary>
    /// <param name="sender">このイベントを呼び出したコントロール</param>
    /// <param name="e">この発生イベントの制御変数</param>
    async void CreateMonsterBtn_Clicked(object sender, EventArgs e)
    {
        var shellNavigationState = new ShellNavigationState("//EditMonsterPage");

        // 次のページへ遷移する。ただし、構成ファイルが設定されていないなら、その設定を要求する
        await ReadyGoToNext(
            onOk: async () =>
            {
                await Shell.Current.GoToAsync(shellNavigationState);
                // ここは通り抜ける。恐らく、UIスレッドを抜けた後に画面遷移する
            },
            onNotYetConfiguration: async () =>
            {
                await GoToConfigurationPage(shellNavigationState);
                // ここは通り抜ける。恐らく、UIスレッドを抜けた後に画面遷移する
            });
    }
    #endregion

    #region イベントハンドラ（［モンスター・グループを編集］ボタン押下時）
    /// <summary>
    /// ［モンスター・グループを編集］ボタン押下時
    /// </summary>
    /// <param name="sender">このイベントを呼び出したコントロール</param>
    /// <param name="e">この発生イベントの制御変数</param>
    async void EditMonsterGroupBtn_Clicked(object sender, EventArgs e)
    {
        var shellNavigationState = new ShellNavigationState("//EditMonsterGroupPage");

        // 次のページへ遷移する。ただし、構成ファイルが設定されていないなら、その設定を要求する
        await ReadyGoToNext(
            onOk: async () =>
            {
                await Shell.Current.GoToAsync(shellNavigationState);
                // ここは通り抜ける。恐らく、UIスレッドを抜けた後に画面遷移する
            },
            onNotYetConfiguration: async () =>
            {
                await GoToConfigurationPage(shellNavigationState);
                // ここは通り抜ける。恐らく、UIスレッドを抜けた後に画面遷移する
            });
    }
    #endregion

    #region イベントハンドラ（［アイテムを編集］ボタン押下時）
    /// <summary>
    /// ［アイテムを編集］ボタン押下時
    /// </summary>
    /// <param name="sender">このイベントを呼び出したコントロール</param>
    /// <param name="e">この発生イベントの制御変数</param>
    async void EditItemBtn_Clicked(object sender, EventArgs e)
    {
        var shellNavigationState = new ShellNavigationState("//EditItemPage");

        // 次のページへ遷移する。ただし、構成ファイルが設定されていないなら、その設定を要求する
        await ReadyGoToNext(
            onOk: async () =>
            {
                await Shell.Current.GoToAsync(shellNavigationState);
                // ここは通り抜ける。恐らく、UIスレッドを抜けた後に画面遷移する
            },
            onNotYetConfiguration: async () =>
            {
                await GoToConfigurationPage(shellNavigationState);
                // ここは通り抜ける。恐らく、UIスレッドを抜けた後に画面遷移する
            });
    }
    #endregion

    #region イベントハンドラ（［話しを編集］ボタン押下時）
    /// <summary>
    /// ［話しを編集］ボタン押下時
    /// </summary>
    /// <param name="sender">このイベントを呼び出したコントロール</param>
    /// <param name="e">この発生イベントの制御変数</param>
    async void EditorStoryBtn_Clicked(object sender, EventArgs e)
    {
        var shellNavigationState = new ShellNavigationState("//EditStoryPage");

        // 次のページへ遷移する。ただし、構成ファイルが設定されていないなら、その設定を要求する
        await ReadyGoToNext(
            onOk: async () =>
            {
                await Shell.Current.GoToAsync(shellNavigationState);
                // ここは通り抜ける。恐らく、UIスレッドを抜けた後に画面遷移する
            },
            onNotYetConfiguration: async () =>
            {
                await GoToConfigurationPage(shellNavigationState);
                // ここは通り抜ける。恐らく、UIスレッドを抜けた後に画面遷移する
            });
    }
    #endregion

    #region イベントハンドラ（［タイルセット一覧］ボタン押下時）
    /// <summary>
    /// ［タイルセット一覧］ボタン押下時
    /// </summary>
    /// <param name="sender">このイベントを呼び出したコントロール</param>
    /// <param name="e">この発生イベントの制御変数</param>
    async void EditsTilesetButton_Clicked(object sender, EventArgs e)
    {
        var shellNavigationState = new ShellNavigationState("//TilesetListPage");

        // 次のページへ遷移する。ただし、構成ファイルが設定されていないなら、その設定を要求する
        await ReadyGoToNext(
            onOk: async () =>
            {
                await Shell.Current.GoToAsync(shellNavigationState);
                // ここは通り抜ける。恐らく、UIスレッドを抜けた後に画面遷移する
            },
            onNotYetConfiguration: async () =>
            {
                await GoToConfigurationPage(shellNavigationState);
                // ここは通り抜ける。恐らく、UIスレッドを抜けた後に画面遷移する
            });
    }
    #endregion

    #region イベントハンドラ（［初期設定］ボタン押下時）
    /// <summary>
    /// ［初期設定］ボタン押下時
    /// </summary>
    /// <param name="sender">このイベントを呼び出したコントロール</param>
    /// <param name="e">この発生イベントの制御変数</param>
    async void StartupConfigurationBtn_Clicked(object sender, EventArgs e)
    {
        // 必ず、初期設定を要求
        // 戻り先はホーム
        App.NextPage.Push(new ShellNavigationState("//MainPage"));
        await Navigation.PushAsync(new ConfigurationPage());
        // ここは通り抜ける。恐らく、UIスレッドを抜けた後に画面遷移する
    }
    #endregion

    #region イベントハンドラ（［ウィンドウ表示テスト］ボタン押下時）
    /// <summary>
    /// ［ウィンドウ表示テスト］ボタン押下時
    /// </summary>
    /// <param name="sender">このイベントを呼び出したコントロール</param>
    /// <param name="e">この発生イベントの制御変数</param>
    async void TestShowWindowButton_Clicked(object sender, EventArgs e)
    {
        /*
        var secondWindow = new Window
        {
            Page = new ConfigurationPage
            {
                // ...
            },
            Width = 1200,
            Height = 400,
        };

        Application.Current.OpenWindow(secondWindow);
        */

        await Navigation.PushAsync(new ConfigurationPage());
    }
    #endregion

    #region イベントハンドラ（［ポップアップ練習］ボタン押下時）
    /// <summary>
    /// ［ポップアップ練習］ボタン押下時
    /// </summary>
    /// <param name="sender">このイベントを呼び出したコントロール</param>
    /// <param name="e">この発生イベントの制御変数</param>
    void PopupPracticeButton_Clicked(object sender, EventArgs e)
    {
        var popup = new PopupPractice();

        this.ShowPopup(popup);
    }
    #endregion

    #region イベントハンドラ（［タイル・パレット表示］ボタン押下時）
    /// <summary>
    /// ［タイル・パレット表示］ボタン押下時
    /// </summary>
    /// <param name="sender">このイベントを呼び出したコントロール</param>
    /// <param name="e">この発生イベントの制御変数</param>
    void ShowTilePalette_Clicked(object sender, EventArgs e)
    {
        var secondWindow = new Window
        {
            Page = new TilePalettePage
            {
                // ...
            },
            Width = 600,
            Height = 300,
        };

        Application.Current.OpenWindow(secondWindow);
    }
    #endregion

    #region イベントハンドラ（［マップ描画ページ表示］ボタン押下時）
    /// <summary>
    /// ［マップ描画ページ表示］ボタン押下時
    /// </summary>
    /// <param name="sender">このイベントを呼び出したコントロール</param>
    /// <param name="e">この発生イベントの制御変数</param>
    void MapDrawingButton_Clicked(object sender, EventArgs e)
    {
        var secondWindow = new Window
        {
            Page = new MapDrawingPage
            {
                // ...
            },
            Width = 600,
            Height = 300,
        };

        Application.Current.OpenWindow(secondWindow);
    }
    #endregion

    #region イベントハンドラ（［タイル切抜き］ボタン押下時）
    /// <summary>
    /// ［タイル切抜き］ボタン押下時
    /// </summary>
    /// <param name="sender">このイベントを呼び出したコントロール</param>
    /// <param name="e">この発生イベントの制御変数</param>
    async void TileCropButton_Clicked(object sender, EventArgs e)
    {
        // Trace.WriteLine($"[MainPage.xaml.cs TileCropButton_Clicked] sender.GetType().FullName: {sender.GetType().FullName}");
        // [MainPage.xaml.cs TileCropButton_Clicked] sender.GetType().FullName: Microsoft.Maui.Controls.Button

        await PolicyOfView.ReactOnPushed((Button)sender);

        var shellNavigationState = new ShellNavigationState("//TileCropPage");

        // 次のページへ遷移する。ただし、構成ファイルが設定されていないなら、その設定を要求する
        await ReadyGoToNext(
            onOk: async () =>
            {
                // ユニティのアセット・フォルダへのパス
                var unityAssetsFolderPathAsStr = App.GetOrLoadConfiguration().UnityAssetsFolder.Path.AsStr;

                // ファイル名の拡張子抜き
                var fileStem = "map-tileset-format-8x19";

                // タイルセット画像ファイル・パス
                var tilesetImageFile = App.GetOrLoadConfiguration().UnityAssetsFolder.YourCircleNameFolder.YourWorkNameFolder.AutoGeneratedFolder.ImagesFolder.TilesetsFolder.CreateTilesetPng(fileStem);

                // タイルセットCSVファイル・パス
                var tilesetSettingsFile = App.GetOrLoadConfiguration().UnityAssetsFolder.YourCircleNameFolder.YourWorkNameFolder.AutoGeneratedFolder.DataFolder.CsvFolder.TilesetsFolder.CreateTilesetCsv(fileStem);

                await Shell.Current.GoToAsync(
                    state: shellNavigationState,
                    parameters: new Dictionary<string, object>
                    {
                        [key: "TilesetImageFile"] = tilesetImageFile,
                        [key: "TilesetSettingsFile"] = tilesetSettingsFile,
                    });
                // ここは通り抜ける。恐らく、UIスレッドを抜けた後に画面遷移する
            },
            onNotYetConfiguration: async () =>
            {
                await GoToConfigurationPage(shellNavigationState);
                // ここは通り抜ける。恐らく、UIスレッドを抜けた後に画面遷移する
            });
    }
    #endregion

    #region イベントハンドラ（ボタンにマウスカーソル進入時）
    /// <summary>
    ///     ボタンにマウスカーソル進入時
    /// </summary>
    /// <param name="sender">このイベントを呼び出したコントロール</param>
    /// <param name="e">この発生イベントの制御変数</param>
    private void Button_PointerGestureRecognizer_PointerEntered(object sender, PointerEventArgs e)
    {
        PolicyOfView.ReactOnMouseEntered((Button)sender);
    }
    #endregion

    #region イベントハンドラ（ボタンからマウスカーソル退出時）
    /// <summary>
    ///     ボタンからマウスカーソル退出時
    /// </summary>
    /// <param name="sender">このイベントを呼び出したコントロール</param>
    /// <param name="e">この発生イベントの制御変数</param>
    private void Button_PointerGestureRecognizer_PointerExited(object sender, PointerEventArgs e)
    {
        PolicyOfView.ReactOnMouseLeaved((Button)sender);
    }
    #endregion
}
