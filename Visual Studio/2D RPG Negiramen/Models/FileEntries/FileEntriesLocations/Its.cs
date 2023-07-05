namespace _2D_RPG_Negiramen.Models.FileEntries.FileEntriesLocations
{
    /// <summary>
    ///     😁 ファイル・ロケーション
    /// </summary>
    abstract class Its
    {
        /// <summary>
        ///     生成
        /// </summary>
        internal Its()
        {
            AsStr = string.Empty;
        }

        /// <summary>
        ///     生成
        /// </summary>
        internal Its(string asStr)
        {
            AsStr = asStr;
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
