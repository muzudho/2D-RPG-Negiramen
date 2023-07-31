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

    #region プロパティ（ネギラーメンの 📂 `Starter Kit` フォルダへのパス）
    /// <summary>
    ///     ネギラーメンの 📂 `Starter Kit` フォルダへのパス
    /// </summary>
    internal Locations.StarterKit.ItsFolder? StarterKitFolder { get; set; }
    #endregion

    #region プロパティ（Unity の 📂 `Assets` フォルダへのパス）
    /// <summary>
    ///     Unity の 📂 `Assets` フォルダへのパス
    /// </summary>
    internal TheFileEntryLocations.UnityAssets.ItsFolder? UnityAssetsFolder { get; set; }
    #endregion
}
