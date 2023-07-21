namespace _2D_RPG_Negiramen.Models.Geometric
{
    /// <summary>
    ///     😁 縦幅
    ///     
    ///     <list type="bullet">
    ///         <item>float 型</item>
    ///         <item>用途：　図形描画。 SkiaSharp のメソッドが float 型で受け付けるから</item>
    ///     </list>
    /// </summary>
    internal class HeightFloat
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
        public static bool operator ==(HeightFloat c1, HeightFloat c2)
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

            return c1.source == c2.source;
        }

        /// <summary>
        ///     非等値か？
        /// </summary>
        /// <param name="c1">左項</param>
        /// <param name="c2">右項</param>
        /// <returns>そうだ</returns>
        public static bool operator !=(HeightFloat c1, HeightFloat c2)
        {
            // (c1 != c2)とすると、無限ループ
            return !(c1 == c2);
        }

        /// <summary>
        /// 任意のオブジェクトと、自分自身が等価か？
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
            //if (!(obj is Height))

            // 要素で比較する
            HeightFloat c = (HeightFloat)obj;
            return source == c.source;
            //または、
            //return (this.Number.Equals(c.Number));
        }

        /// <summary>
        ///     Equalsがtrueを返すときに同じ値を返す
        /// </summary>
        /// <returns>ハッシュ値</returns>
        public override int GetHashCode()
        {
            return source.GetHashCode();
        }
        #endregion

        #region 演算子のオーバーロード（大小比較）
        /// <summary>
        ///     <pre>
        ///         自分自身が、別のオブジェクトより小さいときはマイナスの数、大きいときはプラスの数、
        ///         同じときは0を返す
        ///     </pre>
        /// </summary>
        /// <param name="other">別のオブジェクト</param>
        /// <returns>差</returns>
        /// <exception cref="ArgumentException">自分自身と、別のオブジェクトが別の型だった</exception>
        public int CompareTo(object other)
        {
            if (other == null)
                return 1;
            if (GetType() != other.GetType())
                throw new ArgumentException();
            return source.CompareTo(((HeightFloat)other).source);
        }

        /// <summary>
        ///     小なりか？
        /// </summary>
        /// <param name="c1">左項</param>
        /// <param name="c2">右項</param>
        /// <returns>そうだ</returns>
        /// <exception cref="ArgumentNullException">左項と右項のいずれかがヌルだった</exception>
        public static bool operator <(HeightFloat c1, HeightFloat c2)
        {
            //nullの確認
            if ((object)c1 == null || (object)c2 == null)
            {
                throw new ArgumentNullException();
            }
            //CompareToメソッドを呼び出す
            return c1.CompareTo(c2) < 0;
        }

        /// <summary>
        ///     大なりか？
        /// </summary>
        /// <param name="c1">左項</param>
        /// <param name="c2">右項</param>
        /// <returns>そうだ</returns>
        /// <exception cref="ArgumentNullException">左項と右項のいずれかがヌルだった</exception>
        public static bool operator >(HeightFloat c1, HeightFloat c2)
        {
            //逆にして"<"で比較
            return c2 < c1;
        }

        /// <summary>
        ///     小なりイコールか？
        /// </summary>
        /// <param name="c1">左項</param>
        /// <param name="c2">右項</param>
        /// <returns>そうだ</returns>
        /// <exception cref="ArgumentNullException">左項と右項のいずれかがヌルだった</exception>
        public static bool operator <=(HeightFloat c1, HeightFloat c2)
        {
            //nullの確認
            if ((object)c1 == null || (object)c2 == null)
            {
                throw new ArgumentNullException();
            }
            //CompareToメソッドを呼び出す
            return c1.CompareTo(c2) <= 0;
        }

        /// <summary>
        ///     大なりイコールか？
        /// </summary>
        /// <param name="c1">左項</param>
        /// <param name="c2">右項</param>
        /// <returns>そうだ</returns>
        /// <exception cref="ArgumentNullException">左項と右項のいずれかがヌルだった</exception>
        public static bool operator >=(HeightFloat c1, HeightFloat c2)
        {
            //逆にして"<="で比較
            return c2 <= c1;
        }
        #endregion

        // - その他

        #region その他（生成）
        /// <summary>
        ///     生成
        /// </summary>
        /// <param name="source">元の値</param>
        internal HeightFloat(float source)
        {
            this.source = source;
        }
        #endregion

        // - インターナル静的プロパティー

        #region プロパティ（ゼロ・オブジェクト）
        /// <summary>
        ///     ゼロ・オブジェクト
        /// </summary>
        internal static HeightFloat Empty = new HeightFloat(0);
        #endregion

        // - インターナル・プロパティー

        #region プロパティ（値。浮動小数点数形式）
        /// <summary>
        ///     値。浮動小数点数形式
        /// </summary>
        internal float AsFloat => source;
        #endregion

        // - インターナル・メソッド

        #region メソッド（型変換）
        /// <summary>
        ///     型変換
        /// </summary>
        /// <returns></returns>
        internal HeightInt ToInt()
        {
            return new HeightInt((int)this.source);
        }
        #endregion

        // - プライベート・フィールド

        #region フィールド（値）
        /// <summary>
        ///     値
        /// </summary>
        float source;
        #endregion
    }
}
