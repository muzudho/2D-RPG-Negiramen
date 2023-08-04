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
        SKBitmap bitmap;

        // サイズ
        int width;
        int height;

        // タイルセット画像読込
        using (Stream inputFileStream = System.IO.File.OpenRead(originalPngPathAsStr))
        {
            // ↓ １つのストリームが使えるのは、１回切り
            using (var memStream = new MemoryStream())
            {
                await inputFileStream.CopyToAsync(memStream);
                memStream.Seek(0, SeekOrigin.Begin);

                // 元画像
                bitmap = SkiaSharp.SKBitmap.Decode(memStream);

                width = bitmap.Width;
                height = bitmap.Height;
            };
        }

        return new TilesetImageProperties(
            bitmap: bitmap,
            width: width,
            height: height);
    }

    /// <summary>
    ///     生成
    /// </summary>
    /// <param name="bitmap"></param>
    TilesetImageProperties(
        SKBitmap bitmap,
        int width,
        int height)
    {
        this.Bitmap = bitmap;
        this.Width = width;
        this.Height = height;
    }
    #endregion

    // - インターナル・プロパティ

    /// <summary>
    ///     ビットマップ
    /// </summary>
    internal SKBitmap Bitmap { get; }

    /// <summary>
    ///     横幅
    /// </summary>
    internal int Width;

    /// <summary>
    ///     縦幅
    /// </summary>
    internal int Height;
}
