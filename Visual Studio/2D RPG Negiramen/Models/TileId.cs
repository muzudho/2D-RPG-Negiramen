﻿namespace _2D_RPG_Negiramen.Models
{
    /// <summary>
    ///     😁 タイルＩｄ
    ///     
    ///     <list type="bullet">
    ///         <item>1 から始まる</item>
    ///         <item>0 を指定すると、空欄扱いとする（0 を扱うことはできない）</item>
    ///     </list>
    /// </summary>
    internal class TileId
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
        public static bool operator ==(TileId c1, TileId c2)
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

            return c1.source == c2.source;
        }

        /// <summary>
        ///     非等値か？
        /// </summary>
        /// <param name="c1">左項</param>
        /// <param name="c2">右項</param>
        /// <returns>そうだ</returns>
        public static bool operator !=(TileId c1, TileId c2)
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
            //if (!(obj is Id))

            // 要素で比較する
            TileId c = (TileId)obj;
            return (this.source == c.source);
            //または、
            //return (this.Number.Equals(c.Number));
        }

        /// <summary>
        ///     Equalsがtrueを返すときに同じ値を返す
        /// </summary>
        /// <returns>ハッシュ値</returns>
        public override int GetHashCode()
        {
            return this.source;
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
            if ((object)other == null)
                return 1;
            if (this.GetType() != other.GetType())
                throw new ArgumentException();
            return this.source.CompareTo(((TileId)other).source);
        }

        /// <summary>
        ///     小なりか？
        /// </summary>
        /// <param name="c1">左項</param>
        /// <param name="c2">右項</param>
        /// <returns>そうだ</returns>
        /// <exception cref="ArgumentNullException">左項と右項のいずれかがヌルだった</exception>
        public static bool operator <(TileId c1, TileId c2)
        {
            //nullの確認
            if ((object)c1 == null || (object)c2 == null)
            {
                throw new ArgumentNullException();
            }
            //CompareToメソッドを呼び出す
            return (c1.CompareTo(c2) < 0);
        }

        /// <summary>
        ///     大なりか？
        /// </summary>
        /// <param name="c1">左項</param>
        /// <param name="c2">右項</param>
        /// <returns>そうだ</returns>
        /// <exception cref="ArgumentNullException">左項と右項のいずれかがヌルだった</exception>
        public static bool operator >(TileId c1, TileId c2)
        {
            //逆にして"<"で比較
            return (c2 < c1);
        }

        /// <summary>
        ///     小なりイコールか？
        /// </summary>
        /// <param name="c1">左項</param>
        /// <param name="c2">右項</param>
        /// <returns>そうだ</returns>
        /// <exception cref="ArgumentNullException">左項と右項のいずれかがヌルだった</exception>
        public static bool operator <=(TileId c1, TileId c2)
        {
            //nullの確認
            if ((object)c1 == null || (object)c2 == null)
            {
                throw new ArgumentNullException();
            }
            //CompareToメソッドを呼び出す
            return (c1.CompareTo(c2) <= 0);
        }

        /// <summary>
        ///     大なりイコールか？
        /// </summary>
        /// <param name="c1">左項</param>
        /// <param name="c2">右項</param>
        /// <returns>そうだ</returns>
        /// <exception cref="ArgumentNullException">左項と右項のいずれかがヌルだった</exception>
        public static bool operator >=(TileId c1, TileId c2)
        {
            //逆にして"<="で比較
            return (c2 <= c1);
        }
        #endregion

        // - インターナル静的プロパティー

        #region プロパティ（ゼロ・オブジェクト）
        /// <summary>
        ///     ゼロ・オブジェクト
        /// </summary>
        internal static TileId Empty = new TileId(0);
        #endregion

        // - プライベート静的プロパティー

        /// <summary>
        ///     <pre>
        ///         BASE64 を、オーラル・コミュニケーションがしやすいように英単語に変換するもの
        ///     </pre>
        /// </summary>
        static Dictionary<char, string> PhoneticAlphabet = new Dictionary<char, string>()
        {
            {'A', "Alice"},
            {'B', "Boart"},
            {'C', "Castle"},
            {'D', "Drink"},
            {'E', "Elf"},
            {'F', "Forest"},
            {'G', "Gold"},
            {'H', "Hotel"},
            {'I', "Island"},
            {'J', "John"},
            {'K', "King"},
            {'L', "Level"},
            {'M', "Madam"},
            {'N', "News"},
            {'O', "Ork"},
            {'P', "Pond"},
            {'Q', "Queen"},
            {'R', "Room"},
            {'S', "Soup"},
            {'T', "Talk"},
            {'U', "Uncle"},
            {'V', "Video"},
            {'W', "Wolf"},
            {'X', "Xams"},
            {'Y', "Yogurt"},
            {'Z', "Zebra"},
            {'a', "and"},
            {'b', "big"},
            {'c', "clean"},
            {'d', "damage"},
            {'e', "evil"},
            {'f', "fake"},
            {'g', "get"},
            {'h', "hit"},
            {'i', "ice"},
            {'j', "jump"},
            {'k', "kick"},
            {'l', "love"},
            {'m', "magic"},
            {'n', "no"},
            {'o', "oily"},
            {'p', "poison"},
            {'q', "quiz"},
            {'r', "rain"},
            {'s', "solty"},
            {'t', "trendy"},
            {'u', "up"},
            {'v', "victory"},
            {'w', "wind"},
            {'x', "xrated"},
            {'y', "your"},
            {'z', "zenith"},
            {'0', "0"},
            {'1', "1"},
            {'2', "2"},
            {'3', "3"},
            {'4', "4"},
            {'5', "5"},
            {'6', "6"},
            {'7', "7"},
            {'8', "8"},
            {'9', "9"},
            {'+', "+"},
            {'/', "/"},
            {'=', "="},
        };

        // - その他

        /// <summary>
        ///     生成
        /// </summary>
        /// <param name="source">元の値</param>
        internal TileId(int source)
        {
            this.source = source;
        }

        // - フィールド

        /// <summary>
        ///     値
        /// </summary>
        int source;

        // - プロパティー

        /// <summary>
        ///     整数型形式で取得
        /// </summary>
        internal int AsInt => source;

        /// <summary>
        ///     BASE64形式
        ///     
        ///     <list type="bullet">
        ///         <item>0 は `MA==` だが、これは空文字として返却する</item>
        ///     </list>
        /// </summary>
        internal string AsBASE64
        {
            get
            {
                if (this.AsInt==0)
                {
                    return string.Empty;
                }

                return Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(this.AsInt.ToString())).Replace("=", "");
            }
        }

        /// <summary>
        ///     フォネティックコード形式
        /// </summary>
        internal string AsPhoneticCode
        {
            get
            {
                List<string> list = new List<string>();

                foreach (char c in AsBASE64)
                {
                    list.Add(PhoneticAlphabet[c]);
                }

                return String.Join(" ", list);
            }
        }
    }
}
