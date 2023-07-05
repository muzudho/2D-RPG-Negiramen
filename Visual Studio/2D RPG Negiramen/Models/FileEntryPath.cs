namespace _2D_RPG_Negiramen.Models
{
    /// <summary>
    ///     😁 ファイル・エントリー・パス
    ///     
    ///     <list type="bullet">
    ///         <item>ファイルや、フォルダーのパスのこと</item>
    ///     </list>
    /// </summary>
    internal class FileEntryPath
    {
        // - 静的プロパティ

        /// <summary>
        ///     未設定オブジェクト
        /// </summary>
        internal static FileEntryPath Empty { get; } = new FileEntryPath();

        /// <summary>
        ///     文字列を与えて初期化
        /// </summary>
        /// <param name="fileEntryPath">ファイル・エントリー・パス</param>
        /// <param name="replaceSeparators">`\` を `/` へ置換</param>
        /// <returns>実例</returns>
        internal static FileEntryPath FromString(
            string fileEntryPath,
            bool replaceSeparators = false)
        {
            if (fileEntryPath == null)
            {
                throw new ArgumentNullException(nameof(fileEntryPath));
            }

            if (replaceSeparators)
            {
                fileEntryPath = fileEntryPath.Replace("\\", "/");
            }

            return new FileEntryPath(fileEntryPath);
        }

        // - その他

        /// <summary>
        ///     未設定
        /// </summary>
        internal FileEntryPath()
        {
            AsStr = string.Empty;
        }

        /// <summary>
        ///     生成
        /// </summary>
        internal FileEntryPath(string asStr)
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
