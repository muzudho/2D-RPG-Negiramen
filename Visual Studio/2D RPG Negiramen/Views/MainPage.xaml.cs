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
    /// 次のページへ遷移する。ただし、構成ファイルが設定されていないなら、その設定を要求する
    /// 
    /// <list type="bullet">
    ///     <item>構成ファイルの設定は、ユーザーは苦手とするだろうから、必要となるまで設定を要求しないようにする仕掛け</item>
    /// </list>
    /// </summary>
    /// <param name="shellNavigationState">遷移先ページ</param>
    async Task GoToNextPageRequiredConfiguration(ShellNavigationState shellNavigationState)
    {
        // フォルダーが準備できているなら、そのまま画面遷移する
        var configuration = App.GetOrLoadConfiguration();
        if (configuration.IsReady())
        {
            await Shell.Current.GoToAsync(shellNavigationState);
            // ここは通り抜ける。恐らく、UIスレッドを抜けた後に画面遷移する
        }
        // そうでなければ、初期設定を要求
        else
        {
            App.NextPage.Push(shellNavigationState);
            await Navigation.PushAsync(new StartupConfigurationPage());
            // ここは通り抜ける。恐らく、UIスレッドを抜けた後に画面遷移する
        }
    }

    /// <summary>
    /// ［マップを作る］ボタン押下時
    /// </summary>
    /// <param name="sender">このイベントを呼び出したコントロール</param>
    /// <param name="e">この発生イベントの制御変数</param>
    async void CreateMapViewBtn_Clicked(object sender, EventArgs e)
    {
        // 次のページへ遷移する。ただし、構成ファイルが設定されていないなら、その設定を要求する
        await GoToNextPageRequiredConfiguration(new ShellNavigationState("//MapExplorerPage"));
    }

    /// <summary>
    /// ［戦闘を作る］ボタン押下時
    /// </summary>
    /// <param name="sender">このイベントを呼び出したコントロール</param>
    /// <param name="e">この発生イベントの制御変数</param>
    async void CreateBattleBtn_Clicked(object sender, EventArgs e)
    {
        // 次のページへ遷移する。ただし、構成ファイルが設定されていないなら、その設定を要求する
        await GoToNextPageRequiredConfiguration(new ShellNavigationState("//CreateBattleViewPage"));
    }

    /// <summary>
    /// ［メニューを作る］ボタン押下時
    /// </summary>
    /// <param name="sender">このイベントを呼び出したコントロール</param>
    /// <param name="e">この発生イベントの制御変数</param>
    async void CreateMenuViewBtn_Clicked(object sender, EventArgs e)
    {
        // 次のページへ遷移する。ただし、構成ファイルが設定されていないなら、その設定を要求する
        await GoToNextPageRequiredConfiguration(new ShellNavigationState("//CreateMenuViewPage"));
    }

    /// <summary>
    /// ［会話画面を作る］ボタン押下時
    /// </summary>
    /// <param name="sender">このイベントを呼び出したコントロール</param>
    /// <param name="e">この発生イベントの制御変数</param>
    async void CreateTalkingBtn_Clicked(object sender, EventArgs e)
    {
        // 次のページへ遷移する。ただし、構成ファイルが設定されていないなら、その設定を要求する
        await GoToNextPageRequiredConfiguration(new ShellNavigationState("//CreateTalkingViewPage"));
    }

    /// <summary>
    /// ［プレイヤー・キャラクターを編集］ボタン押下時
    /// </summary>
    /// <param name="sender">このイベントを呼び出したコントロール</param>
    /// <param name="e">この発生イベントの制御変数</param>
    async void EditPlayerCharacterBtn_Clicked(object sender, EventArgs e)
    {
        // 次のページへ遷移する。ただし、構成ファイルが設定されていないなら、その設定を要求する
        await GoToNextPageRequiredConfiguration(new ShellNavigationState("//EditPlayerCharacterPage"));
    }

    /// <summary>
    /// ［モンスターを編集］ボタン押下時
    /// </summary>
    /// <param name="sender">このイベントを呼び出したコントロール</param>
    /// <param name="e">この発生イベントの制御変数</param>
    async void CreateMonsterBtn_Clicked(object sender, EventArgs e)
    {
        // 次のページへ遷移する。ただし、構成ファイルが設定されていないなら、その設定を要求する
        await GoToNextPageRequiredConfiguration(new ShellNavigationState("//EditMonsterPage"));
    }

    /// <summary>
    /// ［モンスター・グループを編集］ボタン押下時
    /// </summary>
    /// <param name="sender">このイベントを呼び出したコントロール</param>
    /// <param name="e">この発生イベントの制御変数</param>
    async void EditMonsterGroupBtn_Clicked(object sender, EventArgs e)
    {
        // 次のページへ遷移する。ただし、構成ファイルが設定されていないなら、その設定を要求する
        await GoToNextPageRequiredConfiguration(new ShellNavigationState("//EditMonsterGroupPage"));
    }

    /// <summary>
    /// ［アイテムを編集］ボタン押下時
    /// </summary>
    /// <param name="sender">このイベントを呼び出したコントロール</param>
    /// <param name="e">この発生イベントの制御変数</param>
    async void EditItemBtn_Clicked(object sender, EventArgs e)
    {
        // 次のページへ遷移する。ただし、構成ファイルが設定されていないなら、その設定を要求する
        await GoToNextPageRequiredConfiguration(new ShellNavigationState("//EditItemPage"));
    }

    /// <summary>
    /// ［話しを編集］ボタン押下時
    /// </summary>
    /// <param name="sender">このイベントを呼び出したコントロール</param>
    /// <param name="e">この発生イベントの制御変数</param>
    async void EditorStoryBtn_Clicked(object sender, EventArgs e)
    {
        // 次のページへ遷移する。ただし、構成ファイルが設定されていないなら、その設定を要求する
        await GoToNextPageRequiredConfiguration(new ShellNavigationState("//EditStoryPage"));
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
        // 次のページへ遷移する。ただし、構成ファイルが設定されていないなら、その設定を要求する
        await GoToNextPageRequiredConfiguration(new ShellNavigationState("//TilePaletteEditPage"));
    }
}

