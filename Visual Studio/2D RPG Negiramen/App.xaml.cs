using _2D_RPG_Negiramen.Models;

namespace _2D_RPG_Negiramen;

public partial class App : Application
{
	/// <summary>
	///		現在の構成
	/// 
	///		<list type="bullet">
	///			<item>ミュータブル</item>
	///		</list>
	/// </summary>
	static internal Configuration Configuration { get; set; } = new Configuration();

	/// <summary>
	/// 画面遷移先の一時記憶
	/// </summary>
	static internal Stack<ShellNavigationState> NextPage { get; set; } = new Stack<ShellNavigationState>();

    /// <summary>
    /// 生成
    /// </summary>
    public App()
	{
		InitializeComponent();

		MainPage = new AppShell();
	}
}
