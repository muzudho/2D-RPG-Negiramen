﻿namespace _2D_RPG_Negiramen.Models
{
    /// <summary>
    ///     😁 あなたの作品名
    /// </summary>
    class YourWorkName
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
        public static bool operator ==(YourWorkName c1, YourWorkName c2)
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
        public static bool operator !=(YourWorkName c1, YourWorkName c2)
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
            YourWorkName c = (YourWorkName)obj;
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

        #region プロパティ（その他）
        /// <summary>
        ///     文字列を与えて初期化
        /// </summary>
        /// <param name="yourWorkName">あなたの作品名</param>
        /// <returns>実例</returns>
        internal static YourWorkName FromString(string yourWorkName)
        {
            if (yourWorkName == null)
            {
                throw new ArgumentNullException(nameof(yourWorkName));
            }

            return new YourWorkName(yourWorkName);
        }

        /// <summary>
        ///     生成
        /// </summary>
        internal YourWorkName()
        {
            this.source = string.Empty;
        }

        /// <summary>
        ///     生成
        /// </summary>
        internal YourWorkName(string source)
        {
            this.source = source;
        }
        #endregion

        // - パブリック・メソッド

        #region メソッド（暗黙的な文字列形式）
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
        internal static YourWorkName Empty { get; } = new YourWorkName();
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
        string source;
        #endregion
    }
}
