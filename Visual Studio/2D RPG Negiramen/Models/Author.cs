namespace _2D_RPG_Negiramen.Models
{
    /// <summary>
    ///     😁 著者
    /// </summary>
    class Author
    {
        // - その他

        #region その他（生成　関連）
        /// <summary>
        ///     生成
        /// </summary>
        internal Author()
        {
            this.AsStr = string.Empty;
        }

        /// <summary>
        ///     生成
        /// </summary>
        internal Author(string asStr)
        {
            this.AsStr = asStr;
        }
        #endregion

        // パブリック・メソッド

        #region メソッド（文字列化）
        /// <summary>
        ///     暗黙的な文字列形式
        /// </summary>
        public override string ToString() => AsStr;
        #endregion

        // - インターナル静的プロパティ

        #region プロパティ（空オブジェクト）
        /// <summary>
        ///     空オブジェクト
        /// </summary>
        internal static Author Empty { get; } = new Author();
        #endregion

        #region プロパティ（文字列を与えて初期化）
        /// <summary>
        ///     文字列を与えて初期化
        /// </summary>
        /// <param name="source">ソース</param>
        /// <returns>実例</returns>
        internal static Author FromString(string source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return new Author(source);
        }
        #endregion

        // - インターナル・プロパティ

        #region プロパティ（文字列形式）
        /// <summary>
        ///     文字列形式
        /// </summary>
        internal string AsStr { get; }
        #endregion
    }
}
