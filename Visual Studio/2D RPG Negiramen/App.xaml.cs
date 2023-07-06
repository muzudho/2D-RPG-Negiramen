namespace _2D_RPG_Negiramen;

using _2D_RPG_Negiramen.Models;

/// <summary>
///     😁 アプリケーション
/// </summary>
public partial class App : Application
{
    // - 静的プロパティー

    /// <summary>
    ///     画面遷移先の一時記憶
    /// </summary>
    static internal Stack<ShellNavigationState> NextPage { get; set; } = new Stack<ShellNavigationState>();

    /// <summary>
    ///     グリッド線の半分の太さ
    /// </summary>
    static internal ThicknessOfLine HalfThicknessOfGridLine { get; } = new Models.ThicknessOfLine(1);

    /// <summary>
    ///     タイル・カーソルの線の半分の太さ
    /// </summary>
    static internal ThicknessOfLine HalfThicknessOfTileCursorLine => new Models.ThicknessOfLine(2 * HalfThicknessOfGridLine.AsInt);

    /// <summary>
    ///     現在作業中の画面の中でのグリッド・タイル・サイズ
    /// </summary>
    static internal Models.Size WorkingGridTileSize { get; set; } = new Models.Size(new Models.Width(32), new Models.Height(32));

    /// <summary>
    ///     現在作業中の画面の中でのタイル・カーソル・サイズ
    /// </summary>
    static internal Models.Size WorkingTileCursorSize { get; set; } = new Models.Size(new Models.Width(0), new Models.Height(0));

    /// <summary>
    ///     グリッド全体の左上表示位置
    /// </summary>
    static internal Models.Point WorkingGridLeftTop = Models.Point.Empty;

    /// <summary>
    ///     ポインティング・デバイス押下中か？
    /// 
    ///     <list type="bullet">
    ///         <item>タイルを選択開始していて、まだ未確定だ</item>
    ///     </list>
    /// </summary>
    static internal bool SelectingOnPointingDevice { get; set; }

    // - 静的メソッド

    #region 静的メソッド（構成）
    /// <summary>
    /// 構成ファイルの取得、またはファイル読込
    /// </summary>
    /// <returns>構成ファイル</returns>
    static internal Models.FileEntries.Configuration GetOrLoadConfiguration()
    {
        if (App.Configuration == null)
        {
            // 構成ファイルの読込
            if (Models.FileEntries.Configuration.LoadTOML(out Models.FileEntries.Configuration configuration))
            {
                App.Configuration = configuration;
            }

            // TODO 構成ファイルが無かったら、エラー対応したい
        }

        return App.Configuration;
    }

    /// <summary>
    /// 構成ファイルをセット
    /// </summary>
    /// <param name="configuration">構成ファイル</param>
    static internal void SetConfiguration(Models.FileEntries.Configuration configuration)
    {
        App.Configuration = configuration;
    }
    #endregion

    #region 静的メソッド（ユーザー構成）
    /// <summary>
    /// ユーザー構成ファイルの取得、またはファイル読込
    /// </summary>
    /// <returns>ユーザー構成ファイル</returns>
    static internal Models.FileEntries.UserConfiguration GetOrLoadUserConfiguration()
    {
        if (App.UserConfiguration == null)
        {
            // 構成ファイルの読込
            if (Models.FileEntries.UserConfiguration.LoadTOML(out Models.FileEntries.UserConfiguration userConfiguration))
            {
                App.UserConfiguration = userConfiguration;
            }

            // TODO 構成ファイルが無かったら、エラー対応したい
        }

        return App.UserConfiguration;
    }

    /// <summary>
    /// ユーザー構成ファイルをセット
    /// </summary>
    /// <param name="userConfiguration">構成ファイル</param>
    static internal void SetUserConfiguration(Models.FileEntries.UserConfiguration userConfiguration)
    {
        App.UserConfiguration = userConfiguration;
    }
    #endregion

    #region 静的メソッド（設定）
    /// <summary>
    /// 設定ファイルの取得、またはファイル読込
    /// </summary>
    /// <returns>設定ファイル</returns>
    static internal Models.FileEntries.Settings GetOrLoadSettings()
    {
        if (App.Settings == null)
        {
            // 設定ファイルの読込（読取成功にせよ、失敗にせよ）
            if (Models.FileEntries.Settings.LoadTOML(out Models.FileEntries.Settings settings))
            {
                App.Settings = settings;
            }
            else
            {
                // 読込に失敗したなら、既定値の設定ファイルを取得（ここでは、保存はしない）
                App.Settings = settings;
            }
        }

        return App.Settings;
    }

    /// <summary>
    /// 設定ファイルをセット
    /// </summary>
    /// <param name="settings">設定ファイル</param>
    static internal void SetSettings(Models.FileEntries.Settings settings)
    {
        App.Settings = settings;
    }
    #endregion

    /// <summary>
    /// 生成
    /// </summary>
    public App()
    {
        InitializeComponent();

        MainPage = new AppShell();
    }

    // - プライベート静的プロパティー

    /// <summary>
    ///		現在の構成
    /// 
    ///		<list type="bullet">
    ///			<item>ミュータブル</item>
    ///		</list>
    /// </summary>
    static Models.FileEntries.Configuration Configuration { get; set; }

    /// <summary>
    ///		現在のユーザー構成
    /// 
    ///		<list type="bullet">
    ///			<item>ミュータブル</item>
    ///		</list>
    /// </summary>
    static Models.FileEntries.UserConfiguration UserConfiguration { get; set; }

    /// <summary>
    ///		現在の設定
    /// 
    ///		<list type="bullet">
    ///			<item>ミュータブル</item>
    ///		</list>
    /// </summary>
    static Models.FileEntries.Settings Settings { get; set; }
}
