namespace _2D_RPG_Negiramen.FeatSkia;

using SkiaSharp;

/// <summary>
///     明度を下げる
/// </summary>
internal static class ReduceBrightness
{
    // - インターナル静的メソッド

    /// <summary>
    ///     そうする
    /// </summary>
    internal static SKBitmap DoItInPlace(SKBitmap bitmapInPlace)
    {
        IntPtr pixelsAddr = bitmapInPlace.GetPixels();
        int width = bitmapInPlace.Width;
        int height = bitmapInPlace.Height;

        unsafe
        {
            uint* ptr = (uint*)pixelsAddr.ToPointer();

            for (int row = 0; row < height; row++)
                for (int col = 0; col < width; col++)
                {
                    // ビッグ・エンディアンか、リトル・エンディアンかの違いを吸収してくれることを期待して
                    SKColor color = new SKColor(*ptr);

                    // RGB値が減れば、暗くなるだろ
                    *ptr++ = (uint)new SKColor(
                        red: (byte)(color.Red * 0.7),
                        green: (byte)(color.Green * 0.7),
                        blue: (byte)(color.Blue * 0.7));
                }
        }

        return bitmapInPlace;
    }
}
