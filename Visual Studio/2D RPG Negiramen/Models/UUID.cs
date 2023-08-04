namespace _2D_RPG_Negiramen.Models
{
    using System;

    /// <summary>
    ///     😁 UUID
    /// </summary>
    class UUID
    {
        // - その他

        #region その他（生成　関連）
        /// <summary>
        ///     文字列を与えて初期化
        /// </summary>
        /// <param name="source">ソース</param>
        /// <returns>実例</returns>
        internal static UUID FromString(string source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return new UUID(source);
        }

        /// <summary>
        ///     生成
        /// </summary>
        internal UUID()
        {
            this.Source = Guid.NewGuid().ToString().ToUpper();
        }

        /// <summary>
        ///     生成
        /// </summary>
        UUID(string source)
        {
            this.Source = source;
        }
        #endregion

        // - パブリック・メソッド

        #region メソッド（暗黙的な文字列形式）
        /// <summary>
        ///     暗黙的な文字列形式
        /// </summary>
        public override string ToString() => Source;
        #endregion

        // - インターナル静的プロパティ

        #region プロパティ（空オブジェクト）
        /// <summary>
        ///     空オブジェクト
        /// </summary>
        internal static UUID Empty { get; } = new UUID(string.Empty);
        #endregion

        // - インターナル・プロパティ

        #region プロパティ（文字列形式）
        /// <summary>
        ///     文字列形式
        /// </summary>
        internal string AsStr => this.Source;
        #endregion

        // - プライベート・プロパティ

        #region プロパティ（入力まま）
        /// <summary>
        ///     入力まま
        /// </summary>
        string Source { get; }
        #endregion
    }
}
