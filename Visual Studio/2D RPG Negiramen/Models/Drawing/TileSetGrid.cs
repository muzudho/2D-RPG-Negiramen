namespace _2D_RPG_Negiramen.Models.Drawing
{
    using Microsoft.Maui.Graphics;
    using System.Diagnostics;

    /// <summary>
    ///     😁 タイルセット用のグリッド
    /// </summary>
    internal class TilesetGrid : BindableObject, IDrawable
    {
        // - パブリック束縛可能プロパティ

        #region 束縛可能プロパティ（グリッド・キャンバス画像のサイズ）
        /// <summary>
        ///     グリッド・キャンバス画像のサイズ
        /// </summary>

/* プロジェクト '2D RPG Negiramen (net7.0-windows10.0.19041.0)' からのマージされていない変更
前:
        public Models.SizeInt GridCanvasImageSize
後:
        public SizeInt GridCanvasImageSize
*/
        public Geometric.SizeInt GridCanvasImageSize
        {

/* プロジェクト '2D RPG Negiramen (net7.0-windows10.0.19041.0)' からのマージされていない変更
前:
            get => (Models.SizeInt)GetValue(GridCanvasImageSizeProperty);
後:
            get => (SizeInt)GetValue(GridCanvasImageSizeProperty);
*/
            get => (Geometric.SizeInt)GetValue(GridCanvasImageSizeProperty);
            set => SetValue(GridCanvasImageSizeProperty, value);
        }

        /// <summary>
        ///     グリッド・キャンバス画像のサイズ
        /// </summary>
        public static BindableProperty GridCanvasImageSizeProperty = BindableProperty.Create(
            // プロパティ名
            propertyName: nameof(GridCanvasImageSize),
            // 返却型

/* プロジェクト '2D RPG Negiramen (net7.0-windows10.0.19041.0)' からのマージされていない変更
前:
            returnType: typeof(Models.SizeInt),
後:
            returnType: typeof(SizeInt),
*/
            returnType: typeof(Geometric.SizeInt),
            // これを含んでいるクラス
            declaringType: typeof(TilesetGrid));
        #endregion

        #region 束縛可能プロパティ（グリッドの線の太さの半分）
        /// <summary>
        ///     グリッドの線の太さの半分
        /// </summary>
        public int HalfThicknessOfGridLineAsInt
        {
            get => (int)GetValue(HalfThicknessOfGridLineAsIntProperty);
            set => SetValue(HalfThicknessOfGridLineAsIntProperty, value);
        }

        /// <summary>
        ///     グリッドの線の太さの半分
        /// </summary>
        public static BindableProperty HalfThicknessOfGridLineAsIntProperty = BindableProperty.Create(
            // プロパティ名
            propertyName: nameof(HalfThicknessOfGridLineAsInt),
            // 返却型
            returnType: typeof(int),
            // これを含んでいるクラス
            declaringType: typeof(TilesetGrid));
        #endregion

        #region 束縛可能プロパティ（グリッド位相の左上表示位置）
        /// <summary>
        ///     グリッド位相の左上表示位置
        /// </summary>
        public Geometric.PointFloat GridPhase
        {
            get => (Geometric.PointFloat)GetValue(GridPhaseProperty);
            set => SetValue(GridPhaseProperty, value);
        }

        /// <summary>
        ///     グリッド位相の左上表示位置
        /// </summary>
        public static BindableProperty GridPhaseProperty = BindableProperty.Create(
            // プロパティ名
            propertyName: nameof(GridPhase),
            // 返却型
            returnType: typeof(Geometric.PointFloat),
            // これを含んでいるクラス
            declaringType: typeof(TilesetGrid),
            // ヌルだと不具合が出る
            defaultValue: Geometric.PointFloat.Empty);
        #endregion

        #region 束縛可能プロパティ（グリッド・タイル　関連）
        /// <summary>
        ///     グリッド・タイル・サイズ
        /// </summary>
        public Geometric.SizeFloat GridTileSize
        {
            get => (Geometric.SizeFloat)GetValue(GridTileSizeProperty);
            set => SetValue(GridTileSizeProperty, value);
        }

        /// <summary>
        ///     グリッド・タイル・サイズ
        /// </summary>
        public static BindableProperty GridTileSizeProperty = BindableProperty.Create(
            // プロパティ名
            propertyName: nameof(GridTileSize),
            // 返却型
            returnType: typeof(Geometric.SizeFloat),
            // これを含んでいるクラス
            declaringType: typeof(TilesetGrid),
            // ヌルだと不具合が出る
            defaultValue: Geometric.SizeFloat.Empty);
        #endregion

        // - パブリック・メソッド

        /// <summary>
        ///     描画
        /// </summary>
        /// <param name="canvas">キャンバス</param>
        /// <param name="dirtyRect">矩形</param>
        /// <exception cref="NotImplementedException"></exception>
        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            Trace.WriteLine($"[TilesetGrid Draw] this.HalfThicknessOfGridLineAsInt: {this.HalfThicknessOfGridLineAsInt}, this.SourceGridPhase: {this.GridPhase.Dump()}, this.SourceGridTileSize: {this.GridTileSize.Dump()} dirtyRect: ({dirtyRect.Width:.2}, {dirtyRect.Height:.2})");

            // 線の色
            canvas.StrokeColor = new Color(255, 0, 0, 127);

            // グリッドの線の太さの半分
            int halfThicknessOfLineAsInt = this.HalfThicknessOfGridLineAsInt;

            // グリッドの線の太さ
            Models.ThicknessOfLine lineThickness = new Models.ThicknessOfLine(2 * halfThicknessOfLineAsInt);
            canvas.StrokeSize = lineThickness.AsInt;

            //
            // 縦線を引いていこう
            // ==================
            //
            {
                float y1 = halfThicknessOfLineAsInt + this.GridPhase.Y.AsFloat;
                float y2 = this.GridCanvasImageSize.Height.AsInt + halfThicknessOfLineAsInt + this.GridPhase.Y.AsFloat;

                float prevX;
                float x = 0;
                for (var i = 0; x < this.GridCanvasImageSize.Width.AsInt + halfThicknessOfLineAsInt; i++)
                {
                    prevX = x;
                    x = i * this.GridTileSize.Width.AsFloat + this.GridPhase.X.AsFloat + halfThicknessOfLineAsInt;

                    if (x <= prevX)
                    {
                        // 単調増加していないのなら停止
                        break;
                    }

                    canvas.DrawLine(x, y1, x, y2);
                }
            }

            //
            // 横線を引いていこう
            // ==================
            //
            {
                float x1 = halfThicknessOfLineAsInt + this.GridPhase.X.AsFloat;

                // CANCEL CODE: 横幅が偶数なら横幅を +1、奇数なら横幅を -1 するという TRICK CODE が別の箇所にあるので、
                //              imageWidth は +1 したり、 -1 したり振動している。これはつらい。
                //              そこで、右辺にもグリッドの線があるから　端まで線を引かなくていいことを利用し
                //              右辺の線の手前まで線を引くようにする
                float x2 = this.GridCanvasImageSize.Width.AsInt - halfThicknessOfLineAsInt + this.GridPhase.X.AsFloat;

                float prevY;
                float y = 0;
                for (var i = 0; y < this.GridCanvasImageSize.Height.AsInt + halfThicknessOfLineAsInt; i++)
                {
                    prevY = y;
                    y = i * this.GridTileSize.Height.AsFloat + this.GridPhase.Y.AsFloat + halfThicknessOfLineAsInt;

                    if (y <= prevY)
                    {
                        // 単調増加していないのなら停止
                        break;
                    }

                    canvas.DrawLine(x1, y, x2, y);
                }
            }
        }
    }
}
