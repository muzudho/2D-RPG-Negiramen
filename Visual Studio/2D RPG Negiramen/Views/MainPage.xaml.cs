namespace _2D_RPG_Negiramen.Views;
using CommunityToolkit.Maui.Views;

public partial class MainPage : ContentPage
{
    /*
	int count = 0;
    */

    public MainPage()
    {
        InitializeComponent();
    }

    /*
    private void OnCounterClicked(object sender, EventArgs e)
    {
        count++;

        if (count == 1)
            CounterBtn.Text = $"Clicked {count} time";
        else
            CounterBtn.Text = $"Clicked {count} times";

        SemanticScreenReader.Announce(CounterBtn.Text);
    }
    */

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

    /// <summary>
    ///     <pre>
    ///         本来の移動先をグローバル変数へ記憶して、構成ページへ移動。
    ///         構成が終わったら、一旦構成ページから戻ったあと、本来の移動先へ遷移
    ///     </pre>
    /// </summary>
    /// <param name="shellNavigationState">本来の移動先</param>
    async Task GoToConfigurationPage(ShellNavigationState shellNavigationState)
    {
        App.NextPage.Push(shellNavigationState);
        await Navigation.PushAsync(new StartupConfigurationPage());
        // ここは通り抜ける。恐らく、UIスレッドを抜けた後に画面遷移する
    }

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
        await Navigation.PushAsync(new StartupConfigurationPage());
        // ここは通り抜ける。恐らく、UIスレッドを抜けた後に画面遷移する
    }

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
            Page = new StartupConfigurationPage
            {
                // ...
            },
            Width = 1200,
            Height = 400,
        };

        Application.Current.OpenWindow(secondWindow);
        */

        await Navigation.PushAsync(new StartupConfigurationPage());
    }

    /// <summary>
    /// ［ポップアップ練習］ボタン押下時
    /// </summary>
    /// <param name="sender">このイベントを呼び出したコントロール</param>
    /// <param name="e">この発生イベントの制御変数</param>
    private void PopupPracticeButton_Clicked(object sender, EventArgs e)
    {
        var popup = new PopupPractice();

        this.ShowPopup(popup);
    }

    /// <summary>
    /// ［タイル・パレット表示］ボタン押下時
    /// </summary>
    /// <param name="sender">このイベントを呼び出したコントロール</param>
    /// <param name="e">この発生イベントの制御変数</param>
    private void ShowTilePalette_Clicked(object sender, EventArgs e)
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

    /// <summary>
    /// ［マップ描画ページ表示］ボタン押下時
    /// </summary>
    /// <param name="sender">このイベントを呼び出したコントロール</param>
    /// <param name="e">この発生イベントの制御変数</param>
    private void MapDrawingButton_Clicked(object sender, EventArgs e)
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

    /// <summary>
    /// ［タイル・パレット編集］ボタン押下時
    /// </summary>
    /// <param name="sender">このイベントを呼び出したコントロール</param>
    /// <param name="e">この発生イベントの制御変数</param>
    async void TilePaletteEditButton_Clicked(object sender, EventArgs e)
    {
        var shellNavigationState = new ShellNavigationState("//TilePaletteEditPage");

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
}

