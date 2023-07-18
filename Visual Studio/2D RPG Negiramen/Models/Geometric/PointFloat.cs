namespace _2D_RPG_Negiramen.Models.Geometric
{
    /// <summary>
    ///     😁 位置
    ///     
    ///     <list type="bullet">
    ///         <item>float 型</item>
    ///         <item>用途：　図形描画。 SkiaSharp のメソッドが float 型で受け付けるから</item>
    ///     </list>
    /// </summary>
    public class PointFloat
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
        public static bool operator ==(PointFloat c1, PointFloat c2)
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
        public static bool operator !=(PointFloat c1, PointFloat c2)
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
            PointFloat c = (PointFloat)obj;
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
        internal static PointFloat Empty = new PointFloat(XFloat.Empty, YFloat.Empty);
        #endregion

        // - その他

        #region その他（生成）
        /// <summary>
        ///     生成
        /// </summary>
        /// <param name="x">位置ｘ</param>
        /// <param name="y">位置ｙ</param>
        internal PointFloat(XFloat x, YFloat y)
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
        internal XFloat X { get; private set; }
        #endregion

        #region プロパティ（位置ｙ）
        /// <summary>
        ///     位置ｙ
        /// </summary>
        internal YFloat Y { get; private set; }
        #endregion

        // - インターナル・メソッド

        #region メソッド（ダンプ）
        /// <summary>
        ///     ダンプ
        /// </summary>
        /// <returns></returns>
        internal string Dump()
        {
            return $"X:{X.AsFloat}, Y:{Y.AsFloat}";
        }
        #endregion
    }
}
