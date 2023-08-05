namespace _2D_RPG_Negiramen.Models.FileEntries.Locations.UnityAssets.Images;

using _2D_RPG_Negiramen.Models;
using System.Globalization;
using TheFileEntryLocations = _2D_RPG_Negiramen.Models.FileEntries.Locations;

/// <summary>
///     😁 Unity の 📂 `Assets/｛あなたのサークル名｝/｛あなたの作品名｝/Auto Generated/Images/Tilesets/Locales/{選択中のロケール}` フォルダの場所
///     
///     <list type="bullet">
///         <item>イミュータブル</item>
///         <item><see cref="Locations.ItsFolder"/></item>
///     </list>
/// </summary>
internal class SelectedLocaleFolder : TheFileEntryLocations.ItsFolder
{
    /// <summary>
    ///     生成
    /// </summary>
    internal SelectedLocaleFolder(FileEntryPath parentPath, CultureInfo cultureInfo)
        : base(pathSource: FileEntryPathSource.FromString(System.IO.Path.Combine(parentPath.AsStr, cultureInfo.Name)),
               evaluate: (pathSource) => FileEntryPath.From(pathSource,
                                                           replaceSeparators: true))
    {
        //this.CultureInfo = cultureInfo;

    }

    // - インターナル・メソッド

    #region メソッド（Unity の 📄 `Assets/｛あなたのサークル名｝/｛あなたの作品名｝/Auto Generated/Images/Tilesets/Locales/{選択中のロケール}/{名前}.toml` ファイルの場所）
    /// <summary>
    ///     Unity の 📄 `Assets/｛あなたのサークル名｝/｛あなたの作品名｝/Auto Generated/Images/Tilesets/Locales/{選択中のロケール}/{名前}.toml` ファイルの場所
    /// </summary>
    internal TilesetLocalToml CreateTilesetLocalToml(FileStem fileStem)
    {
        return new TilesetLocalToml(
            pathSource: FileEntryPathSource.FromString(
                System.IO.Path.Combine(Path.AsStr, $"{fileStem.AsStr}.toml")),
            convert: (pathSource) => FileEntryPath.From(pathSource,
                                                        replaceSeparators: true));
    }
    #endregion

    // - プライベート・プロパティ

    ///// <summary>
    /////     文化情報
    ///// </summary>
    //CultureInfo CultureInfo { get; set; }
}
