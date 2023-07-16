namespace _2D_RPG_Negiramen.Models.Drawing;

using _2D_RPG_Negiramen.Models.FileEntries;
using System.Numerics;

internal class ColoredMap : BindableObject, IDrawable
{
    // - パブリック束縛可能プロパティ

    #region 束縛可能プロパティ（タイル・セット設定）
    /// <summary>
    ///     タイル・セット設定
    /// </summary>
    public TileSetSettings TileSetSettings
    {
        get => (TileSetSettings)GetValue(TileSetSettingsProperty);
        set => SetValue(TileSetSettingsProperty, value);
    }

    /// <summary>
    ///     タイル・セット設定
    /// </summary>
    public static BindableProperty TileSetSettingsProperty = BindableProperty.Create(
        // プロパティ名
        propertyName: nameof(TileSetSettings),
        // 返却型
        returnType: typeof(TileSetSettings),
        // これを含んでいるクラス
        declaringType: typeof(ColoredMap));
    #endregion

    /// <summary>
    ///     描画
    /// </summary>
    /// <param name="canvas">キャンバス</param>
    /// <param name="dirtyRect">位置とサイズ</param>
    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        if (this.TileSetSettings == null)
        {
            return;
        }

        // 各登録タイル
        foreach (var record in this.TileSetSettings.RecordList)
        {
            // Trace.WriteLine($"[TilePaletteEditPage.xaml.cs ContentPage_Loaded] Record: {record.Dump()}");

            if (record.LogicalDelete == Models.LogicalDelete.True)
            {
                // 論理削除されてるから無視
                continue;
            }

            // 枠の線の太さの半分
            int halfFrameThickness = 2;

            // ランダム色作成
            {
                int minValue = 160;
                int maxValue = 255;
                int[] values = new int[3] { minValue, maxValue, Random.Shared.Next(minValue, maxValue) };

                // シャッフル
                MathEx.FisherYatesShuffle(values);

                canvas.StrokeColor = new Color(
                    red: values[0],
                    green: values[1],
                    blue: values[2]);
            }
            // canvas.StrokeColor = Colors.Green;
            // canvas.StrokeColor = new Color(red:220, green:220, blue:255, alpha:192);
            //canvas.StrokeColor = new Color(
            //    red: Random.Shared.Next(220,255),
            //    green: Random.Shared.Next(220, 255),
            //    blue: Random.Shared.Next(220, 255));

            canvas.StrokeSize = 2 * halfFrameThickness;
            canvas.DrawRoundedRectangle(
                // 枠の線の太さの半分だけサイズを縮める
                rect: record.Rectangle.AsGraphis().Inflate(-halfFrameThickness, -halfFrameThickness),
                cornerRadius: 16.0d);

            //canvas.FillColor = new Color(220, 220, 220, 96);
            //canvas.FillRectangle(record.Rectangle.AsGraphis());


        }
    }
}

