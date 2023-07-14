namespace _2D_RPG_Negiramen.Models
{
    /// <summary>
    ///     😁 位置
    /// </summary>
    internal class Point
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
        public static bool operator ==(Point c1, Point c2)
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

            return (c1.X == c2.X) && (c1.Y == c2.Y);
        }

        /// <summary>
        ///     非等値か？
        /// </summary>
        /// <param name="c1">左項</param>
        /// <param name="c2">右項</param>
        /// <returns>そうだ</returns>
        public static bool operator !=(Point c1, Point c2)
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
            //objがnullか、型が違うときは、等価でない
            if (obj == null || this.GetType() != obj.GetType())
            {
                return false;
            }
            // この型が継承できないクラスや構造体であれば、次のようにできる
            //if (!(obj is Point))

            // 要素で比較する
            Point c = (Point)obj;
            return (this.X == c.X) && (this.Y == c.Y);
            //または、
            //return (this.Number.Equals(c.Number));
        }

        /// <summary>
        ///     Equalsがtrueを返すときに同じ値を返す
        /// </summary>
        /// <returns>ハッシュ値</returns>
        public override int GetHashCode()
        {
            return (this.X, this.Y).GetHashCode();
        }
        #endregion

        // - 静的プロパティー

        /// <summary>
        ///     ゼロ・オブジェクト
        /// </summary>
        internal static Point Empty = new Point(Models.X.Empty, Models.Y.Empty);

        // - その他

        /// <summary>
        ///     生成
        /// </summary>
        /// <param name="x">位置ｘ</param>
        /// <param name="y">位置ｙ</param>
        internal Point(Models.X x, Models.Y y)
        {
            this.X = x;
            this.Y = y;
        }

        // - プロパティー

        /// <summary>
        ///     位置ｘ
        /// </summary>
        internal Models.X X { get; private set; }

        /// <summary>
        ///     位置ｙ
        /// </summary>
        internal Models.Y Y { get; private set; }

        // - インターナル・メソッド

        internal string Dump()
        {
            return $"X:{this.X.AsInt}, Y:{this.Y.AsInt}";
        }
    }
}
