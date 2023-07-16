namespace _2D_RPG_Negiramen.Models.Drawing
{
    /// <summary>
    ///     😁 作業中のタイルセット画像
    /// </summary>
    internal class WorkingTilesetImage : IDrawable
    {
        /// <summary>
        ///     描画
        /// </summary>
        /// <param name="canvas">キャンバス</param>
        /// <param name="dirtyRect">矩形</param>
        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            // TODO 毎回、ファイルへの入出力をしていたら遅いので、グローバル変数で制御したい
        }
    }
}
