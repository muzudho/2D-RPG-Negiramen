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
        public Geometric.SizeInt GridCanvasImageSize
        {
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
        public Geometric.PointFloat GridPhaseWorkingLocation
        {
            get => (Geometric.PointFloat)GetValue(GridPhaseWorkingLocationProperty);
            set => SetValue(GridPhaseWorkingLocationProperty, value);
        }

        /// <summary>
        ///     グリッド位相の左上表示位置
        /// </summary>
        public static BindableProperty GridPhaseWorkingLocationProperty = BindableProperty.Create(
            // プロパティ名
            propertyName: nameof(GridPhaseWorkingLocation),
            // 返却型
            returnType: typeof(Geometric.PointFloat),
            // これを含んでいるクラス
            declaringType: typeof(TilesetGrid),
            // ヌルだと不具合が出る
            defaultValue: Geometric.PointFloat.Zero);
        #endregion

        #region 束縛可能プロパティ（グリッド単位）
        /// <summary>
        ///     グリッド単位
        /// </summary>
        public Geometric.SizeFloat GridUnit
        {
            get => (Geometric.SizeFloat)GetValue(GridUnitProperty);
            set => SetValue(GridUnitProperty, value);
        }

        /// <summary>
        ///     グリッド単位
        /// </summary>
        public static BindableProperty GridUnitProperty = BindableProperty.Create(
            // プロパティ名
            propertyName: nameof(GridUnit),
            // 返却型
            returnType: typeof(Geometric.SizeFloat),
            // これを含んでいるクラス
            declaringType: typeof(TilesetGrid),
            // ヌルだと不具合が出る
            defaultValue: Geometric.SizeFloat.Zero);
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
            Trace.WriteLine($"[TilesetGrid Draw] this.HalfThicknessOfGridLineAsInt: {this.HalfThicknessOfGridLineAsInt}, this.GridPhaseSourceLocation: {this.GridPhaseWorkingLocation.Dump()}, this.GridUnit: {this.GridUnit.Dump()} dirtyRect: ({dirtyRect.Width:.2}, {dirtyRect.Height:.2})");

            // 線の色
            canvas.StrokeColor = new Color((int)(0.7 * 255), (int)(0.7 * 255), (int)(0.7 * 255));

            // グリッドの線の太さの半分
            int halfThicknessOfLineAsInt = this.HalfThicknessOfGridLineAsInt;

            // グリッドの線の太さ
            var lineThickness = new Models.ThicknessOfLine(2 * halfThicknessOfLineAsInt);
            canvas.StrokeSize = lineThickness.AsInt;

            //
            // 縦線を引いていこう
            // ==================
            //
            {
                float y1 = halfThicknessOfLineAsInt + this.GridPhaseWorkingLocation.Y.AsFloat;
                float y2 = this.GridCanvasImageSize.Height.AsInt + halfThicknessOfLineAsInt + this.GridPhaseWorkingLocation.Y.AsFloat;

                float prevX;
                float x = 0;
                for (var i = 0; x < this.GridCanvasImageSize.Width.AsInt + halfThicknessOfLineAsInt; i++)
                {
                    prevX = x;
                    x = i * this.GridUnit.Width.AsFloat + this.GridPhaseWorkingLocation.X.AsFloat + halfThicknessOfLineAsInt;

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
                float x1 = halfThicknessOfLineAsInt + this.GridPhaseWorkingLocation.X.AsFloat;

                // CANCEL CODE: 横幅が偶数なら横幅を +1、奇数なら横幅を -1 するという TRICK CODE が別の箇所にあるので、
                //              imageWidth は +1 したり、 -1 したり振動している。これはつらい。
                //              そこで、右辺にもグリッドの線があるから　端まで線を引かなくていいことを利用し
                //              右辺の線の手前まで線を引くようにする
                float x2 = this.GridCanvasImageSize.Width.AsInt - halfThicknessOfLineAsInt + this.GridPhaseWorkingLocation.X.AsFloat;

                float prevY;
                float y = 0;
                for (var i = 0; y < this.GridCanvasImageSize.Height.AsInt + halfThicknessOfLineAsInt; i++)
                {
                    prevY = y;
                    y = i * this.GridUnit.Height.AsFloat + this.GridPhaseWorkingLocation.Y.AsFloat + halfThicknessOfLineAsInt;

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
