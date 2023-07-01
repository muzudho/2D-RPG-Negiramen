namespace _2D_RPG_Negiramen.Models
{
    /// <summary>
    ///     😁 コメント
    /// </summary>
    class Comment
    {
        // - 静的プロパティ

        /// <summary>
        ///     空オブジェクト
        /// </summary>
        internal static Comment Empty { get; } = new Comment();

        /// <summary>
        ///     文字列を与えて初期化
        /// </summary>
        /// <param name="comment">コメント</param>
        /// <returns>実例</returns>
        internal static Comment FromStringAndReplaceSeparators(string comment)
        {
            if (comment == null)
            {
                throw new ArgumentNullException(nameof(comment));
            }

            return new Comment(comment);
        }

        /// <summary>
        ///     生成
        /// </summary>
        internal Comment()
        {
            this.AsStr = string.Empty;
        }

        /// <summary>
        ///     生成
        /// </summary>
        internal Comment(string asStr)
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
