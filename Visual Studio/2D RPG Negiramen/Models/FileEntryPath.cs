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
        /// <param name="fileEntryPathSource">ファイル・エントリー・パス（セパレーター置換前、変数展開前）</param>
        /// <param name="replaceSeparators">`\` を `/` へ置換</param>
        /// <param name="expandVariables">変数展開用の辞書</param>
        /// <returns>実例</returns>
        internal static FileEntryPath From(
            FileEntryPathSource fileEntryPathSource,
            bool replaceSeparators = false,
            Dictionary<string, string> expandVariables = null)
        {
            if (fileEntryPathSource == null)
            {
                throw new ArgumentNullException(nameof(fileEntryPathSource));
            }

            // セパレーター置換後、変数展開後
            var editedPathAsStr = fileEntryPathSource.AsStr;

            if (replaceSeparators)
            {
                editedPathAsStr = editedPathAsStr.Replace("\\", "/");
            }

            // 変数展開
            if (expandVariables != null)
            {
                foreach(var pair in expandVariables)
                {
                    editedPathAsStr = editedPathAsStr.Replace(pair.Key, pair.Value);
                }
            }

            return new FileEntryPath(editedPathAsStr);
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
