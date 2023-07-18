namespace _2D_RPG_Negiramen.Models
{
    /// <summary>
    ///     😁 大きさ
    ///     
    ///     <list type="bullet">
    ///         <item>int 型</item>
    ///     </list>
    /// </summary>
    public class SizeInt
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
        public static bool operator ==(SizeInt c1, SizeInt c2)
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

            return (c1.Width == c2.Width) && (c1.Height == c2.Height);
        }

        /// <summary>
        ///     非等値か？
        /// </summary>
        /// <param name="c1">左項</param>
        /// <param name="c2">右項</param>
        /// <returns>そうだ</returns>
        public static bool operator !=(SizeInt c1, SizeInt c2)
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
            //if (!(obj is Size))

            // 要素で比較する
            SizeInt c = (SizeInt)obj;
            return (this.Width == c.Width) && (this.Height == c.Height);
            //または、
            //return (this.Number.Equals(c.Number));
        }

        /// <summary>
        ///     Equalsがtrueを返すときに同じ値を返す
        /// </summary>
        /// <returns>ハッシュ値</returns>
        public override int GetHashCode()
        {
            return (this.Width, this.Height).GetHashCode();
        }
        #endregion

        // - インターナル静的プロパティー

        #region プロパティ（ゼロ・オブジェクト）
        /// <summary>
        ///     ゼロ・オブジェクト
        /// </summary>
        internal static SizeInt Empty = new SizeInt(Models.WidthInt.Empty, Models.HeightInt.Empty);
        #endregion

        // - その他

        #region その他（生成）
        /// <summary>
        ///     生成
        /// </summary>
        /// <param name="width">横幅</param>
        /// <param name="height">縦幅</param>
        internal SizeInt(Models.WidthInt width, Models.HeightInt height)
        {
            this.Width = width;
            this.Height = height;
        }
        #endregion

        // - インターナル・プロパティー

        #region プロパティ（横幅）
        /// <summary>
        ///     横幅
        /// </summary>
        internal Models.WidthInt Width { get; private set; }
        #endregion

        #region プロパティ（縦幅）
        /// <summary>
        ///     縦幅
        /// </summary>
        internal Models.HeightInt Height { get; private set; }
        #endregion

        // - インターナル・メソッド

        #region メソッド（ダンプ）
        /// <summary>
        ///     ダンプ
        /// </summary>
        /// <returns></returns>
        internal string Dump()
        {
            return $"Width:{this.Width.AsInt}, Height:{this.Height.AsInt}";
        }
        #endregion
    }
}
