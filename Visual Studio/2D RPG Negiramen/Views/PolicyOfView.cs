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
}
