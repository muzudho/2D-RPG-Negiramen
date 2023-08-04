namespace _2D_RPG_Negiramen.Models;

using SkiaSharp;

/// <summary>
///     😁 タイルセット画像プロパティーズ
/// </summary>
class TilesetImageProperties
{
    // - その他

    #region その他（生成）
    /// <summary>
    ///     タイルセット画像のプロパティー読込
    /// </summary>
    internal static async Task<TilesetImageProperties> ReadAsync(
        string originalPngPathAsStr)
    {
        SKBitmap originalBitmap;

        // サイズ
        int originalWidth;
        int originalHeight;

        // タイルセット画像読込
        using (Stream inputFileStream = System.IO.File.OpenRead(originalPngPathAsStr))
        {
            // ↓ １つのストリームが使えるのは、１回切り
            using (var memStream = new MemoryStream())
            {
                await inputFileStream.CopyToAsync(memStream);
                memStream.Seek(0, SeekOrigin.Begin);

                // 元画像
                originalBitmap = SkiaSharp.SKBitmap.Decode(memStream);

                originalWidth = originalBitmap.Width;
                originalHeight = originalBitmap.Height;
            };
        }

        return new TilesetImageProperties(
            originalBitmap: originalBitmap,
            originalWidth: originalWidth,
            originalHeight: originalHeight);
    }

    /// <summary>
    ///     生成
    /// </summary>
    /// <param name="originalBitmap"></param>
    TilesetImageProperties(
        SKBitmap originalBitmap,
        int originalWidth,
        int originalHeight)
    {
        this.OriginalBitmap = originalBitmap;
        this.OriginalWidth = originalWidth;
        this.OriginalHeight = originalHeight;
    }
    #endregion

    // - インターナル・プロパティ

    /// <summary>
    ///     元画像のビットマップ
    /// </summary>
    internal SKBitmap OriginalBitmap { get; }

    /// <summary>
    ///     元画像の横幅
    /// </summary>
    internal int OriginalWidth;

    /// <summary>
    ///     元画像の縦幅
    /// </summary>
    internal int OriginalHeight;
}
