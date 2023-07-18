namespace _2D_RPG_Negiramen.Models
{
    using TheGraphics = Microsoft.Maui.Graphics;

    /// <summary>
    ///     😁 矩形
    ///     
    ///     <list type="bullet">
    ///         <item>int 型</item>
    ///     </list>
    /// </summary>
    internal class RectangleInt
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
        public static bool operator ==(RectangleInt c1, RectangleInt c2)
        {
            // nullの確認（構造体のようにNULLにならない型では不要）
            // 両方nullか（参照元が同じか）
            // (c1 == c2)とすると、無限ループ
            if (object.ReferenceEquals(c1, c2))
            {
                return true;
            }

            // どちらかがnullか
            // (c1 == null)とすると、無限ループ
            if (((object)c1 == null) || ((object)c2 == null))
            {
                return false;
            }

            return (c1.Point == c2.Point) && (c1.Size == c2.Size);
        }

        /// <summary>
        ///     非等値か？
        /// </summary>
        /// <param name="c1">左項</param>
        /// <param name="c2">右項</param>
        /// <returns>そうだ</returns>
        public static bool operator !=(RectangleInt c1, RectangleInt c2)
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
            if (obj == null || this.GetType() != obj.GetType())
            {
                return false;
            }
            // この型が継承できないクラスや構造体であれば、次のようにできる
            //if (!(obj is Rectangle))

            // 要素で比較する
            RectangleInt c = (RectangleInt)obj;
            return (this.Point == c.Point) && (this.Size == c.Size);
            //または、
            //return (this.Number.Equals(c.Number));
        }

        /// <summary>
        ///     Equalsがtrueを返すときに同じ値を返す
        /// </summary>
        /// <returns>ハッシュ値</returns>
        public override int GetHashCode()
        {
            return (this.Point, this.Size).GetHashCode();
        }
        #endregion

        // - インターナル静的プロパティ

        #region プロパティ（空オブジェクト）
        /// <summary>
        ///     空オブジェクト
        /// </summary>
        internal static RectangleInt Empty = new RectangleInt(Models.PointInt.Empty, Models.SizeInt.Empty);
        #endregion

        // - その他

        #region その他（生成）
        /// <summary>
        ///     生成
        /// </summary>
        /// <param name="point">位置</param>
        /// <param name="size">大きさ</param>
        internal RectangleInt(Models.PointInt point, Models.SizeInt size)
        {
            this.Point = point;
            this.Size = size;
        }
        #endregion

        // - インターナル・プロパティー

        #region プロパティ（位置）
        /// <summary>
        ///     位置
        /// </summary>
        internal Models.PointInt Point { get; private set; }
        #endregion

        #region プロパティ（大きさ）
        /// <summary>
        ///     大きさ
        /// </summary>
        internal Models.SizeInt Size { get; private set; }
        #endregion

        // - インターナル・メソッド

        #region メソッド（描画で使う形式）
        /// <summary>
        ///     描画で使う形式
        /// </summary>
        /// <returns></returns>
        internal TheGraphics.Rect AsGraphis()
        {
            return new Rect(
                x: this.Point.X.AsInt,
                y: this.Point.Y.AsInt,
                width: this.Size.Width.AsInt,
                height: this.Size.Height.AsInt);
        }
        #endregion

        #region メソッド（プロパティ出力）
        /// <summary>
        ///     プロパティ出力
        /// </summary>
        /// <returns></returns>
        internal string Dump()
        {
            return $"Point:{this.Point.Dump()}, Size:{this.Size.Dump()}";
        }
        #endregion
    }
}
