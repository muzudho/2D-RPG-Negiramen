namespace _2D_RPG_Negiramen;

using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using SkiaSharp.Views.Maui.Controls.Hosting;

/// <summary>
///		😁 マウイ・プログラム
/// </summary>
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
        // MVVM をやり始めるときに、こう書く
        // =================================
        //
        // 手動で追加。こんなん分からんて
        //
        // 📖 [.NET MAUI Community Toolkit Popup PopupHandler is incompatible](https://stackoverflow.com/questions/72506202/net-maui-community-toolkit-popup-popuphandler-is-incompatible)
        //
        builder.UseMauiApp<App>().UseMauiCommunityToolkit();


        //
        // SkiaSharp を直で使いたいときは、こう書く
        // ========================================
        //
        // 手動で追加。こんなん分からんて
        //
        // 📖 [[BUG] MAUI: SKCanvasView crash, unable to display SKBitmap directly #2139](https://github.com/mono/SkiaSharp/issues/2139)
        //
        builder.UseMauiApp<App>().UseSkiaSharp();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
