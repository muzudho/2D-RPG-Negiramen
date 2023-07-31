namespace _2D_RPG_Negiramen.Models.FileEntries.Locations.AppData;

using _2D_RPG_Negiramen;
using _2D_RPG_Negiramen.Models;

/// <summary>
///     😁 OSの 📂 アプリケーション・データ・フォルダ の `{あたなのサークル・フォルダ名}` フォルダ―の場所
///     
///     <list type="bullet">
///         <item>イミュータブル</item>
///         <item><see cref="App.Configuration"/></item>
///     </list>
/// </summary>
/// <example>
///     "C:\Users\むずでょ\AppData\Local\Packages\1802ca7b-559d-489e-8a13-f02ac4d27fcc_9zz4h110yvjzm\LocalState\Doujin Circle Negiramen"
/// </example>
internal class YourCircleFolder : Its
{
    // - その他

    #region その他（生成　関連）
    /// <summary>
    ///     生成
    /// </summary>
    internal YourCircleFolder(FileEntryPath parentPath)
        : base(pathSource: FileEntryPathSource.FromString(System.IO.Path.Combine(parentPath.AsStr, App.GetOrLoadConfiguration().RememberYourCircleFolderName.AsStr)),
               convert: (pathSource) => FileEntryPath.From(pathSource,
                                                           replaceSeparators: true))
    {
    }
    #endregion

    // - インターナル・プロパティ

    #region プロパティ（OSの 📂 アプリケーション・データ・フォルダ の `{あたなのサークル・フォルダ名}/{あなたの作品フォルダ名}` フォルダの場所）
    /// <summary>
    ///     OSの 📂 アプリケーション・データ・フォルダ の `{あたなのサークル・フォルダ名}/{あなたの作品フォルダ名}` フォルダの場所
    ///     
    ///     <list type="bullet">
    ///         <item>構成ファイルを作る前に　このプロパティを使うと、循環参照します</item>
    ///     </list>
    /// </summary>
    internal YourWorkFolder YourWorkFolder
    {
        get
        {
            if (yourWorkFolder == null)
            {
                yourWorkFolder = new YourWorkFolder(Path);
            }

            return yourWorkFolder;
        }
    }
    #endregion

    // - プライベート・フィールド

    YourWorkFolder? yourWorkFolder;
}
