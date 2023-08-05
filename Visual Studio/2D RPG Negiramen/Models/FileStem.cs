namespace _2D_RPG_Negiramen.Models
{
    /// <summary>
    ///     😁 ファイル・ステム
    /// </summary>
    public class FileStem
    {
        // - その他

        #region その他（生成　関連）
        /// <summary>
        ///     生成
        /// </summary>
        /// <param name="source">ヌル不可</param>
        /// <returns></returns>
        internal static FileStem FromString(string source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return new FileStem(source);
        }

        /// <summary>
        ///     生成
        /// </summary>
        internal FileStem()
        {
            this.source = string.Empty;
        }

        /// <summary>
        ///     生成
        /// </summary>
        FileStem(string source)
        {
            this.source = source;
        }
        #endregion

        // - パブリック・メソッド

        #region メソッド（暗黙的な文字列形式）
        /// <summary>
        ///     暗黙的な文字列形式
        /// </summary>
        public override string ToString() => source;
        #endregion

        // - インターナル静的プロパティ

        #region プロパティ（空オブジェクト）
        /// <summary>
        ///     空オブジェクト
        /// </summary>
        internal static FileStem Empty { get; } = new FileStem();
        #endregion

        // - インターナル・プロパティ

        #region プロパティ（文字列形式）
        /// <summary>
        ///     文字列形式
        /// </summary>
        internal string AsStr => this.source;
        #endregion

        // - プライベート・プロパティ

        #region プロパティ（入力まま）
        /// <summary>
        ///     入力まま
        /// </summary>
        string source { get; }
        #endregion
    }
}
