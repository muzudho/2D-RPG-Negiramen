/// <summary>
///     😁 ２Ｄ ＲＰＧ ネギラーメン
/// </summary>
namespace _2D_RPG_Negiramen;

using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		//
        // 手動で追加。こんなん分からんて
		//
        // 📖 [.NET MAUI Community Toolkit Popup PopupHandler is incompatible](https://stackoverflow.com/questions/72506202/net-maui-community-toolkit-popup-popuphandler-is-incompatible)
		//
        builder.UseMauiApp<App>().UseMauiCommunityToolkit();

#if DEBUG
        builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
