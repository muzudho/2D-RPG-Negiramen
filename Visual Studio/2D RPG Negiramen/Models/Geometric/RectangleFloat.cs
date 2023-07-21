namespace _2D_RPG_Negiramen.Models.Geometric
{
    /// <summary>
    ///     😁 矩形
    ///     
    ///     <list type="bullet">
    ///         <item>float 型</item>
    ///         <item>用途：　図形描画。 SkiaSharp のメソッドが float 型で受け付けるから</item>
    ///     </list>
    /// </summary>
    public class RectangleFloat
    {
        // - 演算子のオーバーロード

        #region 演算子のオーバーロード（== と !=）
        /// <summary>
        ///     <pre>
        ///         等値か？
        ///         
        ///         📖 [自作クラスの演算子をオーバーロードする](https://dobon.net/vb/dotnet/beginner/operator.html)
        ///         📖 [自作クラスのEqualsメソッドをオーバーライドして、等価の定義を変更する](https://dobon.net/vb/dotnet/beginner/equals.html)
        ///     </pre>
        /// </summary>
        /// <param name="c1">左項</param>
        /// <param name="c2">右項</param>
        /// <returns>そうだ</returns>
        public static bool operator ==(RectangleFloat c1, RectangleFloat c2)
        {
            // nullの確認（構造体のようにNULLにならない型では不要）
            // 両方nullか（参照元が同じか）
            // (c1 == c2)とすると、無限ループ
            if (ReferenceEquals(c1, c2))
            {
                return true;
            }

            // どちらかがnullか
            // (c1 == null)とすると、無限ループ
            if ((object)c1 == null || (object)c2 == null)
            {
                return false;
            }

            return c1.Point == c2.Point && c1.Size == c2.Size;
        }

        /// <summary>
        ///     非等値か？
        /// </summary>
        /// <param name="c1">左項</param>
        /// <param name="c2">右項</param>
        /// <returns>そうだ</returns>
        public static bool operator !=(RectangleFloat c1, RectangleFloat c2)
        {
            // (c1 != c2)とすると、無限ループ
            return !(c1 == c2);
        }

        /// <summary>
        ///     任意のオブジェクトと、自分自身が等価か？
        /// </summary>
        /// <param name="obj">任意のオブジェクト</param>
        /// <returns>そうだ</returns>
        public override bool Equals(object obj)
        {
            // objがnullか、型が違うときは、等価でない
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            // この型が継承できないクラスや構造体であれば、次のようにできる
            //if (!(obj is Rectangle))

            // 要素で比較する
            RectangleFloat c = (RectangleFloat)obj;
            return Point == c.Point && Size == c.Size;
            //または、
            //return (this.Number.Equals(c.Number));
        }

        /// <summary>
        ///     Equalsがtrueを返すときに同じ値を返す
        /// </summary>
        /// <returns>ハッシュ値</returns>
        public override int GetHashCode()
        {
            return (Point, Size).GetHashCode();
        }
        #endregion

        // - インターナル静的プロパティ

        #region プロパティ（空オブジェクト）
        /// <summary>
        ///     空オブジェクト
        /// </summary>
        internal static RectangleFloat Empty = new RectangleFloat(PointFloat.Empty, SizeFloat.Empty);
        #endregion

        // - その他

        #region その他（生成）
        /// <summary>
        ///     生成
        /// </summary>
        /// <param name="sourceRect">矩形の位置とサイズ</param>
        internal static RectangleFloat FromModel(RectangleInt sourceRect)
        {
            return new RectangleFloat(
                point: new PointFloat(
                    x: new XFloat(sourceRect.Point.X.AsInt),
                    y: new YFloat(sourceRect.Point.Y.AsInt)),
                size: new SizeFloat(
                    width: new WidthFloat(sourceRect.Size.Width.AsInt),
                    height: new HeightFloat(sourceRect.Size.Height.AsInt)));
        }

        /// <summary>
        ///     生成
        /// </summary>
        /// <param name="point">位置</param>
        /// <param name="size">大きさ</param>
        internal RectangleFloat(PointFloat point, SizeFloat size)
        {
            Point = point;
            Size = size;
        }
        #endregion

        // - インターナル・プロパティー

        #region プロパティ（位置）
        /// <summary>
        ///     位置
        /// </summary>
        internal PointFloat Point { get; private set; }
        #endregion

        #region プロパティ（大きさ）
        /// <summary>
        ///     大きさ
        /// </summary>
        internal SizeFloat Size { get; private set; }
        #endregion

        // - インターナル・メソッド

        #region メソッド（描画で使う形式）
        /// <summary>
        ///     描画で使う形式
        /// </summary>
        /// <returns></returns>
        internal Rect AsGraphis()
        {
            return new Rect(
                x: Point.X.AsFloat,
                y: Point.Y.AsFloat,
                width: Size.Width.AsFloat,
                height: Size.Height.AsFloat);
        }
        #endregion

        #region メソッド（プロパティ出力）
        /// <summary>
        ///     プロパティ出力
        /// </summary>
        /// <returns></returns>
        internal string Dump()
        {
            return $"Point:{Point.Dump()}, Size:{Size.Dump()}";
        }
        #endregion
    }
}
