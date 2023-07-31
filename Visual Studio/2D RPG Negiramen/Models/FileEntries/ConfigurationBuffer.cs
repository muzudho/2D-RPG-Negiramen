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

    #region プロパティ（プロジェクトＩｄリスト）
    /// <summary>
    ///     プロジェクトＩｄリスト
    /// </summary>
    internal List<ProjectId>? ProjectIdList { get; set; }
    #endregion
}
