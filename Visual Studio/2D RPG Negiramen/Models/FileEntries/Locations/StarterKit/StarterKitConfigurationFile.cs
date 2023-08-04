namespace _2D_RPG_Negiramen.Models.FileEntries.Locations.StarterKit;

using _2D_RPG_Negiramen.Coding;
using TheFileEntryLocations = _2D_RPG_Negiramen.Models.FileEntries.Locations;

/// <summary>
///     😁 ネギラーメンの 📂 `Starter Kit/For Unity Assets` フォルダの場所
///     
///     <list type="bullet">
///         <item>イミュータブル</item>
///     </list>
/// </summary>
class StarterKitConfigurationFile : TheFileEntryLocations.ItsFile
{
    // - その他

    #region その他（生成　関連）
    /// <summary>
    ///     生成
    /// </summary>
    internal StarterKitConfigurationFile()
        : base()
    {
    }

    /// <summary>
    ///     <pre>
    ///         生成
    ///         
    ///         以下の場所から呼び出される
    ///     </pre>
    ///     <list type="bullet">
    ///         <item>構成ファイル読込時</item>
    ///     </list>
    /// </summary>
    internal StarterKitConfigurationFile(FileEntryPathSource pathSource, LazyArgs.Convert<FileEntryPathSource, FileEntryPath> convert)
        : base(pathSource, convert)
    {
    }
    #endregion

    // - インターナル静的プロパティ

    #region プロパティ（空オブジェクト）
    /// <summary>
    ///     空オブジェクト
    /// </summary>
    internal static StarterKitConfigurationFile Empty { get; } = new StarterKitConfigurationFile();
    #endregion
}
