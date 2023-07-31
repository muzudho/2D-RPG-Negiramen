namespace _2D_RPG_Negiramen.Models;

using _2D_RPG_Negiramen.Models.Geometric;

/// <summary>
///     構成ファイルの entry 配列の要素
///     
///     <list type="bullet">
///         <item>イミュータブル</item>
///     </list>
/// </summary>
internal class ConfigurationEntry
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
    public static bool operator ==(ConfigurationEntry c1, ConfigurationEntry c2)
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

        return c1.YourCircleFolderName == c2.YourCircleFolderName &&
               c1.YourWorkFolderName == c2.YourWorkFolderName;
    }

    /// <summary>
    ///     非等値か？
    /// </summary>
    /// <param name="c1">左項</param>
    /// <param name="c2">右項</param>
    /// <returns>そうだ</returns>
    public static bool operator !=(ConfigurationEntry c1, ConfigurationEntry c2)
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
        //if (!(obj is Y))

        // 要素で比較する
        ConfigurationEntry c = (ConfigurationEntry)obj;
        return this.YourCircleFolderName == c.YourCircleFolderName &&
               this.YourWorkFolderName == c.YourWorkFolderName;
        //または、
        //return (this.Number.Equals(c.Number));
    }

    /// <summary>
    ///     Equalsがtrueを返すときに同じ値を返す
    /// </summary>
    /// <returns>ハッシュ値</returns>
    public override int GetHashCode() => (this.YourCircleFolderName, this.YourWorkFolderName).GetHashCode();
    #endregion

    // - その他

    #region その他（生成）
    /// <summary>
    ///     生成
    /// </summary>
    /// <param name="yourCircleFolderName">あなたのサークル・フォルダ名</param>
    /// <param name="yourWorkFolderName">あなたの作品フォルダ名</param>
    internal ConfigurationEntry(
        YourCircleFolderName yourCircleFolderName,
        YourWorkFolderName yourWorkFolderName)
    {
        this.YourCircleFolderName = yourCircleFolderName;
        this.YourWorkFolderName = yourWorkFolderName;
    }
    #endregion

    // - パブリック・プロパティ

    #region プロパティ（表示用文字列）
    /// <summary>
    ///     表示用文字列
    /// </summary>
    public string PresentableTextAsStr => $"{this.YourCircleFolderName.AsStr}/{this.YourWorkFolderName.AsStr}";
    #endregion

    // - インターナル・プロパティ

    #region プロパティ（あなたのサークル・フォルダ名）
    /// <summary>
    ///     あなたのサークル・フォルダ名
    /// </summary>
    internal YourCircleFolderName YourCircleFolderName { get; }
    #endregion

    #region プロパティ（あなたの作品名）
    /// <summary>
    ///     あなたの作品名
    /// </summary>
    internal YourWorkFolderName YourWorkFolderName { get; }
    #endregion
}
