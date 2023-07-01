namespace _2D_RPG_Negiramen.Models
{
    /// <summary>
    ///     😁 ファイル・パス
    /// </summary>
    abstract class FilePath
    {
        /// <summary>
        ///     生成
        /// </summary>
        internal FilePath()
        {
            this.AsStr = string.Empty;
        }

        /// <summary>
        ///     生成
        /// </summary>
        internal FilePath(string asStr)
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
