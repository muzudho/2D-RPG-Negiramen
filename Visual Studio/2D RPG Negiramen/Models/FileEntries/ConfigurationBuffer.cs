namespace _2D_RPG_Negiramen.Models.FileEntries;

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

    #region プロパティ（現在のサークル・フォルダ名）
    /// <summary>
    ///     現在のサークル・フォルダ名
    /// </summary>
    internal YourCircleFolderName? CurrentYourCircleFolderName { get; set; }
    #endregion

    #region プロパティ（現在のあなたの作品フォルダ名）
    /// <summary>
    ///     現在のあなたの作品フォルダ名
    /// </summary>
    internal YourWorkFolderName? CurrentYourWorkFolderName { get; set; }
    #endregion

    #region プロパティ（プロジェクトＩｄリスト）
    /// <summary>
    ///     プロジェクトＩｄリスト
    /// </summary>
    internal List<ProjectId>? ProjectIdList { get; set; }
    #endregion
}
