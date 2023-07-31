﻿namespace _2D_RPG_Negiramen.Models.FileEntries.Locations.UnityAssets;

using _2D_RPG_Negiramen.Models;
using _2D_RPG_Negiramen.Models.FileEntries.Locations;

/// <summary>
///     😁 Unity の 📂 `Assets/｛あなたのサークル名｝/｛あなたの作品名｝/Auto Generated/Images` フォルダの場所
///     
///     <list type="bullet">
///         <item>イミュータブル</item>
///         <item><see cref="ItsFolder"/></item>
///     </list>
/// </summary>
internal class ImagesFolder : Its
{
    // - その他

    #region その他（生成　関連）
    /// <summary>
    ///     生成
    /// </summary>
    internal ImagesFolder(FileEntryPath parentPath)
        : base(pathSource: FileEntryPathSource.FromString(System.IO.Path.Combine(parentPath.AsStr, "Images")),
               convert: (pathSource) => FileEntryPath.From(pathSource,
                                                           replaceSeparators: true))
    {
    }
    #endregion

    // - インターナル・プロパティ

    #region プロパティ（Unity の 📂 `Assets/｛あなたのサークル名｝/｛あなたの作品名｝/Auto Generated/Images/Tilesets` フォルダの場所）
    /// <summary>
    ///     Unity の 📂 `Assets/｛あなたのサークル名｝/｛あなたの作品名｝/Auto Generated/Images/Tileset` フォルダの場所
    /// </summary>
    internal ImagesTilesetsFolder TilesetsFolder
    {
        get
        {
            if (tilesetFolder == null)
            {
                tilesetFolder = new ImagesTilesetsFolder(Path);
            }

            return tilesetFolder;
        }
    }
    #endregion

    // - プライベート・フィールド

    ImagesTilesetsFolder? tilesetFolder;
}
