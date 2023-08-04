namespace _2D_RPG_Negiramen.Models;

using SkiaSharp;

/// <summary>
///     😁 タイルセット・サムネイル画像ヘルパー
/// </summary>
static class TilesetThumbnailImageHelper
{
    // - インターナル静的メソッド

    /// <summary>
    ///     ビットマップ作成
    /// </summary>
    /// <returns></returns>
    internal static SKBitmap CreateBitmap(
        SKBitmap originalBitmap,
        TilesetThumbnailImageProperties tilesetThumbnailImageProperties)
    {
        // 作業画像のリサイズ
        return originalBitmap.Resize(
            size: new SKSizeI(
                width: tilesetThumbnailImageProperties.Width,
                height: tilesetThumbnailImageProperties.Height),
            quality: SKFilterQuality.Medium);
    }

    /// <summary>
    ///     画像書出し
    /// </summary>
    internal static void WriteImage(
        string thumbnailPathAsStr,
        SKBitmap thumbnailBitmap)
    {
        //
        // 書出先（ウィンドウズ・ローカルＰＣ）
        //
        // 📖 [Using SkiaSharp, how to save a SKBitmap ?](https://social.msdn.microsoft.com/Forums/en-US/25fe8438-8afb-4acf-9d68-09acc6846918/using-skiasharp-how-to-save-a-skbitmap-?forum=xamarinforms)  
        //
        using (Stream outputFileStream = System.IO.File.Open(
            path: thumbnailPathAsStr,
            mode: FileMode.OpenOrCreate))
        {
            // 画像にする
            SKImage skImage = SkiaSharp.SKImage.FromBitmap(thumbnailBitmap);

            // PNG画像にする
            SKData pngImage = skImage.Encode(SKEncodedImageFormat.Png, 100);

            // 出力
            pngImage.SaveTo(outputFileStream);
        }
    }
}
