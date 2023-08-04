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
        int thumbnailWidth;
        int thumbnailHeight;

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

                // TODO サムネイル画像のサイズをここで決めるのはおかしい
                int longLength = Math.Max(originalWidth, originalHeight);
                int shortLength = Math.Min(originalWidth, originalHeight);
                // 長い方が 128 より大きければ縮める
                if (128 < longLength)
                {
                    float rate = (float)longLength / 128.0f;
                    thumbnailWidth = (int)(originalWidth / rate);
                    thumbnailHeight = (int)(originalHeight / rate);
                }
                else
                {
                    thumbnailWidth = originalWidth;
                    thumbnailHeight = originalHeight;
                }
            };
        }

        return new TilesetImageProperties(
            originalBitmap: originalBitmap,
            originalWidth: originalWidth,
            originalHeight: originalHeight,
            thumbnailWidth: thumbnailWidth,
            thumbnailHeight: thumbnailHeight);
    }

    /// <summary>
    ///     生成
    /// </summary>
    /// <param name="originalBitmap"></param>
    TilesetImageProperties(
        SKBitmap originalBitmap,
        int originalWidth,
        int originalHeight,
        int thumbnailWidth,
        int thumbnailHeight)
    {
        this.OriginalBitmap = originalBitmap;
        this.OriginalWidth = originalWidth;
        this.OriginalHeight = originalHeight;
        this.ThumbnailWidth = thumbnailWidth;
        this.ThumbnailHeight = thumbnailHeight;
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

    /// <summary>
    ///     サムネイル画像の横幅
    /// </summary>
    internal int ThumbnailWidth;

    /// <summary>
    ///     サムネイル画像の縦幅
    /// </summary>
    internal int ThumbnailHeight;

}
