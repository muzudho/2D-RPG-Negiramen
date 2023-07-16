// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace _2D_RPG_Negiramen.WinUI;

using Microsoft.UI;
using Microsoft.UI.Windowing;
using Windows.Graphics;

/// <summary>
/// Provides application-specific behavior to supplement the default Application class.
/// </summary>
public partial class App : MauiWinUIApplication
{
	/// <summary>
	/// Initializes the singleton application object.  This is the first line of authored code
	/// executed, and as such is the logical equivalent of main() or WinMain().
	/// </summary>
	public App()
	{
		this.InitializeComponent();

        //
        // ウィンドウ・サイズ設定
        // ======================
        //
        // 📖 [.NET MAUI でウインドウサイズを指定する方法(Windowsで実行した場合)](https://developers-trash.com/archives/974)
        //
        // 2023年現在、ディスプレイの解像度は 1920 x 1080 が主流だから、それの９掛けぐらいにしよう
        int WindowWidth = (int)(1920 * 0.9);
        int WindowHeight = (int)(1080 * 0.9);
        Microsoft.Maui.Handlers.WindowHandler.Mapper.AppendToMapping(nameof(IWindow), (handler, view) =>
        {
            var mauiWindow = handler.VirtualView;
            var nativeWindow = handler.PlatformView;
            nativeWindow.Activate();
            IntPtr windowHandle = WinRT.Interop.WindowNative.GetWindowHandle(nativeWindow);
            WindowId windowId = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(windowHandle);
            AppWindow appWindow = Microsoft.UI.Windowing.AppWindow.GetFromWindowId(windowId);
            appWindow.Resize(new SizeInt32(WindowWidth, WindowHeight));
        });
    }

	protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
}

