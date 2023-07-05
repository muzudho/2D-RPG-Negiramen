﻿namespace _2D_RPG_Negiramen.Models.FileSpace
{
    /// <summary>
    ///     😁 タイル・セット画像ファイル・パス
    /// </summary>
    class TileSetImageFilePath : Models.FilePath
    {
        // - 静的プロパティ

        /// <summary>
        ///     空オブジェクト
        /// </summary>
        internal static TileSetImageFilePath Empty { get; } = new TileSetImageFilePath();

        // - 静的その他

        /// <summary>
        ///     文字列を与えて初期化
        /// </summary>
        /// <param name="filePath">ファイルへのパス</param>
        /// <param name="replaceSeparators">`\` を `/` へ置換</param>
        /// <returns>実例</returns>
        internal static TileSetImageFilePath FromString(
            string filePath,
            bool replaceSeparators = false)
        {
            if (filePath == null)
            {
                throw new ArgumentNullException(nameof(filePath));
            }

            if (replaceSeparators)
            {
                filePath = filePath.Replace("\\", "/");
            }

            return new TileSetImageFilePath(filePath);
        }

        // - その他

        /// <summary>
        ///     生成
        /// </summary>
        internal TileSetImageFilePath()
            : base()
        {
        }

        /// <summary>
        ///     生成
        /// </summary>
        internal TileSetImageFilePath(string asStr)
            : base(asStr: asStr)
        {
        }
    }
}
