namespace _2D_RPG_Negiramen.Models.FileEntries.Locations.UnityAssets.Images;

using _2D_RPG_Negiramen.Coding;
using _2D_RPG_Negiramen.Models;
using _2D_RPG_Negiramen.Models.FileEntries.Locations;

/// <summary>
///     😁 Unity の 📄 `Assets/｛あなたのサークル名｝/｛あなたの作品名｝/Auto Generated/Images/Tilesets/Locales/{選択中のロケール}/{名前}.toml` ファイルの場所
///     
///     <list type="bullet">
///         <item>イミュータブル</item>
///         <item><see cref="ItsFolder"/></item>
///     </list>
/// </summary>
class TilesetLocalToml : ItsFile
{
    // - その他

    #region その他（生成　関連）
    /// <summary>
    ///     生成
    /// </summary>
    internal TilesetLocalToml()
        : base()
    {
    }

    /// <summary>
    ///     生成
    /// </summary>
    internal TilesetLocalToml(FileEntryPathSource pathSource, LazyArgs.Convert<FileEntryPathSource, FileEntryPath> convert)
        : base(pathSource, convert)
    {
    }
    #endregion

    // - インターナル静的プロパティ

    #region プロパティ（空オブジェクト）
    /// <summary>
    ///     空オブジェクト
    /// </summary>
    internal static TilesetLocalToml Empty { get; } = new TilesetLocalToml();
    #endregion
}
