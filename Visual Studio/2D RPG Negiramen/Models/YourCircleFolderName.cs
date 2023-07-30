namespace _2D_RPG_Negiramen.Models
{
    /// <summary>
    ///     😁 あなたのサークル・フォルダ名
    /// </summary>
    class YourCircleFolderName
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
        public static bool operator ==(YourCircleFolderName c1, YourCircleFolderName c2)
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
        public static bool operator !=(YourCircleFolderName c1, YourCircleFolderName c2)
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
            //if (!(obj is X))

            // 要素で比較する
            YourCircleFolderName c = (YourCircleFolderName)obj;
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
            return (source).GetHashCode();
        }
        #endregion

        // - その他

        #region その他（生成　関連）
        /// <summary>
        ///     文字列を与えて初期化
        /// </summary>
        /// <param name="yourCircleName">あなたのサークル名</param>
        /// <returns>実例</returns>
        internal static YourCircleFolderName FromString(string yourCircleName)
        {
            if (yourCircleName == null)
                throw new ArgumentNullException(nameof(yourCircleName));

            return new YourCircleFolderName(yourCircleName);
        }

        /// <summary>
        ///     生成
        /// </summary>
        internal YourCircleFolderName()
        {
            this.source = string.Empty;
        }

        /// <summary>
        ///     生成
        /// </summary>
        internal YourCircleFolderName(string source)
        {
            this.source = source;
        }
        #endregion

        // - パブリック・プロパティ

        #region プロパティ（暗黙的な文字列形式）
        /// <summary>
        ///     暗黙的な文字列形式
        /// </summary>
        public override string ToString() => AsStr;
        #endregion

        // - インターナル静的プロパティ

        #region プロパティ（空オブジェクト）
        /// <summary>
        ///     空オブジェクト
        /// </summary>
        internal static YourCircleFolderName Empty { get; } = new YourCircleFolderName();
        #endregion

        // - インターナル・プロパティ

        #region プロパティ（文字列形式）
        /// <summary>
        ///     文字列形式
        /// </summary>
        internal string AsStr => this.source;
        #endregion

        // - プライベート・フィールド

        #region フィールド（入力値）
        /// <summary>
        ///     入力値
        /// </summary>
        string source { get; }
        #endregion
    }
}
