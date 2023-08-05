namespace _2D_RPG_Negiramen.Models.FileEntries.Locations.AppData;

using _2D_RPG_Negiramen;
using _2D_RPG_Negiramen.Models;
using TheFileEntryLocations = _2D_RPG_Negiramen.Models.FileEntries.Locations;

/// <summary>
///     😁 OSの 📂 アプリケーション・データ・フォルダ の `{あたなのサークル・フォルダ名}/{あなたの作品フォルダ名}` フォルダ―の場所
///     
///     <list type="bullet">
///         <item>イミュータブル</item>
///         <item><see cref="App.Configuration"/></item>
///     </list>
/// </summary>
/// <example>
///     "C:\Users\むずでょ\AppData\Local\Packages\1802ca7b-559d-489e-8a13-f02ac4d27fcc_9zz4h110yvjzm\LocalState\Doujin Circle Negiramen\Negiramen Quest"
/// </example>
internal class YourWorkFolder : TheFileEntryLocations.ItsFolder
{
    // - その他

    #region その他（生成　関連）
    /// <summary>
    ///     生成
    /// </summary>
    internal YourWorkFolder(FileEntryPath parentPath)
        : base(pathSource: FileEntryPathSource.FromString(System.IO.Path.Combine(parentPath.AsStr, App.GetOrLoadConfiguration().CurrentYourWorkFolderName.AsStr)),
               evaluate: (pathSource) => FileEntryPath.From(pathSource,
                                                           replaceSeparators: true))
    {
    }
    #endregion

    // - インターナル・メソッド

    #region メソッド（プロジェクト構成ファイルの場所）
    /// <summary>
    ///     プロジェクト構成ファイルの場所
    /// </summary>
    internal ProjectConfigurationToml ProjectConfigurationToml
    {
        get
        {
            if (this.projectConfigurationToml == null)
            {
                this.projectConfigurationToml = new ProjectConfigurationToml(Path);
            }

            return this.projectConfigurationToml;
        }
    }
    #endregion

    // - プライベート・フィールド

    ProjectConfigurationToml? projectConfigurationToml;
}
