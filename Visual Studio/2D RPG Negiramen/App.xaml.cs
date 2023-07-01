namespace _2D_RPG_Negiramen;

using _2D_RPG_Negiramen.Models;

/// <summary>
///     😁 アプリケーション
/// </summary>
public partial class App : Application
{
    // - 静的プロパティー

    /// <summary>
    /// 画面遷移先の一時記憶
    /// </summary>
    static internal Stack<ShellNavigationState> NextPage { get; set; } = new Stack<ShellNavigationState>();

    /// <summary>
    /// グリッド線の半分の太さ
    /// </summary>
    static internal ThicknessOfLine HalfThicknessOfGridLine { get; } = new Models.ThicknessOfLine(1);

    /// <summary>
    /// タイル・カーソルの線の半分の太さ
    /// </summary>
    static internal ThicknessOfLine HalfThicknessOfTileCursorLine => new Models.ThicknessOfLine(2 * HalfThicknessOfGridLine.AsInt);

    // - 静的メソッド

    /// <summary>
    /// 構成ファイルの取得、またはファイル読込
    /// </summary>
    /// <returns>構成ファイル</returns>
    static internal Configuration GetOrLoadConfiguration()
    {
        if (App.Configuration == null)
        {
            // 構成ファイルの読込
            if (Configuration.LoadTOML(out Configuration configuration))
            {
                App.Configuration = configuration;
            }
        }

        return App.Configuration;
    }

    /// <summary>
    /// 構成ファイルをセット
    /// </summary>
    /// <param name="configuration">構成ファイル</param>
    static internal void SetConfiguration(Configuration configuration)
    {
        App.Configuration = configuration;
    }

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
    static Configuration Configuration { get; set; }
}
