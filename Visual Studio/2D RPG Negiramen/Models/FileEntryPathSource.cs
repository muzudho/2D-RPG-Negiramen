namespace _2D_RPG_Negiramen.Models
{
    /// <summary>
    ///     😁 設定ファイルに記述されたままのファイル・パス
    ///     
    ///     <list type="bullet">
    ///         <item>セパレーター置換前</item>
    ///         <item>変数展開前</item>
    ///     </list>
    /// </summary>
    internal class FileEntryPathSource
    {
        // - 静的プロパティ

        /// <summary>
        ///     未設定オブジェクト
        /// </summary>
        internal static FileEntryPathSource Empty { get; } = new FileEntryPathSource();

        /// <summary>
        ///     文字列を与えて初期化
        /// </summary>
        /// <param name="fileEntryPathAsStr">ファイル・エントリー・パス（セパレーター置換前、変数展開前）</param>
        /// <returns>実例</returns>
        internal static FileEntryPathSource FromString(
            string fileEntryPathAsStr)
        {
            if (fileEntryPathAsStr == null)
            {
                throw new ArgumentNullException(nameof(fileEntryPathAsStr));
            }

            return new FileEntryPathSource(fileEntryPathAsStr);
        }

        // - その他

        /// <summary>
        ///     未設定
        /// </summary>
        internal FileEntryPathSource()
        {
            AsStr = string.Empty;
        }

        /// <summary>
        ///     生成
        /// </summary>
        internal FileEntryPathSource(string asStr)
        {
            AsStr = asStr;
        }

        // - インターナル・プロパティー

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
