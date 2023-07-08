using _2D_RPG_Negiramen.Models;

namespace _2D_RPG_Negiramen.Coding
{
    /// <summary>
    ///     ヌルか、ヌルでないかの確認を課す仕組み
    /// </summary>
    /// <typeparam name="T">任意の型</typeparam>
    class Option<T>
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
        public static bool operator ==(Option<T> c1, Option<T> c2)
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

            // T型変数 == T型変数 とは書けないらしい
            return c1.Equals(c2);   // return c1.Some == c2.Some;
        }

        /// <summary>
        ///     非等値か？
        /// </summary>
        /// <param name="c1">左項</param>
        /// <param name="c2">右項</param>
        /// <returns>そうだ</returns>
        public static bool operator !=(Option<T> c1, Option<T> c2)
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
            //if (!(obj is Option))

            // 要素で比較する
            Option<T> c = (Option<T>)obj;
            // T型変数 == T型変数 とは書けないらしい
            // return (this.Some == c.Some);
            //または、
            return (this.Some.Equals(c.Some));
        }

        /// <summary>
        ///     Equalsがtrueを返すときに同じ値を返す
        /// </summary>
        /// <returns>ハッシュ値</returns>
        public override int GetHashCode()
        {
            return this.Some.GetHashCode();
        }
        #endregion

        // - インターナル静的プロパティ

        /// <summary>
        ///     ヌル・オブジェクト
        /// </summary>
        internal static Option<T> None = new Option<T>(default(T));

        // - その他

        /// <summary>
        /// 生成
        /// </summary>
        /// <param name="some"></param>
        internal Option(T some)
        {
            this.Some = some;
        }

        // - インターナル・メソッド

        internal bool TryGetValue(out T some)
        {
            some = this.Some;
            return some != null;
        }

        // - プライベート・プロパティ

        /// <summary>
        /// 値
        /// </summary>
        T Some { get; set; }
    }
}
