namespace _2D_RPG_Negiramen.Models
{
    /// <summary>
    ///     😁 タイル・タイトル
    /// </summary>
    class TileTitle
    {
        // - 静的プロパティ

        /// <summary>
        ///     空オブジェクト
        /// </summary>
        internal static TileTitle Empty { get; } = new TileTitle();

        /// <summary>
        ///     文字列を与えて初期化
        /// </summary>
        /// <param name="title">タイトル</param>
        /// <returns>実例</returns>
        internal static TileTitle FromString(string title)
        {
            if (title == null)
            {
                throw new ArgumentNullException(nameof(title));
            }

            return new TileTitle(title);
        }

        /// <summary>
        ///     生成
        /// </summary>
        internal TileTitle()
        {
            this.AsStr = string.Empty;
        }

        /// <summary>
        ///     生成
        /// </summary>
        internal TileTitle(string asStr)
        {
            this.AsStr = asStr;
        }

        /// <summary>
        ///     文字列形式
        /// </summary>
        internal string AsStr { get; }

        /// <summary>
        ///     暗黙的な文字列形式
        /// </summary>
        public override string ToString() => AsStr;
    }
}
