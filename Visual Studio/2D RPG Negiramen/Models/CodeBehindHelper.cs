namespace _2D_RPG_Negiramen.Models;

using _2D_RPG_Negiramen.Models;
using _2D_RPG_Negiramen.ViewModels;
using CommunityToolkit.Maui.Views;
using System.Diagnostics;
using _2D_RPG_Negiramen.Views;

/// <summary>
///     😁 コードビハインド・ヘルパー
/// </summary>
static internal class CodeBehindHelper
{
    // - パブリック静的メソッド

    #region メソッド（環境が構成ファイル通りか判定する）
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
    public static async Task ReadyGoToNext(
        Func<Task> onOk,
        Func<Task> onNotYetConfiguration)
    {
        // 構成を取得
        var configuration = App.GetOrLoadConfiguration();

        // 構成通り準備できているなら、そのまま画面遷移する
        if (ProjectHelper.IsReady())
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
    #endregion

    #region メソッド（構成ページへ移動）
    /// <summary>
    ///     <pre>
    ///         構成ページへ移動
    ///         
    ///         本来の移動先をグローバル変数へ記憶して、構成ページへ移動。
    ///         構成が終わったら、一旦構成ページから戻ったあと、本来の移動先へ遷移
    ///     </pre>
    /// </summary>
    /// <param name="contentPage">コンテント・ページ</param>
    /// <param name="shellNavigationState">本来の移動先</param>
    public static async Task GoToConfigurationPage(ContentPage contentPage, ShellNavigationState shellNavigationState)
    {
        App.NextPage.Push(shellNavigationState);
        await contentPage.Navigation.PushAsync(new ConfigurationPage());
        // ここは通り抜ける。恐らく、UIスレッドを抜けた後に画面遷移する
    }
    #endregion
}
