namespace _2D_RPG_Negiramen.Models.Drawing
{
    using Microsoft.Maui.Graphics;
    using System.Diagnostics;

    /// <summary>
    ///     <pre>
    ///         😁 タイル上のカーソル
    /// 
    ///         📖 [.net MAUI: how to draw on canvas](https://stackoverflow.com/questions/71001039/net-maui-how-to-draw-on-canvas)
    ///     </pre>
    /// </summary>
    internal class TileCursor : BindableObject, IDrawable
    {
        // - パブリック束縛可能プロパティ

        #region 束縛可能プロパティ（タイル・カーソルの線の半分の太さ）
        /// <summary>
        ///     タイル・カーソルの線の半分の太さ
        /// </summary>
        public ThicknessOfLine HalfThicknessOfTileCursorLine
        {
            get => (ThicknessOfLine)GetValue(HalfThicknessOfTileCursorLineProperty);
            set => SetValue(HalfThicknessOfTileCursorLineProperty, value);
        }

        /// <summary>
        ///     タイル・カーソルの線の半分の太さ
        /// </summary>
        public static BindableProperty HalfThicknessOfTileCursorLineProperty = BindableProperty.Create(
            // プロパティ名
            propertyName: nameof(HalfThicknessOfTileCursorLine),
            // 返却型
            returnType: typeof(ThicknessOfLine),
            // これを含んでいるクラス
            declaringType: typeof(TileSetGrid),
            defaultValue: ThicknessOfLine.Empty);
        #endregion

        #region 束縛可能プロパティ（選択タイルのサイズ）
        /// <summary>
        ///     選択タイルのサイズ
        /// </summary>
        public Models.Size SelectedTileSize
        {
            get => (Models.Size)GetValue(SelectedTileSizeProperty);
            set => SetValue(SelectedTileSizeProperty, value);
        }

        /// <summary>
        ///     現在作業中の画面の中での選択タイルのサイズ
        /// </summary>
        public static BindableProperty SelectedTileSizeProperty = BindableProperty.Create(
            // プロパティ名
            propertyName: nameof(SelectedTileSize),
            // 返却型
            returnType: typeof(Models.Size),
            // これを含んでいるクラス
            declaringType: typeof(TileSetGrid),
            defaultValue: Models.Size.Empty);
        #endregion

        #region 束縛可能プロパティ（ポインティング・デバイス押下中か？）
        /// <summary>
        ///     ポインティング・デバイス押下中か？
        /// </summary>
        public bool SelectingOnPointingDevice
        {
            get => (bool)GetValue(SelectingOnPointingDeviceProperty);
            set => SetValue(SelectingOnPointingDeviceProperty, value);
        }

        /// <summary>
        ///     ポインティング・デバイス押下中か？
        /// </summary>
        public static BindableProperty SelectingOnPointingDeviceProperty = BindableProperty.Create(
            // プロパティ名
            propertyName: nameof(SelectingOnPointingDevice),
            // 返却型
            returnType: typeof(bool),
            // これを含んでいるクラス
            declaringType: typeof(TileSetGrid));
        #endregion

        // - パブリック・メソッド

        /// <summary>
        ///     描画
        /// </summary>
        /// <param name="canvas">キャンバス</param>
        /// <param name="dirtyRect">位置とサイズ</param>
        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            if (this.SelectedTileSize.Width.AsInt < 1 || this.SelectedTileSize.Height.AsInt < 1)
            {
                // カーソルが無いケース
                // Trace.WriteLine($"[TileCursor Draw] カーソルに大きさが無いから描画しない。  this.SelectingOnPointingDevice: {this.SelectingOnPointingDevice}, this.HalfThicknessOfTileCursorLine.AsInt: {this.HalfThicknessOfTileCursorLine.AsInt}");
                return;
            }

            try
            {
                // Trace.WriteLine($"[TileCursor Draw] this.SelectingOnPointingDevice: {this.SelectingOnPointingDevice}, this.HalfThicknessOfTileCursorLine.AsInt: {this.HalfThicknessOfTileCursorLine.AsInt}, this.SelectedTileSize: {this.SelectedTileSize.Dump()}");
            }
            catch
            {
                return;
            }

            // 線の色
            if (this.SelectingOnPointingDevice)
            {
                // 選択中
                canvas.StrokeColor = new Color(0, 0, 255, 95);
                // Trace.WriteLine("[TileCursor Draw] 半透明の青いカーソル");
            }
            else
            {
                // 確定時
                canvas.StrokeColor = Colors.Blue;
                // Trace.WriteLine("[TileCursor Draw] 青いカーソル");
            }


            // タイル・カーソルの線の太さの半分
            int halfThicknessOfLineAsInt = this.HalfThicknessOfTileCursorLine.AsInt;

            // タイル・カーソルの線の太さ
            Models.ThicknessOfLine thickness = new Models.ThicknessOfLine(2 * halfThicknessOfLineAsInt);
            canvas.StrokeSize = thickness.AsInt;

            // 選択タイルのサイズ
            Models.Size tileCursorSize = this.SelectedTileSize;

            // キャンバス・サイズいっぱいにタイル・カーソルを描画
            canvas.DrawRectangle(new Rect(
                // タイル・カーソルの位置を調整するのは、キャンバス自体の位置を動かすこと
                // 端が切れないように、線の太さの半分をずらして描画
                halfThicknessOfLineAsInt,
                halfThicknessOfLineAsInt,
                // 境界線上ではなく、境界線に外接するように描くために、線の太さの半分をずらして描画
                tileCursorSize.Width.AsInt + thickness.AsInt,
                tileCursorSize.Height.AsInt + thickness.AsInt));
        }
    }
}
