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
        // 透明化と縮小は同時にやってほしいが、並列にしても　うまくいかなかった
        await button.FadeTo(
            opacity: 0.5,
            length: 100); // milliseconds

        await button.ScaleTo(
            scale: 0.9,
            length: 100);

        await button.ScaleTo(
            scale: 1.0,
            length: 100);

        await button.FadeTo(
            opacity: 1.0,
            length: 100);
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

    /*
    /// <summary>
    ///     マウスカーソルのホバーによるピッカーの反応
    /// </summary>
    /// <param name="button"></param>
    internal static void ReactOnMouseEntered(Picker picker)
    {
        // 色変更
        // TODO スタイル指定は外部ファイルで行いたい
        picker.TextColor = Colors.DodgerBlue;
        picker.BackgroundColor = Colors.DodgerBlue;
    }

    /// <summary>
    ///     ボタンの上からマウスカーソルが外れることによるピッカーの反応
    /// </summary>
    /// <param name="button"></param>
    internal static void ReactOnMouseLeaved(Picker picker)
    {
        // ボタンの色変更
        // TODO スタイルの名前のハードコーディングは止めたい
        if (ResourcesHelper.TryFind("Primary", out var color))
        {
            picker.BackgroundColor = (Color)color;
        }
    }
    */
}
