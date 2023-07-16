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

            // 手動で追加（MVVM や、ボタンのアニメーションのために）
            // 📖 [.NET MAUI Community Toolkit Popup PopupHandler is incompatible](https://stackoverflow.com/questions/72506202/net-maui-community-toolkit-popup-popuphandler-is-incompatible)
            .UseMauiCommunityToolkit()

            // 手動で追加（画像処理のために）
            // 📖 [[BUG] MAUI: SKCanvasView crash, unable to display SKBitmap directly #2139](https://github.com/mono/SkiaSharp/issues/2139)
            .UseSkiaSharp()

            // フォント設定
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
