namespace _2D_RPG_Negiramen;

using _2D_RPG_Negiramen.Models;
using CsvHelper;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;

/// <summary>
///     😁 アプリケーション
/// </summary>
public partial class App : Application
{
    // - インターナル静的プロパティ

    #region プロパティ（ロケールＩｄのリスト）
    /// <summary>
    ///     ロケールＩｄのリスト
    /// </summary>
    internal static ObservableCollection<string> LocaleIdCollection { get; } = new ObservableCollection<string>(new List<string>()
        {
            "ja-JP",
            "en-US",
        });
    #endregion

    #region プロパティ（画面遷移先の一時記憶）
    /// <summary>
    ///     画面遷移先の一時記憶
    /// </summary>
    static internal Stack<ShellNavigationState> NextPage { get; set; } = new Stack<ShellNavigationState>();
    #endregion

    #region プロパティ（グリッド線の半分の太さ）
    /// <summary>
    ///     グリッド線の半分の太さ
    /// </summary>
    static internal ThicknessOfLine HalfThicknessOfGridLine { get; } = new Models.ThicknessOfLine(1);
    #endregion

    #region プロパティ（タイル・カーソルの線の半分の太さ）
    /// <summary>
    ///     タイル・カーソルの線の半分の太さ
    /// </summary>
    static internal ThicknessOfLine HalfThicknessOfTileCursorLine => new Models.ThicknessOfLine(2 * HalfThicknessOfGridLine.AsInt);
    #endregion

    #region プロパティ（現在作業中の画面の中でのグリッド・タイル・サイズ）
    /// <summary>
    ///     現在作業中の画面の中でのグリッド・タイル・サイズ
    /// </summary>
    static internal Models.Size WorkingGridTileSize { get; set; } = new Models.Size(new Models.Width(32), new Models.Height(32));
    #endregion

    #region プロパティ（現在作業中の画面の中での選択タイルのサイズ）
    /// <summary>
    ///     <pre>
    ///         現在作業中の画面の中での選択タイルのサイズ
    ///         
    ///         TODO ★ 削除？ IDrawing インスタンスに値を渡すのに使う
    ///     </pre>
    /// </summary>
    static internal Models.Size SelectedTileSize { get; set; } = new Models.Size(new Models.Width(0), new Models.Height(0));
    #endregion

    // - インターナル静的メソッド

    ///// <summary>
    /////     バンドルド・ファイルの読込
    /////     
    /////     <list type="bullet">
    /////         <item>📖 [Microsoft　＞　Bundled Files](https://learn.microsoft.com/en-us/dotnet/maui/platform-integration/storage/file-system-helpers?tabs=windows#bundled-files)</item>
    /////     </list>
    ///// </summary>
    ///// <param name="filePath"></param>
    ///// <returns></returns>
    //internal static async Task<string> ReadTextFile(string filePath)
    //{
    //    using Stream fileStream = await FileSystem.Current.OpenAppPackageFileAsync(filePath);
    //    using StreamReader reader = new StreamReader(fileStream);

    //    return await reader.ReadToEndAsync();
    //}

    // - プライベート静的メソッド

    #region メソッド（構成）
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

    #region メソッド（ユーザー構成）
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

    #region メソッド（設定）
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

    // - その他

    #region その他（生成）
    /// <summary>
    ///     生成
    /// </summary>
    public App()
    {
        // 調査
        {
            Trace.WriteLine($"[App.xaml.cs App] Environment.CurrentDirectory: {Environment.CurrentDirectory}");
            // [App.xaml.cs App] Environment.CurrentDirectory: C:\WINDOWS\system32

            Trace.WriteLine($"[App.xaml.cs App] FileSystem.AppDataDirectory : {FileSystem.AppDataDirectory}");
            // [App.xaml.cs App] FileSystem.AppDataDirectory: C:\Users\むずでょ\AppData\Local\Packages\1802ca7b-559d-489e-8a13-f02ac4d27fcc_9zz4h110yvjzm\LocalState

            Trace.WriteLine($"[App.xaml.cs App] FileSystem.CacheDirectory   : {FileSystem.CacheDirectory}");
            // [App.xaml.cs App] FileSystem.CacheDirectory  : C:\Users\むずでょ\AppData\Local\Packages\1802ca7b-559d-489e-8a13-f02ac4d27fcc_9zz4h110yvjzm\LocalCache

        }

        /*
        // TODO ★ CSV 読取
        {
            using (var reader = new StreamReader("languages.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.CurrentCulture))
            {

            }
        }
        */

        // 初期化（多言語対応）
        {
            // TODO 切替方法はあとで考える
            LocalizationResourceManager.Instance.SetCulture(new CultureInfo("ja-JP"));
        }

        InitializeComponent();

        MainPage = new AppShell();
    }
    #endregion

    // - プライベート静的プロパティー

    #region プロパティ（現在の構成）
    /// <summary>
    ///		現在の構成
    /// 
    ///		<list type="bullet">
    ///			<item>ミュータブル</item>
    ///		</list>
    /// </summary>
    static Models.FileEntries.Configuration Configuration { get; set; }
    #endregion

    #region プロパティ（現在のユーザー構成）
    /// <summary>
    ///		現在のユーザー構成
    /// 
    ///		<list type="bullet">
    ///			<item>ミュータブル</item>
    ///		</list>
    /// </summary>
    static Models.FileEntries.UserConfiguration UserConfiguration { get; set; }
    #endregion

    #region プロパティ（現在の設定）
    /// <summary>
    ///		現在の設定
    /// 
    ///		<list type="bullet">
    ///			<item>ミュータブル</item>
    ///		</list>
    /// </summary>
    static Models.FileEntries.Settings Settings { get; set; }
    #endregion
}
