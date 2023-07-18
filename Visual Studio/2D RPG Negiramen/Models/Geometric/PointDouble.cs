namespace _2D_RPG_Negiramen.Models.Geometric
{
    /// <summary>
    ///     😁 位置
    ///     
    ///     <list type="bullet">
    ///         <item>double 型</item>
    ///     </list>
    /// </summary>
    public class PointDouble
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
        public static bool operator ==(PointDouble c1, PointDouble c2)
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

            return c1.X == c2.X && c1.Y == c2.Y;
        }

        /// <summary>
        ///     非等値か？
        /// </summary>
        /// <param name="c1">左項</param>
        /// <param name="c2">右項</param>
        /// <returns>そうだ</returns>
        public static bool operator !=(PointDouble c1, PointDouble c2)
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
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            // この型が継承できないクラスや構造体であれば、次のようにできる
            //if (!(obj is Point))

            // 要素で比較する
            PointDouble c = (PointDouble)obj;
            return X == c.X && Y == c.Y;
            //または、
            //return (this.Number.Equals(c.Number));
        }

        /// <summary>
        ///     Equalsがtrueを返すときに同じ値を返す
        /// </summary>
        /// <returns>ハッシュ値</returns>
        public override int GetHashCode()
        {
            return (X, Y).GetHashCode();
        }
        #endregion

        // - 静的プロパティー

        #region プロパティ（ゼロ・オブジェクト）
        /// <summary>
        ///     ゼロ・オブジェクト
        /// </summary>

        /* プロジェクト '2D RPG Negiramen (net7.0-windows10.0.19041.0)' からのマージされていない変更
        前:
                internal static PointDouble Empty = new PointDouble(Models.XDouble.Empty, Models.YDouble.Empty);
        後:
                internal static PointDouble Empty = new PointDouble(XDouble.Empty, Models.YDouble.Empty);
        */

        /* プロジェクト '2D RPG Negiramen (net7.0-windows10.0.19041.0)' からのマージされていない変更
        前:
                internal static PointDouble Empty = new PointDouble(Geometric.XDouble.Empty, Models.YDouble.Empty);
        後:
                internal static PointDouble Empty = new PointDouble(Geometric.XDouble.Empty, YDouble.Empty);
        */
        internal static PointDouble Empty = new PointDouble(XDouble.Empty, YDouble.Empty);
        #endregion

        // - その他

        #region その他（生成）
        /// <summary>
        ///     生成
        /// </summary>
        /// <param name="x">位置ｘ</param>
        /// <param name="y">位置ｙ</param>

        /* プロジェクト '2D RPG Negiramen (net7.0-windows10.0.19041.0)' からのマージされていない変更
        前:
                internal PointDouble(Models.XDouble x, Models.YDouble y)
        後:
                internal PointDouble(XDouble x, Models.YDouble y)
        */

        /* プロジェクト '2D RPG Negiramen (net7.0-windows10.0.19041.0)' からのマージされていない変更
        前:
                internal PointDouble(Geometric.XDouble x, Models.YDouble y)
        後:
                internal PointDouble(Geometric.XDouble x, YDouble y)
        */
        internal PointDouble(XDouble x, YDouble y)
        {
            X = x;
            Y = y;
        }
        #endregion

        // - インターナル・プロパティー

        #region プロパティ（位置ｘ）
        /// <summary>
        ///     位置ｘ
        /// </summary>

        /* プロジェクト '2D RPG Negiramen (net7.0-windows10.0.19041.0)' からのマージされていない変更
        前:
                internal Models.XDouble X { get; private set; }
        後:
                internal XDouble X { get; private set; }
        */
        internal XDouble X { get; private set; }
        #endregion

        #region プロパティ（位置ｙ）
        /// <summary>
        ///     位置ｙ
        /// </summary>

        /* プロジェクト '2D RPG Negiramen (net7.0-windows10.0.19041.0)' からのマージされていない変更
        前:
                internal Models.YDouble Y { get; private set; }
        後:
                internal YDouble Y { get; private set; }
        */
        internal YDouble Y { get; private set; }
        #endregion

        // - インターナル・メソッド

        #region メソッド（ダンプ）
        /// <summary>
        ///     ダンプ
        /// </summary>
        /// <returns></returns>
        internal string Dump()
        {
            return $"X:{X.AsDouble}, Y:{Y.AsDouble}";
        }
        #endregion
    }
}
