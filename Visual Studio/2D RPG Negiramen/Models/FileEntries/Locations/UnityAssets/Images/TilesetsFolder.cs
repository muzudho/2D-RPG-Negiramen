namespace _2D_RPG_Negiramen.Models.FileEntries.Locations.UnityAssets.Images;

using _2D_RPG_Negiramen.Models;
using TheFileEntryLocations = _2D_RPG_Negiramen.Models.FileEntries.Locations;

/// <summary>
///     😁 Unity の 📂 `Assets/｛あなたのサークル名｝/｛あなたの作品名｝/Auto Generated/Images/Tilesets` フォルダの場所
///     
///     <list type="bullet">
///         <item>イミュータブル</item>
///         <item><see cref="Locations.ItsFolder"/></item>
///     </list>
/// </summary>
class TilesetsFolder : TheFileEntryLocations.ItsFolder
{
    // - その他

    #region その他（生成　関連）
    /// <summary>
    ///     生成
    /// </summary>
    internal TilesetsFolder(FileEntryPath parentPath)
        : base(pathSource: FileEntryPathSource.FromString(System.IO.Path.Combine(parentPath.AsStr, "Tilesets")),
               evaluate: (pathSource) => FileEntryPath.From(pathSource,
                                                           replaceSeparators: true))
    {
    }
    #endregion

    // - インターナル・プロパティ

    #region プロパティ（Unity の 📂 `Assets/｛あなたのサークル名｝/｛あなたの作品名｝/Auto Generated/Images/Tilesets` フォルダの場所）
    /// <summary>
    ///     Unity の 📂 `Assets/｛あなたのサークル名｝/｛あなたの作品名｝/Auto Generated/Images/Tileset` フォルダの場所
    /// </summary>
    internal LocalesFolder LocalesFolder
    {
        get
        {
            if (localesFolder == null)
            {
                localesFolder = new LocalesFolder(Path);
            }

            return localesFolder;
        }
    }
    #endregion

    // - インターナル・メソッド

    #region メソッド（Unity の 📄 `Assets/｛あなたのサークル名｝/｛あなたの作品名｝/Auto Generated/Images/Tilesets/{名前}.png` ファイルの場所）
    /// <summary>
    ///     Unity の 📄 `Assets/｛あなたのサークル名｝/｛あなたの作品名｝/Auto Generated/Images/Tilesets/{名前}.png` ファイルの場所
    /// </summary>
    internal TilesetPng CreateTilesetPng(string fileStem)
    {
        return new TilesetPng(
            pathSource: FileEntryPathSource.FromString(
                System.IO.Path.Combine(Path.AsStr, $"{fileStem}.png")),
            convert: (pathSource) => FileEntryPath.From(pathSource,
                                                        replaceSeparators: true));
    }
    #endregion

    #region メソッド（Unity の 📄 `Assets/｛あなたのサークル名｝/｛あなたの作品名｝/Auto Generated/Images/Tilesets/{名前}.toml` ファイルの場所）
    /// <summary>
    ///     Unity の 📄 `Assets/｛あなたのサークル名｝/｛あなたの作品名｝/Auto Generated/Images/Tilesets/{名前}.toml` ファイルの場所
    /// </summary>
    internal TilesetGlobalToml CreateTilesetGlobalToml(FileStem fileStem)
    {
        return new TilesetGlobalToml(
            pathSource: FileEntryPathSource.FromString(
                System.IO.Path.Combine(Path.AsStr, $"{fileStem.AsStr}.toml")),
            convert: (pathSource) => FileEntryPath.From(pathSource,
                                                        replaceSeparators: true));
    }
    #endregion

    // - プライベート・フィールド

    LocalesFolder? localesFolder;
}
