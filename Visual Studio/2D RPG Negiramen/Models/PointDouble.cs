﻿namespace _2D_RPG_Negiramen.Models
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
            if (obj == null || this.GetType() != obj.GetType())
            {
                return false;
            }
            // この型が継承できないクラスや構造体であれば、次のようにできる
            //if (!(obj is Point))

            // 要素で比較する
            PointDouble c = (PointDouble)obj;
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

        #region プロパティ（ゼロ・オブジェクト）
        /// <summary>
        ///     ゼロ・オブジェクト
        /// </summary>
        internal static PointDouble Empty = new PointDouble(Models.XDouble.Empty, Models.YDouble.Empty);
        #endregion

        // - その他

        #region その他（生成）
        /// <summary>
        ///     生成
        /// </summary>
        /// <param name="x">位置ｘ</param>
        /// <param name="y">位置ｙ</param>
        internal PointDouble(Models.XDouble x, Models.YDouble y)
        {
            this.X = x;
            this.Y = y;
        }
        #endregion

        // - インターナル・プロパティー

        #region プロパティ（位置ｘ）
        /// <summary>
        ///     位置ｘ
        /// </summary>
        internal Models.XDouble X { get; private set; }
        #endregion

        #region プロパティ（位置ｙ）
        /// <summary>
        ///     位置ｙ
        /// </summary>
        internal Models.YDouble Y { get; private set; }
        #endregion

        // - インターナル・メソッド

        #region メソッド（ダンプ）
        /// <summary>
        ///     ダンプ
        /// </summary>
        /// <returns></returns>
        internal string Dump()
        {
            return $"X:{this.X.AsDouble}, Y:{this.Y.AsDouble}";
        }
        #endregion
    }
}
