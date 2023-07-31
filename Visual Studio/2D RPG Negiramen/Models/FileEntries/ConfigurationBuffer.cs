﻿namespace _2D_RPG_Negiramen.Models.FileEntries;

using TheFileEntryLocations = _2D_RPG_Negiramen.Models.FileEntries.Locations;

/// <summary>
///     😁 構成ファイルの差分
///     
///     <list type="bullet">
///         <item>ミュータブル</item>
///     </list>
/// </summary>
internal class ConfigurationBuffer
{
    // - インターナル・プロパティ

    // TODO ★ 廃止予定
    /// <summary>
    ///     ネギラーメンの 📂 `Starter Kit` フォルダへのパス
    /// </summary>
    internal Locations.StarterKit.ItsFolder? StarterKitFolder { get; set; }

    // TODO ★ 廃止予定
    /// <summary>
    ///     Unity の 📂 `Assets` フォルダへのパス
    /// </summary>
    internal TheFileEntryLocations.UnityAssets.ItsFolder? UnityAssetsFolder { get; set; }

    #region プロパティ（あなたのサークル・フォルダ名）
    /// <summary>
    ///     あなたのサークル・フォルダ名
    /// </summary>
    internal YourCircleFolderName? RememberYourCircleFolderName { get; set; }
    #endregion

    #region プロパティ（あなたの作品フォルダ名）
    /// <summary>
    ///     あなたの作品フォルダ名
    /// </summary>
    internal YourWorkFolderName? RememberYourWorkFolderName { get; set; }
    #endregion

    #region プロパティ（エントリー・リスト）
    /// <summary>
    ///     エントリー・リスト
    /// </summary>
    internal List<ConfigurationEntry>? EntryList { get; set; }
    #endregion
}
