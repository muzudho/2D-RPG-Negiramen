﻿namespace _2D_RPG_Negiramen.Models.FileEntries.Locations.UnityAssets;

using _2D_RPG_Negiramen.Models;
using _2D_RPG_Negiramen.Models.FileEntries.Locations;

/// <summary>
///     😁 Unity の 📂 `Assets/｛あなたのサークル・フォルダ名｝/｛あなたの作品フォルダ名｝` フォルダの場所
///     
///     <list type="bullet">
///         <item>イミュータブル</item>
///         <item><see cref="_2D_RPG_Negiramen.Models.FileEntries.Locations.UnityAssets.ItsFolder"/></item>
///     </list>
/// </summary>
internal class YourWorkFolder : ItsFolder
{
    // - その他

    #region その他（生成　関連）
    /// <summary>
    ///     生成
    /// </summary>
    internal YourWorkFolder(FileEntryPath parentPath)
        : base(pathSource: FileEntryPathSource.FromString(System.IO.Path.Combine(parentPath.AsStr, App.GetOrLoadConfiguration().CurrentYourWorkFolderName.AsStr)),
               convert: (pathSource) => FileEntryPath.From(pathSource,
                                                           replaceSeparators: true))
    {
    }
    #endregion

    // - インターナル・プロパティ

    #region プロパティ（Unity の 📂 `Assets/｛あなたのサークル名｝/｛あなたの作品名｝/Auto Generated` フォルダの場所）
    /// <summary>
    ///     Unity の 📂 `Assets/｛あなたのサークル名｝/｛あなたの作品名｝/Auto Generated` フォルダの場所
    /// </summary>
    internal AutoGeneratedFolder AutoGeneratedFolder
    {
        get
        {
            if (autoGeneratedFolder == null)
            {
                autoGeneratedFolder = new AutoGeneratedFolder(Path);
            }

            return autoGeneratedFolder;
        }
    }
    #endregion

    // - プライベート・フィールド

    AutoGeneratedFolder? autoGeneratedFolder;
}
