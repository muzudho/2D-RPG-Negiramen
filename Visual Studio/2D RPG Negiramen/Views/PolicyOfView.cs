namespace _2D_RPG_Negiramen.Views;

using _2D_RPG_Negiramen.Models;

/// <summary>
///     ビューの方策
/// </summary>
internal static class PolicyOfView
{
    /// <summary>
    ///     ボタン押下時のボタンの反応
    /// </summary>
    internal static async Task ReactOnPushed(Button button)
    {
        await button.FadeTo(
            opacity: 0.5,
            length: 125); // milliseconds
        await button.FadeTo(
            opacity: 1.0,
            length: 125); // milliseconds
    }

    /// <summary>
    ///     マウスカーソルのホバーによるボタンの反応
    /// </summary>
    /// <param name="button"></param>
    internal static void ReactOnMouseEntered(Button button)
    {
        // 色変更
        // TODO スタイル指定は外部ファイルで行いたい
        button.BackgroundColor = Colors.DodgerBlue;
    }

    /// <summary>
    ///     ボタンの上からマウスカーソルが外れることによるボタンの反応
    /// </summary>
    /// <param name="button"></param>
    internal static void ReactOnMouseLeaved(Button button)
    {
        // ボタンの色変更
        // TODO スタイルの名前のハードコーディングは止めたい
        if (ResourcesHelper.TryFind("Primary", out var color))
        {
            button.BackgroundColor = (Color)color;
        }
    }
}
