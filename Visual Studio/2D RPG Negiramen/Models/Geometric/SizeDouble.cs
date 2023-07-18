﻿namespace _2D_RPG_Negiramen.Models.Geometric
{
    /// <summary>
    ///     😁 大きさ
    ///     
    ///     <list type="bullet">
    ///         <item>double 型</item>
    ///     </list>
    /// </summary>
    public class SizeDouble
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
        public static bool operator ==(SizeDouble c1, SizeDouble c2)
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

            return c1.Width == c2.Width && c1.Height == c2.Height;
        }

        /// <summary>
        ///     非等値か？
        /// </summary>
        /// <param name="c1">左項</param>
        /// <param name="c2">右項</param>
        /// <returns>そうだ</returns>
        public static bool operator !=(SizeDouble c1, SizeDouble c2)
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
            //if (!(obj is Size))

            // 要素で比較する
            SizeDouble c = (SizeDouble)obj;
            return Width == c.Width && Height == c.Height;
            //または、
            //return (this.Number.Equals(c.Number));
        }

        /// <summary>
        ///     Equalsがtrueを返すときに同じ値を返す
        /// </summary>
        /// <returns>ハッシュ値</returns>
        public override int GetHashCode()
        {
            return (Width, Height).GetHashCode();
        }
        #endregion

        // - インターナル静的プロパティー

        #region プロパティ（ゼロ・オブジェクト）
        /// <summary>
        ///     ゼロ・オブジェクト
        /// </summary>

        /* プロジェクト '2D RPG Negiramen (net7.0-windows10.0.19041.0)' からのマージされていない変更
        前:
                internal static SizeDouble Empty = new SizeDouble(Models.WidthDouble.Empty, Models.HeightDouble.Empty);
        後:
                internal static SizeDouble Empty = new SizeDouble(WidthDouble.Empty, Models.HeightDouble.Empty);
        */
        internal static SizeDouble Empty = new SizeDouble(WidthDouble.Empty, HeightDouble.Empty);
        #endregion

        // - その他

        #region その他（生成）
        /// <summary>
        ///     生成
        /// </summary>
        /// <param name="width">横幅</param>
        /// <param name="height">縦幅</param>

        /* プロジェクト '2D RPG Negiramen (net7.0-windows10.0.19041.0)' からのマージされていない変更
        前:
                internal SizeDouble(Models.WidthDouble width, Models.HeightDouble height)
        後:
                internal SizeDouble(WidthDouble width, Models.HeightDouble height)
        */
        internal SizeDouble(WidthDouble width, HeightDouble height)
        {
            Width = width;
            Height = height;
        }
        #endregion

        // - インターナル・プロパティー

        #region プロパティ（横幅）
        /// <summary>
        ///     横幅
        /// </summary>

        /* プロジェクト '2D RPG Negiramen (net7.0-windows10.0.19041.0)' からのマージされていない変更
        前:
                internal Models.WidthDouble Width { get; private set; }
        後:
                internal WidthDouble Width { get; private set; }
        */
        internal WidthDouble Width { get; private set; }
        #endregion

        #region プロパティ（縦幅）
        /// <summary>
        ///     縦幅
        /// </summary>
        internal HeightDouble Height { get; private set; }
        #endregion

        // - インターナル・メソッド

        #region メソッド（ダンプ）
        /// <summary>
        ///     ダンプ
        /// </summary>
        /// <returns></returns>
        internal string Dump()
        {
            return $"Width:{Width.AsDouble}, Height:{Height.AsDouble}";
        }
        #endregion
    }
}
