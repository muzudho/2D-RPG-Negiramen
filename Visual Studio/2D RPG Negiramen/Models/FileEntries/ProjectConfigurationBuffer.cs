namespace _2D_RPG_Negiramen.Models.FileEntries;

using TheFileEntryLocations = _2D_RPG_Negiramen.Models.FileEntries.Locations;

/// <summary>
///     😁 プロジェクト構成ファイルの差分
///     
///     <list type="bullet">
///         <item>ミュータブル</item>
///     </list>
/// </summary>
internal class ProjectConfigurationBuffer
{
    // - インターナル・プロパティ

    #region プロパティ（ネギラーメンの 📂 `Starter Kit` フォルダの場所）
    /// <summary>
    ///     ネギラーメンの 📂 `Starter Kit` フォルダの場所
    /// </summary>
    internal Locations.StarterKit.ItsFolder? StarterKitFolderLocation { get; set; }
    #endregion

    #region プロパティ（Unity の 📂 `Assets` フォルダの場所）
    /// <summary>
    ///     Unity の 📂 `Assets` フォルダの場所
    /// </summary>
    internal TheFileEntryLocations.UnityAssets.ItsFolder? UnityAssetsFolderLocation { get; set; }
    #endregion
}
