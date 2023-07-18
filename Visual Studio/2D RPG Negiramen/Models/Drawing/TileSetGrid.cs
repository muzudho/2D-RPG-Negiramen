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

/* プロジェクト '2D RPG Negiramen (net7.0-windows10.0.19041.0)' からのマージされていない変更
前:
        public Models.PointInt GridPhase
後:
        public PointInt GridPhase
*/
        public Geometric.PointInt GridPhase
        {

/* プロジェクト '2D RPG Negiramen (net7.0-windows10.0.19041.0)' からのマージされていない変更
前:
            get => (Models.PointInt)GetValue(GridPhaseProperty);
後:
            get => (PointInt)GetValue(GridPhaseProperty);
*/
            get => (Geometric.PointInt)GetValue(GridPhaseProperty);
            set => SetValue(GridPhaseProperty, value);
        }

        /// <summary>
        ///     グリッド位相の左上表示位置
        /// </summary>
        public static BindableProperty GridPhaseProperty = BindableProperty.Create(
            // プロパティ名
            propertyName: nameof(GridPhase),
            // 返却型

/* プロジェクト '2D RPG Negiramen (net7.0-windows10.0.19041.0)' からのマージされていない変更
前:
            returnType: typeof(Models.PointInt),
後:
            returnType: typeof(PointInt),
*/
            returnType: typeof(Geometric.PointInt),
            // これを含んでいるクラス
            declaringType: typeof(TilesetGrid),
            // ヌルだと不具合が出る

/* プロジェクト '2D RPG Negiramen (net7.0-windows10.0.19041.0)' からのマージされていない変更
前:
            defaultValue: Models.PointInt.Empty);
後:
            defaultValue: PointInt.Empty);
*/
            defaultValue: Geometric.PointInt.Empty);
        #endregion

        #region 束縛可能プロパティ（グリッド・タイル・サイズ）
        /// <summary>
        ///     グリッド・タイル・サイズ
        /// </summary>

/* プロジェクト '2D RPG Negiramen (net7.0-windows10.0.19041.0)' からのマージされていない変更
前:
        public Models.SizeInt GridTileSize
後:
        public SizeInt GridTileSize
*/
        public Geometric.SizeInt GridTileSize
        {

/* プロジェクト '2D RPG Negiramen (net7.0-windows10.0.19041.0)' からのマージされていない変更
前:
            get => (Models.SizeInt)GetValue(GridTileSizeProperty);
後:
            get => (SizeInt)GetValue(GridTileSizeProperty);
*/
            get => (Geometric.SizeInt)GetValue(GridTileSizeProperty);
            set => SetValue(GridTileSizeProperty, value);
        }

        /// <summary>
        ///     グリッド・タイル・サイズ
        /// </summary>
        public static BindableProperty GridTileSizeProperty = BindableProperty.Create(
            // プロパティ名
            propertyName: nameof(GridTileSize),
            // 返却型

/* プロジェクト '2D RPG Negiramen (net7.0-windows10.0.19041.0)' からのマージされていない変更
前:
            returnType: typeof(Models.SizeInt),
後:
            returnType: typeof(SizeInt),
*/
            returnType: typeof(Geometric.SizeInt),
            // これを含んでいるクラス
            declaringType: typeof(TilesetGrid),
            // ヌルだと不具合が出る

/* プロジェクト '2D RPG Negiramen (net7.0-windows10.0.19041.0)' からのマージされていない変更
前:
            defaultValue: Models.SizeInt.Empty);
後:
            defaultValue: SizeInt.Empty);
*/
            defaultValue: Geometric.SizeInt.Empty);
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

            // グリッド位相の左上表示位置
            int gridPhaseLeftAsInt = this.GridPhase.X.AsInt;
            int gridPhaseTopAsInt = this.GridPhase.Y.AsInt;

            // グリッド・タイル・サイズ

/* プロジェクト '2D RPG Negiramen (net7.0-windows10.0.19041.0)' からのマージされていない変更
前:
            Models.SizeInt gridTileSize = this.GridTileSize;
後:
            SizeInt gridTileSize = this.GridTileSize;
*/
            Geometric.SizeInt gridTileSize = this.GridTileSize;

            // キャンバス・サイズ
            var canvasWidth = this.GridCanvasImageSize.Width.AsInt;
            var canvasHeight = this.GridCanvasImageSize.Height.AsInt;

            //
            // 縦線を引いていこう
            // ==================
            //
            {
                int y1 = halfThicknessOfLineAsInt + gridPhaseTopAsInt;
                int y2 = canvasHeight + halfThicknessOfLineAsInt + gridPhaseTopAsInt;

                int prevX = 0;
                int x = 0;
                for (var i = 0; x < canvasWidth + halfThicknessOfLineAsInt; i++)
                {
                    prevX = x;
                    x = i * gridTileSize.Width.AsInt + gridPhaseLeftAsInt + halfThicknessOfLineAsInt;

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
                int x1 = halfThicknessOfLineAsInt + gridPhaseLeftAsInt;

                // CANCEL CODE: 横幅が偶数なら横幅を +1、奇数なら横幅を -1 するという TRICK CODE が別の箇所にあるので、
                //              imageWidth は +1 したり、 -1 したり振動している。これはつらい。
                //              そこで、右辺にもグリッドの線があるから　端まで線を引かなくていいことを利用し
                //              右辺の線の手前まで線を引くようにする
                int x2 = canvasWidth - halfThicknessOfLineAsInt + gridPhaseLeftAsInt;

                int prevY = 0;
                int y = 0;
                for (var i = 0; y < canvasHeight + halfThicknessOfLineAsInt; i++)
                {
                    prevY = y;
                    y = i * gridTileSize.Height.AsInt + gridPhaseTopAsInt + halfThicknessOfLineAsInt;

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
