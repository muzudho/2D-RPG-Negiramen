namespace _2D_RPG_Negiramen.Models.FileEntries.Locations.UnityAssets;

using _2D_RPG_Negiramen.Models;
using _2D_RPG_Negiramen.Models.FileEntries.Locations;

/// <summary>
///     😁 Unity の 📂 `Assets/｛あなたのサークル名｝/｛あなたの作品名｝/Auto Generated/Data/Data/CSV/Tileset` フォルダの場所
///     
///     <list type="bullet">
///         <item>イミュータブル</item>
///         <item><see cref="ItsFolder"/></item>
///     </list>
/// </summary>
class DataCsvTilesetsFolder : Its
{
    // - その他

    #region その他（生成　関連）
    /// <summary>
    ///     生成
    /// </summary>
    internal DataCsvTilesetsFolder(FileEntryPath parentPath)
        : base(pathSource: FileEntryPathSource.FromString(System.IO.Path.Combine(parentPath.AsStr, "Tilesets")),
               convert: (pathSource) => FileEntryPath.From(pathSource,
                                                           replaceSeparators: true))
    {
    }
    #endregion

    #region プロパティ（Unity の 📂 `Assets/｛あなたのサークル名｝/｛あなたの作品名｝/Auto Generated/Images/Tilesets/{名前}.csv` フォルダの場所）
    /// <summary>
    ///     Unity の 📂 `Assets/｛あなたのサークル名｝/｛あなたの作品名｝/Auto Generated/Images/Tilesets/{名前}.csv` フォルダの場所
    /// </summary>
    internal DataCsvTilesetCsv CreateTilesetCsv(string fileStem)
    {
        return new DataCsvTilesetCsv(
            pathSource: FileEntryPathSource.FromString(
                System.IO.Path.Combine(Path.AsStr, $"{fileStem}.csv")),
            convert: (pathSource) => FileEntryPath.From(pathSource,
                                                        replaceSeparators: true));
    }
    #endregion
}
