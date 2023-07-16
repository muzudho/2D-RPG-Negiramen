namespace _2D_RPG_Negiramen.Views
{
    /// <summary>
    ///     ボタンのアニメーション
    /// </summary>
    internal static class ButtonAnimationHelper
    {
        /// <summary>
        ///     そうする
        /// </summary>
        internal static async Task DoIt(Button button)
        {
            await button.FadeTo(
                opacity: 0.5,
                length: 150); // milliseconds
            await button.FadeTo(
                opacity: 1.0,
                length: 150); // milliseconds
        }
    }
}
